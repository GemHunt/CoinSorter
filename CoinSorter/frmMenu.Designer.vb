<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMenu
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMenu))
        Me.cmdBreak = New System.Windows.Forms.Button()
        Me.lblSolenoidVoltage = New System.Windows.Forms.Label()
        Me.TrackBarVoltage = New System.Windows.Forms.TrackBar()
        Me.cmdToggleSolenoid = New System.Windows.Forms.Button()
        Me.lblSolenoidMillisecondsOn = New System.Windows.Forms.Label()
        Me.TrackBarmSeconds = New System.Windows.Forms.TrackBar()
        Me.lblSolenoidDelay = New System.Windows.Forms.Label()
        Me.TrackBarSolenoidDelay = New System.Windows.Forms.TrackBar()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.TrackBarVoltage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBarmSeconds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBarSolenoidDelay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdBreak
        '
        Me.cmdBreak.Location = New System.Drawing.Point(135, 160)
        Me.cmdBreak.Name = "cmdBreak"
        Me.cmdBreak.Size = New System.Drawing.Size(75, 23)
        Me.cmdBreak.TabIndex = 0
        Me.cmdBreak.Text = "Break"
        Me.cmdBreak.UseVisualStyleBackColor = True
        '
        'lblSolenoidVoltage
        '
        Me.lblSolenoidVoltage.AutoSize = True
        Me.lblSolenoidVoltage.Location = New System.Drawing.Point(25, 256)
        Me.lblSolenoidVoltage.Name = "lblSolenoidVoltage"
        Me.lblSolenoidVoltage.Size = New System.Drawing.Size(87, 13)
        Me.lblSolenoidVoltage.TabIndex = 6
        Me.lblSolenoidVoltage.Text = "Solenoid Voltage"
        '
        'TrackBarVoltage
        '
        Me.TrackBarVoltage.Location = New System.Drawing.Point(25, 275)
        Me.TrackBarVoltage.Maximum = 20
        Me.TrackBarVoltage.Name = "TrackBarVoltage"
        Me.TrackBarVoltage.Size = New System.Drawing.Size(418, 45)
        Me.TrackBarVoltage.TabIndex = 5
        Me.TrackBarVoltage.Value = 12
        '
        'cmdToggleSolenoid
        '
        Me.cmdToggleSolenoid.Location = New System.Drawing.Point(239, 160)
        Me.cmdToggleSolenoid.Name = "cmdToggleSolenoid"
        Me.cmdToggleSolenoid.Size = New System.Drawing.Size(136, 23)
        Me.cmdToggleSolenoid.TabIndex = 7
        Me.cmdToggleSolenoid.Text = "Toggle Solenoid"
        Me.cmdToggleSolenoid.UseVisualStyleBackColor = True
        '
        'lblSolenoidMillisecondsOn
        '
        Me.lblSolenoidMillisecondsOn.AutoSize = True
        Me.lblSolenoidMillisecondsOn.Location = New System.Drawing.Point(25, 189)
        Me.lblSolenoidMillisecondsOn.Name = "lblSolenoidMillisecondsOn"
        Me.lblSolenoidMillisecondsOn.Size = New System.Drawing.Size(125, 13)
        Me.lblSolenoidMillisecondsOn.TabIndex = 9
        Me.lblSolenoidMillisecondsOn.Text = "Solenoid Milliseconds On"
        '
        'TrackBarmSeconds
        '
        Me.TrackBarmSeconds.Location = New System.Drawing.Point(25, 208)
        Me.TrackBarmSeconds.Maximum = 300
        Me.TrackBarmSeconds.Name = "TrackBarmSeconds"
        Me.TrackBarmSeconds.Size = New System.Drawing.Size(418, 45)
        Me.TrackBarmSeconds.TabIndex = 8
        Me.TrackBarmSeconds.TickFrequency = 10
        Me.TrackBarmSeconds.Value = 127
        '
        'lblSolenoidDelay
        '
        Me.lblSolenoidDelay.AutoSize = True
        Me.lblSolenoidDelay.Location = New System.Drawing.Point(28, 331)
        Me.lblSolenoidDelay.Name = "lblSolenoidDelay"
        Me.lblSolenoidDelay.Size = New System.Drawing.Size(78, 13)
        Me.lblSolenoidDelay.TabIndex = 11
        Me.lblSolenoidDelay.Text = "Solenoid Delay"
        '
        'TrackBarSolenoidDelay
        '
        Me.TrackBarSolenoidDelay.Location = New System.Drawing.Point(28, 350)
        Me.TrackBarSolenoidDelay.Maximum = 1200
        Me.TrackBarSolenoidDelay.Minimum = 200
        Me.TrackBarSolenoidDelay.Name = "TrackBarSolenoidDelay"
        Me.TrackBarSolenoidDelay.Size = New System.Drawing.Size(418, 45)
        Me.TrackBarSolenoidDelay.TabIndex = 10
        Me.TrackBarSolenoidDelay.TickFrequency = 5
        Me.TrackBarSolenoidDelay.Value = 770
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(12, 12)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(443, 130)
        Me.RichTextBox1.TabIndex = 12
        Me.RichTextBox1.Text = resources.GetString("RichTextBox1.Text")
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(569, 118)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(64, 71)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(697, 430)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.lblSolenoidDelay)
        Me.Controls.Add(Me.TrackBarSolenoidDelay)
        Me.Controls.Add(Me.lblSolenoidMillisecondsOn)
        Me.Controls.Add(Me.TrackBarmSeconds)
        Me.Controls.Add(Me.cmdToggleSolenoid)
        Me.Controls.Add(Me.lblSolenoidVoltage)
        Me.Controls.Add(Me.TrackBarVoltage)
        Me.Controls.Add(Me.cmdBreak)
        Me.Name = "frmMenu"
        Me.Text = "Coin Tracker"
        CType(Me.TrackBarVoltage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBarmSeconds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBarSolenoidDelay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdBreak As System.Windows.Forms.Button
    Friend WithEvents lblSolenoidVoltage As System.Windows.Forms.Label
    Friend WithEvents TrackBarVoltage As System.Windows.Forms.TrackBar
    Friend WithEvents cmdToggleSolenoid As System.Windows.Forms.Button
    Friend WithEvents lblSolenoidMillisecondsOn As System.Windows.Forms.Label
    Friend WithEvents TrackBarmSeconds As System.Windows.Forms.TrackBar
    Friend WithEvents lblSolenoidDelay As System.Windows.Forms.Label
    Friend WithEvents TrackBarSolenoidDelay As System.Windows.Forms.TrackBar
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button

End Class
