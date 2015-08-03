Public Class FullImageDirectory
    Inherits Dictionary(Of Int32, FullImage)
    Public Overloads Sub Add(fullImage As FullImage)
        If Not Me.ContainsKey(fullImage.ImageID) Then
            Me.Add(fullImage.ImageID, fullImage)
        End If
    End Sub
End Class
