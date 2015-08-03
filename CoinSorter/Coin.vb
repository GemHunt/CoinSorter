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
End Class
