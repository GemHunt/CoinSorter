Imports System.Threading
Imports System.Text
Imports System.IO

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
    End Sub

    Public Sub Classify()
        Static coinType As Int32 = 0
        If coinType = 0 Then
            coinType = 1
        Else
            coinType = 0
        End If
        CoinTypeID = coinType
        Exit Sub

        'This works, but clunky!
        'Dim oProcess As New Process()
        'Dim oStartInfo As New ProcessStartInfo("F:\curl.exe", "54.87.125.0:5000/models/images/classification/classify_one.json -XPOST -F job_id=20150720-190912-2cfe -F image_file=@F:\Pennies\Run001\crop\100002_106.jpg")
        'oStartInfo.UseShellExecute = False
        'oStartInfo.RedirectStandardOutput = True
        'oProcess.StartInfo = oStartInfo
        'oProcess.Start()
        'Dim sOutput As String
        'Using oStreamReader As System.IO.StreamReader = oProcess.StandardOutput
        '    sOutput = oStreamReader.ReadToEnd()
        'End Using
    End Sub
End Class
