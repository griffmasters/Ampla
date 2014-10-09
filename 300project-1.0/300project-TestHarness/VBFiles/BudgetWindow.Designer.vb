<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BudgetWindow
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BudgetWindow))
        Me.labelTotal = New System.Windows.Forms.Label()
        Me.labelRemained = New System.Windows.Forms.Label()
        Me.labelHeader_type = New System.Windows.Forms.Label()
        Me.labelHeader_alloc = New System.Windows.Forms.Label()
        Me.labelHeader_prev = New System.Windows.Forms.Label()
        Me.bCancel = New System.Windows.Forms.Button()
        Me.bReset = New System.Windows.Forms.Button()
        Me.bCommit = New System.Windows.Forms.Button()
        Me.labelTotal_value = New System.Windows.Forms.Label()
        Me.labelRemained_value = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'labelTotal
        '
        Me.labelTotal.AutoSize = True
        Me.labelTotal.Location = New System.Drawing.Point(20, 18)
        Me.labelTotal.Name = "labelTotal"
        Me.labelTotal.Size = New System.Drawing.Size(80, 12)
        Me.labelTotal.TabIndex = 4
        Me.labelTotal.Text = "Total Budget:"
        '
        'labelRemained
        '
        Me.labelRemained.AutoSize = True
        Me.labelRemained.Location = New System.Drawing.Point(184, 18)
        Me.labelRemained.Name = "labelRemained"
        Me.labelRemained.Size = New System.Drawing.Size(109, 12)
        Me.labelRemained.TabIndex = 5
        Me.labelRemained.Text = "Budget Remained:"
        '
        'labelHeader_type
        '
        Me.labelHeader_type.AutoSize = True
        Me.labelHeader_type.Location = New System.Drawing.Point(20, 41)
        Me.labelHeader_type.Name = "labelHeader_type"
        Me.labelHeader_type.Size = New System.Drawing.Size(77, 12)
        Me.labelHeader_type.TabIndex = 6
        Me.labelHeader_type.Text = "Budget Type"
        '
        'labelHeader_alloc
        '
        Me.labelHeader_alloc.AutoSize = True
        Me.labelHeader_alloc.Location = New System.Drawing.Point(220, 41)
        Me.labelHeader_alloc.Name = "labelHeader_alloc"
        Me.labelHeader_alloc.Size = New System.Drawing.Size(60, 12)
        Me.labelHeader_alloc.TabIndex = 7
        Me.labelHeader_alloc.Text = "Allocation"
        '
        'labelHeader_prev
        '
        Me.labelHeader_prev.AutoSize = True
        Me.labelHeader_prev.Location = New System.Drawing.Point(306, 41)
        Me.labelHeader_prev.Name = "labelHeader_prev"
        Me.labelHeader_prev.Size = New System.Drawing.Size(30, 12)
        Me.labelHeader_prev.TabIndex = 8
        Me.labelHeader_prev.Text = "Prev"
        '
        'bCancel
        '
        Me.bCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bCancel.Location = New System.Drawing.Point(314, 371)
        Me.bCancel.Name = "bCancel"
        Me.bCancel.Size = New System.Drawing.Size(75, 23)
        Me.bCancel.TabIndex = 11
        Me.bCancel.Text = "Cancel"
        Me.bCancel.UseVisualStyleBackColor = True
        '
        'bReset
        '
        Me.bReset.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.bReset.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bReset.Location = New System.Drawing.Point(108, 371)
        Me.bReset.Name = "bReset"
        Me.bReset.Size = New System.Drawing.Size(75, 23)
        Me.bReset.TabIndex = 10
        Me.bReset.Text = "Reset"
        Me.bReset.UseVisualStyleBackColor = False
        '
        'bCommit
        '
        Me.bCommit.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.bCommit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bCommit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bCommit.Location = New System.Drawing.Point(20, 371)
        Me.bCommit.Name = "bCommit"
        Me.bCommit.Size = New System.Drawing.Size(75, 23)
        Me.bCommit.TabIndex = 9
        Me.bCommit.Text = "Commit"
        Me.bCommit.UseVisualStyleBackColor = False
        '
        'labelTotal_value
        '
        Me.labelTotal_value.AutoSize = True
        Me.labelTotal_value.Location = New System.Drawing.Point(106, 18)
        Me.labelTotal_value.Name = "labelTotal_value"
        Me.labelTotal_value.Size = New System.Drawing.Size(11, 12)
        Me.labelTotal_value.TabIndex = 12
        Me.labelTotal_value.Text = "0"
        '
        'labelRemained_value
        '
        Me.labelRemained_value.AutoSize = True
        Me.labelRemained_value.Location = New System.Drawing.Point(299, 18)
        Me.labelRemained_value.Name = "labelRemained_value"
        Me.labelRemained_value.Size = New System.Drawing.Size(11, 12)
        Me.labelRemained_value.TabIndex = 13
        Me.labelRemained_value.Text = "0"
        '
        'BudgetWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(416, 416)
        Me.Controls.Add(Me.labelRemained_value)
        Me.Controls.Add(Me.labelTotal_value)
        Me.Controls.Add(Me.bCancel)
        Me.Controls.Add(Me.bReset)
        Me.Controls.Add(Me.bCommit)
        Me.Controls.Add(Me.labelHeader_prev)
        Me.Controls.Add(Me.labelHeader_alloc)
        Me.Controls.Add(Me.labelHeader_type)
        Me.Controls.Add(Me.labelRemained)
        Me.Controls.Add(Me.labelTotal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "BudgetWindow"
        Me.Text = "Budget Allocation"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents labelTotal As System.Windows.Forms.Label
    Friend WithEvents labelRemained As System.Windows.Forms.Label
    Friend WithEvents labelHeader_type As System.Windows.Forms.Label
    Friend WithEvents labelHeader_alloc As System.Windows.Forms.Label
    Friend WithEvents labelHeader_prev As System.Windows.Forms.Label
    Friend WithEvents bCancel As System.Windows.Forms.Button
    Friend WithEvents bReset As System.Windows.Forms.Button
    Friend WithEvents bCommit As System.Windows.Forms.Button
    Friend WithEvents labelTotal_value As System.Windows.Forms.Label
    Friend WithEvents labelRemained_value As System.Windows.Forms.Label
End Class
