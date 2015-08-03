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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMenu))
        Me.cmdBreak = New System.Windows.Forms.Button()
        Me.lblSolenoidVoltage = New System.Windows.Forms.Label()
        Me.TrackBarVoltage = New System.Windows.Forms.TrackBar()
        Me.cmdToggleSolenoid = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TrackBarmSeconds = New System.Windows.Forms.TrackBar()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TrackBarSolenoidDelay = New System.Windows.Forms.TrackBar()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
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
        Me.TrackBarVoltage.Value = 10
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(25, 189)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(125, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Solenoid Milliseconds On"
        '
        'TrackBarmSeconds
        '
        Me.TrackBarmSeconds.Location = New System.Drawing.Point(25, 208)
        Me.TrackBarmSeconds.Maximum = 300
        Me.TrackBarmSeconds.Name = "TrackBarmSeconds"
        Me.TrackBarmSeconds.Size = New System.Drawing.Size(418, 45)
        Me.TrackBarmSeconds.TabIndex = 8
        Me.TrackBarmSeconds.TickFrequency = 10
        Me.TrackBarmSeconds.Value = 40
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(28, 331)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Solenoid Delay"
        '
        'TrackBarSolenoidDelay
        '
        Me.TrackBarSolenoidDelay.Location = New System.Drawing.Point(28, 350)
        Me.TrackBarSolenoidDelay.Maximum = 8000
        Me.TrackBarSolenoidDelay.Name = "TrackBarSolenoidDelay"
        Me.TrackBarSolenoidDelay.Size = New System.Drawing.Size(418, 45)
        Me.TrackBarSolenoidDelay.TabIndex = 10
        Me.TrackBarSolenoidDelay.TickFrequency = 50
        Me.TrackBarSolenoidDelay.Value = 2000
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(12, 12)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(443, 130)
        Me.RichTextBox1.TabIndex = 12
        Me.RichTextBox1.Text = resources.GetString("RichTextBox1.Text")
        '
        'frmMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(633, 430)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TrackBarSolenoidDelay)
        Me.Controls.Add(Me.Label1)
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TrackBarmSeconds As System.Windows.Forms.TrackBar
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TrackBarSolenoidDelay As System.Windows.Forms.TrackBar
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox

End Class
