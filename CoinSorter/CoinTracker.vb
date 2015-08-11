Imports System.IO
'This is the singleton "class" for the project. 
Module CoinTracker
    Public FullImages As New FullImageDirectory
    Public CroppedImages As New CroppedImageDirectory
    Public Coins As New CoinDirectory
    Public PowerSupply As New HPPowerSupply
    Public CoinSpeedPixelsPerMs As Double = 1.14
    Public CoinRadiusOnFullImage As Double = 140
    Public MsCoinsAreInView As Double = 10000
    Public ImageWidth As Int32 = 960
    Public DigitsIPAddress As String = "50.16.43.125"
    'Public DigitsJobID As String = "20150720-190912-2cfe"  ' Very simple AlexNet 256x256 Heads Tails
    Public DigitsJobID As String = "20150810-213455-64df"


    Public CurrentDirectory As String = "C:/Temp/TempCoinImages/"
    Public ArchivedDirectory As String = "F:/liveview/"

    Public Sub HandleNewImages(sender As Object, e As IO.FileSystemEventArgs)
        Dim fi As New FileInfo(e.FullPath)
        If fi.Name.Length = 10 Then
            Dim fullImage As New FullImage(fi.Name)
            FullImages.Add(fullImage)
        End If
        If fi.Name.Length = 20 Then
            Dim croppedImage As New CroppedImage(fi.Name)
            CroppedImages.Add(croppedImage)
            Dim coinIsDup As Boolean
            Dim coinID As Int32
            Coins.AddorUpdate(croppedImage, coinIsDup, coinID)
            If Not coinIsDup And Coins(coinID).CoinTypeID = 1 Then
                PowerSupply.ToggleSolenoid(Coins(coinID).CaptureTime, Coins(coinID).HorizontalCenter)
            End If
        End If
    End Sub
End Module
