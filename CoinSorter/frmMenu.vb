Imports System.IO
Imports System.Threading
Public Class frmMenu
    Private imagesToClassify As New List(Of FileInfo)
    Private imagesToClassifyIndex As Int32

    Private imageDirectory As New ImageHandler()

    Private Sub frmMenu_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblSolenoidVoltage.Text = "SolenoidVoltage: " & TrackBarVoltage.Value
        PowerSupply.SolenoidVoltage = TrackBarVoltage.Value
        PowerSupply.SolenoidOnTime = TrackBarmSeconds.Value
        PowerSupply.MilliSecondsFromEndOfImageToSolenoid = TrackBarSolenoidDelay.Value
        Dim dir As New DirectoryInfo("F:\Rotated\HeadsWithRotation360\180\")
        imagesToClassify = dir.GetFiles.ToList
    End Sub

    Private Sub cmdBreak_Click(sender As Object, e As EventArgs) Handles cmdBreak.Click
        Dim temp As Int32 = 0
    End Sub

    Private Sub TrackBarVoltage_Scroll(sender As Object, e As EventArgs) Handles TrackBarVoltage.Scroll
        lblSolenoidVoltage.Text = "SolenoidVoltage: " & TrackBarVoltage.Value
        PowerSupply.SolenoidVoltage = TrackBarVoltage.Value
    End Sub
    Private Sub TrackBarmSeconds_Scroll(sender As Object, e As EventArgs) Handles TrackBarmSeconds.Scroll
        PowerSupply.SolenoidOnTime = TrackBarmSeconds.Value
        lblSolenoidMillisecondsOn.Text = "Solenoid Milliseconds On: " & TrackBarmSeconds.Value
    End Sub
    Private Sub TrackBarSolenoidDelay_Scroll(sender As Object, e As EventArgs) Handles TrackBarSolenoidDelay.Scroll
        PowerSupply.MilliSecondsFromEndOfImageToSolenoid = TrackBarSolenoidDelay.Value
        lblSolenoidDelay.Text = "Solenoid Delay: " & TrackBarSolenoidDelay.Value
    End Sub

    Private Sub cmdToggleSolenoid_Click(sender As Object, e As EventArgs) Handles cmdToggleSolenoid.Click
        PowerSupply.ToggleSolenoid(Now, 0)
        lblSolenoidDelay.Text = "Solenoid Delay: " & TrackBarSolenoidDelay.Value
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Timer1.Enabled = True

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Dim t As task = Coin.Classify(DigitsIPAddress, DigitsJobID, imagesToClassify(imagesToClassifyIndex).FullName)
        imagesToClassifyIndex += 1
        If imagesToClassifyIndex = imagesToClassify.Count Then
            Timer1.Enabled = False
        End If



    End Sub
End Class
