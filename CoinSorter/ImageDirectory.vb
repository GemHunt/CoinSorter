Imports System.IO
Imports CoinTracker
Public Class ImageHandler
    Private currentDirectoryName As String
    Private archivedDirectoryName As String
    Private currentDirectoryWatcher As FileSystemWatcher
    Public Sub New(currentDir As String, archiveddir As String)
        currentDirectoryName = currentDir
        archivedDirectoryName = archiveddir
        If Not Directory.Exists(currentDirectoryName) Then
            Directory.CreateDirectory(currentDirectoryName)
        End If
        If Not Directory.Exists(archivedDirectoryName) Then
            Directory.CreateDirectory(archivedDirectoryName)
        End If
        currentDirectoryWatcher = New FileSystemWatcher(currentDirectoryName)
        currentDirectoryWatcher.EnableRaisingEvents = True
        AddHandler currentDirectoryWatcher.Created, AddressOf HandleNewImages
    End Sub
End Class
