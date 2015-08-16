Public Class CroppedImageDirectory
    Inherits Dictionary(Of Int64, CroppedImage)
    Public Overloads Sub Add(croppedImage As CroppedImage)
        If Not Me.ContainsKey(croppedImage.ImageID) Then
            Me.Add(croppedImage.CroppedImageID, croppedImage)
        End If
    End Sub
End Class
