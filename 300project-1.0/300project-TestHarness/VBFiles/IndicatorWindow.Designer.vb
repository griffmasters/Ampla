<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IndicatorWindow
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IndicatorWindow))
        Me.pDisplay = New System.Windows.Forms.Panel()
        Me.bClose = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'pDisplay
        '
        Me.pDisplay.Location = New System.Drawing.Point(13, 13)
        Me.pDisplay.Name = "pDisplay"
        Me.pDisplay.Size = New System.Drawing.Size(393, 231)
        Me.pDisplay.TabIndex = 0
        '
        'bClose
        '
        Me.bClose.BackColor = System.Drawing.SystemColors.Control
        Me.bClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.bClose.ForeColor = System.Drawing.Color.Black
        Me.bClose.Location = New System.Drawing.Point(170, 267)
        Me.bClose.Name = "bClose"
        Me.bClose.Size = New System.Drawing.Size(75, 23)
        Me.bClose.TabIndex = 1
        Me.bClose.Text = "Close"
        Me.bClose.UseVisualStyleBackColor = False
        '
        'IndicatorWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(418, 316)
        Me.Controls.Add(Me.bClose)
        Me.Controls.Add(Me.pDisplay)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "IndicatorWindow"
        Me.Text = "Current Planet Status"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pDisplay As System.Windows.Forms.Panel
    Friend WithEvents bClose As System.Windows.Forms.Button
End Class
