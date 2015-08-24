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
    Public DigitsIPAddress As String = "54.144.148.83"
    Public DigitsJobID As String = "20150820-222359-cd38"

    Public CurrentDirectory As String = "C:\Temp\TempCoinImages\"
    Public ArchivedDirectory As String = "F:\liveview\"

    Public Sub HandleNewImages(sender As Object, e As IO.FileSystemEventArgs)
        Dim fi As New FileInfo(e.FullPath)
        If fi.Name.Length = 13 Then
            Dim fullImage As New FullImage(fi.Name)
            FullImages.Add(fullImage)
        End If
        If fi.Name.Length = 20 Then
            Dim croppedImage As New CroppedImage(fi.Name)
            CroppedImages.Add(croppedImage)
            Dim coinIsDup As Boolean
            Dim coinID As Int64
            Coins.AddorUpdate(croppedImage, coinIsDup, coinID)
            ''Sort out strong Lincoln memorials:
            'If Not coinIsDup And Coins(coinID).Type = "tails" And Coins(coinID).TypeStrength > 80 Then
            '    PowerSupply.ToggleSolenoid(Coins(coinID).CaptureTime, Coins(coinID).HorizontalCenter)
            'End If
        End If
    End Sub
End Module
