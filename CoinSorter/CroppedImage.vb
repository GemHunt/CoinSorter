Public Class CroppedImage
    Public CroppedImageID As Int32
    Public ImageID As Int32
    Public CropID As Int32
    Public HorizontalCenter As Int32
    Public CaptureTime As DateTime
    Public Sub New(fileName As String)
        ImageID = CInt(fileName.Substring(0, 6))
        CropID = CInt(fileName.Substring(7, 3))
        CroppedImageID = ImageID * 1000 + CropID
        HorizontalCenter = CInt(fileName.Substring(12, 4))
        CaptureTime = FullImages(ImageID).CaptureTime
    End Sub
End Class
