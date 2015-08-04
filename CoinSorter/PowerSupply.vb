Imports System.Threading

Public Class HPPowerSupply
    Private PowerSupplyOn As Boolean
    Private ioMgr As Ivi.Visa.Interop.ResourceManager
    Private powerSupply(8) As Ivi.Visa.Interop.FormattedIO488
    Private initString(8) As String
    Private address As Int32 = 7
    Public SolenoidVoltage As Double
    Public SolenoidOnTime As Int32
    Public MilliSecondsFromEndOfImageToSolenoid As Int32

    Public Sub New()
        ioMgr = New Ivi.Visa.Interop.ResourceManager
        initString(address) = "GPIB0::7::INSTR"
        powerSupply(address) = New Ivi.Visa.Interop.FormattedIO488
        Try
            powerSupply(address).IO = ioMgr.Open(initString(address))
            powerSupply(address).WriteString("iset 1, 2")
            powerSupply(address).WriteString("vset 1, 0")
            PowerSupplyOn = True
        Catch ex As Exception
            'Do nothing, probably the HP power supply was not on. 
            PowerSupplyOn = False
        End Try


    End Sub

    Public Sub ToggleSolenoid(fromTime As DateTime, horizontalCenter As Int32)
        Dim span As TimeSpan = Now() - fromTime
        Dim msProcessingDelay As Double = span.TotalMilliseconds
        Dim milliSecondsToEndOfImage As Int32 = (ImageWidth - horizontalCenter) * CoinSpeedPixelsPerMs
        Dim delay As Int32 = MilliSecondsFromEndOfImageToSolenoid + milliSecondsToEndOfImage - msProcessingDelay
        If delay <= 0 Then
            delay = 1
        End If

        Console.WriteLine("MilliSecondsFromEndOfImageToSolenoid: " & MilliSecondsFromEndOfImageToSolenoid & " msDiff: " & msProcessingDelay & "  milliSecondsToEndOfImage: " & milliSecondsToEndOfImage)
        Console.WriteLine("delay: " & delay)

        Dim SolenoidTimerOn As New System.Timers.Timer(delay)
        AddHandler SolenoidTimerOn.Elapsed, AddressOf ToggleSolenoidOn
        SolenoidTimerOn.Enabled = True
        GC.KeepAlive(SolenoidTimerOn)
        Dim SolenoidTimerOff As New System.Timers.Timer(delay + SolenoidOnTime)
        AddHandler SolenoidTimerOff.Elapsed, AddressOf ToggleSolenoidOff
        SolenoidTimerOff.Enabled = True
        GC.KeepAlive(SolenoidTimerOff)
    End Sub

    Private Sub ToggleSolenoidOn(source As Object, e As System.Timers.ElapsedEventArgs)
        Dim timer As System.Timers.Timer = source
        timer.Enabled = False
        timer.Close()
        powerSupply(address).WriteString("vset 1, " & SolenoidVoltage)
    End Sub

    Private Sub ToggleSolenoidOff(source As Object, e As System.Timers.ElapsedEventArgs)
        Dim timer As System.Timers.Timer = source
        timer.Enabled = False
        timer.Close()
        powerSupply(address).WriteString("vset 1, 0")
    End Sub
End Class
