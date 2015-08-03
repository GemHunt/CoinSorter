Public Class frmMenu
    Private imageDirectory As New ImageHandler("C:/Temp/TempCoinImages", "F:/archivedCoinImages")

    Private Sub frmMenu_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblSolenoidVoltage.Text = "SolenoidVoltage: " & TrackBarVoltage.Value
        PowerSupply.SolenoidVoltage = TrackBarVoltage.Value
        PowerSupply.SolenoidOnTime = TrackBarmSeconds.Value
        PowerSupply.SolenoidDelay = TrackBarSolenoidDelay.Value
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
    End Sub
    Private Sub TrackBarSolenoidDelay_Scroll(sender As Object, e As EventArgs) Handles TrackBarSolenoidDelay.Scroll
        PowerSupply.SolenoidDelay = TrackBarSolenoidDelay.Value
    End Sub

    Private Sub cmdToggleSolenoid_Click(sender As Object, e As EventArgs) Handles cmdToggleSolenoid.Click
        PowerSupply.ToggleSolenoid(Now)
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

    End Sub
End Class
