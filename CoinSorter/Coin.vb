Imports System.Threading
Imports System.Text
Imports System.IO
Imports System.Web.Script.Serialization
Imports System.Net.Http


Public Class Coin
    Public coinID As Int64  'The first CroppedImageID
    Public CoinCroppedImages As New HashSet(Of CroppedImage)
    Public Type As String
    Public TypeStrength As Double
    Public HorizontalCenter As Int32
    Public CaptureTime As DateTime
    Public Sub New(croppedImage As CroppedImage)
        coinID = croppedImage.CroppedImageID
        CoinCroppedImages.Add(croppedImage)
        HorizontalCenter = croppedImage.HorizontalCenter
        CaptureTime = croppedImage.CaptureTime

        'Dim st As Stopwatch = Stopwatch.StartNew
        'Type = ClassifyWithCurl()
        'Console.WriteLine("Classify took: " & st.ElapsedMilliseconds)

        Dim t As Task = Upload(DigitsIPAddress, DigitsJobID, CurrentDirectory & CoinCroppedImages(0).FileName)

    End Sub

    Public Function ClassifyWithCurl() As String
        'This works, but is very clunky as it flashes the command shell 
        'The .Net POST is much better
        Dim oProcess As New Process()
        Dim callURL As String = DigitsIPAddress & ":5000/models/images/classification/classify_one.json"
        Dim commandLine As New StringBuilder
        commandLine.Append(callURL)
        commandLine.Append(" -XPOST -F job_id=")
        commandLine.Append(DigitsJobID)
        commandLine.Append(" -F image_file=@")
        commandLine.Append(CurrentDirectory & CoinCroppedImages(0).FileName)
        Console.WriteLine(commandLine.ToString)

        Dim oStartInfo As New ProcessStartInfo("curl.exe", commandLine.ToString)
        oStartInfo.UseShellExecute = False
        oStartInfo.RedirectStandardOutput = True
        oProcess.StartInfo = oStartInfo
        oProcess.Start()
        Dim sOutput As String
        Using oStreamReader As System.IO.StreamReader = oProcess.StandardOutput
            sOutput = oStreamReader.ReadToEnd()
        End Using
        Console.WriteLine(sOutput)
        Dim j As Object = New JavaScriptSerializer().Deserialize(Of Object)(sOutput)
        TypeStrength = j("predictions")(0)(1)
        Return j("predictions")(0)(0)
    End Function

    Public Function Classify() As String
        Dim callURL As String = DigitsIPAddress & ":5000/models/images/classification/classify_one.json"
        Dim commandLine As New StringBuilder
        commandLine.Append(callURL)
        commandLine.Append(" -XPOST -F job_id=")
        commandLine.Append(DigitsJobID)
        commandLine.Append(" -F image_file=@")
        commandLine.Append(CurrentDirectory & CoinCroppedImages(0).FileName)
        Console.WriteLine(commandLine.ToString)
        Dim sOutput As String = "temp"
        Console.WriteLine(sOutput)
        Dim j As Object = New JavaScriptSerializer().Deserialize(Of Object)(sOutput)
        TypeStrength = j("predictions")(0)(1)
        Return j("predictions")(0)(0)
    End Function

    Public Shared Function GetDigitsCall(DigitsIPAddress As String, DigitsJobID As String) As String
        Dim digitsCall As New StringBuilder()
        digitsCall.Append("http://")
        digitsCall.Append(DigitsIPAddress)
        digitsCall.Append(":5000/models/images/classification/classify_one.json")
        'digitsCall.Append("?job_id=");
        'digitsCall.Append(DigitsJobID);
        Return digitsCall.ToString()
    End Function

    Public Async Function Upload(digitsIPAddress As String, digitsJobID As String, fileName As String) As Task
        Using client = New System.Net.Http.HttpClient()
            Using content = New MultipartFormDataContent()
                Dim job_id As New StringContent(digitsJobID, System.Text.Encoding.UTF8)
                content.Add(job_id, "job_id")

                'A delay was put in to allow the file to be fully written: 
                Thread.Sleep(50)
                'Depending on how the image was read,
                'DIGITS was complaining files where tuncated by 20-30 byte without this 50 ms delay,
                'or an "out of memory" error was occuring,
                'and it's not a memory issue, it's a poorly worded mesg from Windows GDI
                
                Dim crop As Bitmap = Bitmap.FromFile(fileName)
                'Dim crop2 As Bitmap = crop.Clone
                'crop.Dispose()
                Dim converter As New ImageConverter()
                Dim buff() As Byte = converter.ConvertTo(crop, GetType(Byte()))
                crop.Dispose()

                content.Add(New StreamContent(New MemoryStream(buff)), "image_file", "image_file")

                Using message = Await client.PostAsync(GetDigitsCall(digitsIPAddress, digitsJobID), content)
                    Dim sOutput = Await message.Content.ReadAsStringAsync()

                    Dim j As Object = New JavaScriptSerializer().Deserialize(Of Object)(sOutput)
                    TypeStrength = j("predictions")(0)(1)
                    Type = j("predictions")(0)(0)

                    Dim ArchivedTypeDirectory As String = ArchivedDirectory & Type & "\"
                    If Not Directory.Exists(ArchivedTypeDirectory) Then
                        Directory.CreateDirectory(ArchivedTypeDirectory)
                    End If
                    File.Copy(CurrentDirectory & CoinCroppedImages(0).FileName, ArchivedTypeDirectory & CoinCroppedImages(0).FileName)

                    'Sort out strong Lincoln memorials:
                    'If Coins(coinID).Type = "tails" And Coins(coinID).TypeStrength > 80 Then

                    'Sort out Lincoln memorials:
                    If Coins(coinID).Type = "1982D" Or Coins(coinID).Type = "1982P" Then
                        PowerSupply.ToggleSolenoid(Coins(coinID).CaptureTime, Coins(coinID).HorizontalCenter)
                    End If
                End Using
            End Using
        End Using
    End Function

    Public Function ConvertImageFiletoBytes(ByVal ImageFilePath As String) As Byte()
        Dim _tempByte() As Byte = Nothing
        If String.IsNullOrEmpty(ImageFilePath) = True Then
            Throw New ArgumentNullException("Image File Name Cannot be Null or Empty", "ImageFilePath")
            Return Nothing
        End If
        Try
            Dim _fileInfo As New IO.FileInfo(ImageFilePath)
            Dim _NumBytes As Long = _fileInfo.Length
            Dim _FStream As New IO.FileStream(ImageFilePath, IO.FileMode.Open, IO.FileAccess.Read)
            Dim _BinaryReader As New IO.BinaryReader(_FStream)
            _tempByte = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes))
            _fileInfo = Nothing
            _NumBytes = 0
            _FStream.Close()
            _FStream.Dispose()
            _BinaryReader.Close()
            Return _tempByte
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
