Imports System.Threading
Public Class CroppedImage
    Public CroppedImageID As Int32
    Public ImageID As Int32
    Public CropID As Int32
    Public HorizontalCenter As Int32
    Public CaptureTime As DateTime
    Public FileName As String

    Public Sub New(NewFileName As String)
        FileName = NewFileName
        ImageID = CInt(NewFileName.Substring(0, 6))
        CropID = CInt(NewFileName.Substring(7, 3))
        CroppedImageID = ImageID * 1000 + CropID
        HorizontalCenter = CInt(NewFileName.Substring(12, 4))
        While Not FullImages.ContainsKey(ImageID)
            Thread.Sleep(5)
        End While
        CaptureTime = FullImages(ImageID).CaptureTime
    End Sub
End Class
