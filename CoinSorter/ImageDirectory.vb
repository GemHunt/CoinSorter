Imports System.IO
Imports CoinTracker
Public Class ImageHandler
    Private currentDirectoryWatcher As FileSystemWatcher

    Public Sub New()
        If Not Directory.Exists(CurrentDirectory) Then
            Directory.CreateDirectory(CurrentDirectory)
        End If
        If Not Directory.Exists(ArchivedDirectory) Then
            Directory.CreateDirectory(ArchivedDirectory)
        End If
        currentDirectoryWatcher = New FileSystemWatcher(CurrentDirectory)
        currentDirectoryWatcher.EnableRaisingEvents = True
        AddHandler currentDirectoryWatcher.Created, AddressOf HandleNewImages
    End Sub
End Class
