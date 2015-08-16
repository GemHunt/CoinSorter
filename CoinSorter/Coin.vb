Imports System.Threading
Imports System.Text
Imports System.IO
Imports System.Web.Script.Serialization

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
        Dim st As Stopwatch = Stopwatch.StartNew
        Type = Classify()
        Console.WriteLine("Classify took: " & st.ElapsedMilliseconds)
        Dim ArchivedTypeDirectory As String = ArchivedDirectory & Type & "\"
        If Not Directory.Exists(ArchivedTypeDirectory) Then
            Directory.CreateDirectory(ArchivedTypeDirectory)
        End If
        File.Copy(CurrentDirectory & croppedImage.FileName, ArchivedTypeDirectory & croppedImage.FileName)

    End Sub

    Public Function Classify() As String
        'This works, but is very clunky as it flashes the command shell 
        'This needs to be replaced with a .Net POST!
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
End Class
