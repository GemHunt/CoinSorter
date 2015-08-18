﻿Imports System.Threading
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

    Public Function GetDigitsCall() As String
        Dim digitsCall As New StringBuilder
        digitsCall.Append(DigitsIPAddress)
        digitsCall.Append(":5000/models/images/classification/classify_one.json")
        digitsCall.Append(" -XPOST -F job_id=")
        digitsCall.Append(DigitsJobID)
        digitsCall.Append(" -F image_file=@")
        digitsCall.Append(CurrentDirectory & CoinCroppedImages(0).FileName)
        Return digitsCall.ToString
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

                Dim crop = Bitmap.FromFile(fileName)
                Dim converter As New ImageConverter()
                Dim buff() As Byte = converter.ConvertTo(crop, GetType(Byte()))
                content.Add(New StreamContent(New MemoryStream(buff)), "image_file", "image_file")

                Using message = Await client.PostAsync(GetDigitsCall(digitsIPAddress, digitsJobID), content)
                    Dim sOutput = Await message.Content.ReadAsStringAsync()
                    crop.Dispose()

                    Dim j As Object = New JavaScriptSerializer().Deserialize(Of Object)(sOutput)
                    TypeStrength = j("predictions")(0)(1)
                    Type = j("predictions")(0)(0)

                    Dim ArchivedTypeDirectory As String = ArchivedDirectory & Type & "\"
                    If Not Directory.Exists(ArchivedTypeDirectory) Then
                        Directory.CreateDirectory(ArchivedTypeDirectory)
                    End If
                    File.Copy(CurrentDirectory & CoinCroppedImages(0).FileName, ArchivedTypeDirectory & CoinCroppedImages(0).FileName)

                    'Sort out strong Lincoln memorials:
                    If Coins(coinID).Type = "tails" And Coins(coinID).TypeStrength > 80 Then
                        PowerSupply.ToggleSolenoid(Coins(coinID).CaptureTime, Coins(coinID).HorizontalCenter)
                    End If

                End Using

            End Using
        End Using
    End Function




End Class
