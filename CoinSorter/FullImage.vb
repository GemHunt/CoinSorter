Public Class FullImage
    Public ImageID As Int64
    Public CaptureTime As DateTime = Now()

    Public Sub New(fileName As String)
        ImageID = CInt(fileName.Substring(0, 9))
        'Console.WriteLine(ImageID.ToString & "  " & Now.Second * 1000 + Now.Millisecond)
    End Sub

End Class
