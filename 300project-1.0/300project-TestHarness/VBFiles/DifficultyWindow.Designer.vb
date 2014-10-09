<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DifficultyWindow
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DifficultyWindow))
        Me.bConfirm = New System.Windows.Forms.Button()
        Me.bCancel = New System.Windows.Forms.Button()
        Me.bSwitch = New System.Windows.Forms.Button()
        Me.pDisplay = New System.Windows.Forms.Panel()
        Me.pAdvanced = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'bConfirm
        '
        Me.bConfirm.BackColor = System.Drawing.SystemColors.Control
        Me.bConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bConfirm.Location = New System.Drawing.Point(220, 238)
        Me.bConfirm.Name = "bConfirm"
        Me.bConfirm.Size = New System.Drawing.Size(75, 23)
        Me.bConfirm.TabIndex = 0
        Me.bConfirm.Text = "Confirm"
        Me.bConfirm.UseVisualStyleBackColor = False
        '
        'bCancel
        '
        Me.bCancel.BackColor = System.Drawing.SystemColors.Control
        Me.bCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bCancel.Location = New System.Drawing.Point(311, 238)
        Me.bCancel.Name = "bCancel"
        Me.bCancel.Size = New System.Drawing.Size(75, 23)
        Me.bCancel.TabIndex = 1
        Me.bCancel.Text = "Cancel"
        Me.bCancel.UseVisualStyleBackColor = False
        '
        'bSwitch
        '
        Me.bSwitch.BackColor = System.Drawing.Color.Tomato
        Me.bSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bSwitch.Location = New System.Drawing.Point(24, 238)
        Me.bSwitch.Name = "bSwitch"
        Me.bSwitch.Size = New System.Drawing.Size(75, 23)
        Me.bSwitch.TabIndex = 3
        Me.bSwitch.UseVisualStyleBackColor = False
        '
        'pDisplay
        '
        Me.pDisplay.Location = New System.Drawing.Point(24, 13)
        Me.pDisplay.Name = "pDisplay"
        Me.pDisplay.Size = New System.Drawing.Size(360, 115)
        Me.pDisplay.TabIndex = 4
        '
        'pAdvanced
        '
        Me.pAdvanced.Location = New System.Drawing.Point(26, 145)
        Me.pAdvanced.Name = "pAdvanced"
        Me.pAdvanced.Size = New System.Drawing.Size(360, 77)
        Me.pAdvanced.TabIndex = 5
        '
        'DifficultyWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(411, 283)
        Me.Controls.Add(Me.pAdvanced)
        Me.Controls.Add(Me.pDisplay)
        Me.Controls.Add(Me.bSwitch)
        Me.Controls.Add(Me.bCancel)
        Me.Controls.Add(Me.bConfirm)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DifficultyWindow"
        Me.Text = "Select Difficulty"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents bConfirm As System.Windows.Forms.Button
    Friend WithEvents bCancel As System.Windows.Forms.Button
    Friend WithEvents bSwitch As System.Windows.Forms.Button
    Friend WithEvents pDisplay As System.Windows.Forms.Panel
    Friend WithEvents pAdvanced As System.Windows.Forms.Panel
End Class
