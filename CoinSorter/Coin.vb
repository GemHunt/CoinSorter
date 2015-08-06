Imports System.Threading
Imports System.Text
Imports System.IO
Imports System.Web.Script.Serialization

Public Class Coin
    Public coinID As Int32  'The first CroppedImageID
    Public CoinCroppedImages As New HashSet(Of CroppedImage)
    Public CoinTypeID As Int32
    Public HorizontalCenter As Int32
    Public CaptureTime As DateTime
    Public Sub New(croppedImage As CroppedImage)
        coinID = croppedImage.CroppedImageID
        CoinCroppedImages.Add(croppedImage)
        HorizontalCenter = croppedImage.HorizontalCenter
        CaptureTime = croppedImage.CaptureTime
        Dim st As Stopwatch = Stopwatch.StartNew
        CoinTypeID = Classify()
        Console.WriteLine("Classify took: " & st.ElapsedMilliseconds)
    End Sub

    Public Function Classify() As Int32
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
        Dim tails As Double
        If j("predictions")(0)(0) = "trails" Then
            tails = j("predictions")(0)(1)
        Else
            tails = j("predictions")(1)(1)
        End If
        If tails > 90 Then
            Return 1
        Else
            Return 0
        End If
    End Function
End Class
