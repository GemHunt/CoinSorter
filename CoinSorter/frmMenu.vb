Public Class frmMenu
    Private imageDirectory As New ImageHandler()

    Private Sub frmMenu_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblSolenoidVoltage.Text = "SolenoidVoltage: " & TrackBarVoltage.Value
        PowerSupply.SolenoidVoltage = TrackBarVoltage.Value
        PowerSupply.SolenoidOnTime = TrackBarmSeconds.Value
        PowerSupply.MilliSecondsFromEndOfImageToSolenoid = TrackBarSolenoidDelay.Value
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
        Dim fileName As String = "F:/liveview/heads\1010000021030599.jpg"
        If System.IO.File.Exists(fileName) Then
            System.IO.File.Delete(fileName)
        End If

        Dim fullImage As New FullImage("101000002.jpg")
        FullImages.Add(fullImage)
        Dim cropped As New CroppedImage("1010000021030599.jpg")
        Dim coin As New Coin(cropped)
    End Sub
End Class
