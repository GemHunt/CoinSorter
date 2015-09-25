Public Class Arduino
    Public ComPortNumber As Int32 = 4
    Private comPort As IO.Ports.SerialPort

    Sub New()
        comPort = My.Computer.Ports.OpenSerialPort("COM" & ComPortNumber)
    End Sub

    Sub SendSerialData(ByVal data As String)
        comPort.WriteLine(data)
    End Sub



End Class
