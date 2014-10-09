<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SplashWindow
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SplashWindow))
        Me.bBudget = New System.Windows.Forms.Button()
        Me.bIndicators = New System.Windows.Forms.Button()
        Me.bNewGame = New System.Windows.Forms.Button()
        Me.bLoadGame = New System.Windows.Forms.Button()
        Me.bSaveGame = New System.Windows.Forms.Button()
        Me.bExit = New System.Windows.Forms.Button()
        Me.labelIteration = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'bBudget
        '
        Me.bBudget.BackColor = System.Drawing.Color.Tomato
        Me.bBudget.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bBudget.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bBudget.Location = New System.Drawing.Point(646, 52)
        Me.bBudget.Name = "bBudget"
        Me.bBudget.Size = New System.Drawing.Size(108, 25)
        Me.bBudget.TabIndex = 0
        Me.bBudget.Text = "Budget Allocation"
        Me.bBudget.UseVisualStyleBackColor = False
        '
        'bIndicators
        '
        Me.bIndicators.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.bIndicators.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bIndicators.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bIndicators.Location = New System.Drawing.Point(646, 164)
        Me.bIndicators.Name = "bIndicators"
        Me.bIndicators.Size = New System.Drawing.Size(108, 25)
        Me.bIndicators.TabIndex = 1
        Me.bIndicators.Text = "Indicators"
        Me.bIndicators.UseVisualStyleBackColor = False
        '
        'bNewGame
        '
        Me.bNewGame.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bNewGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bNewGame.Location = New System.Drawing.Point(647, 387)
        Me.bNewGame.Name = "bNewGame"
        Me.bNewGame.Size = New System.Drawing.Size(108, 25)
        Me.bNewGame.TabIndex = 2
        Me.bNewGame.Text = "New Game"
        Me.bNewGame.UseVisualStyleBackColor = False
        '
        'bLoadGame
        '
        Me.bLoadGame.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bLoadGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bLoadGame.Location = New System.Drawing.Point(647, 418)
        Me.bLoadGame.Name = "bLoadGame"
        Me.bLoadGame.Size = New System.Drawing.Size(108, 25)
        Me.bLoadGame.TabIndex = 3
        Me.bLoadGame.Text = "Load Game"
        Me.bLoadGame.UseVisualStyleBackColor = True
        '
        'bSaveGame
        '
        Me.bSaveGame.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bSaveGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bSaveGame.Location = New System.Drawing.Point(647, 450)
        Me.bSaveGame.Name = "bSaveGame"
        Me.bSaveGame.Size = New System.Drawing.Size(108, 25)
        Me.bSaveGame.TabIndex = 4
        Me.bSaveGame.Text = "Save Game"
        Me.bSaveGame.UseVisualStyleBackColor = True
        '
        'bExit
        '
        Me.bExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.bExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.bExit.Location = New System.Drawing.Point(647, 481)
        Me.bExit.Name = "bExit"
        Me.bExit.Size = New System.Drawing.Size(108, 25)
        Me.bExit.TabIndex = 5
        Me.bExit.Text = "Exit Game"
        Me.bExit.UseVisualStyleBackColor = True
        '
        'labelIteration
        '
        Me.labelIteration.AutoSize = True
        Me.labelIteration.Location = New System.Drawing.Point(645, 20)
        Me.labelIteration.Name = "labelIteration"
        Me.labelIteration.Size = New System.Drawing.Size(48, 13)
        Me.labelIteration.TabIndex = 6
        Me.labelIteration.Text = "Iteration:"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(645, 90)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 61)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Note: Graph will show the last result of budget allocation"
        '
        'SplashWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(765, 525)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.labelIteration)
        Me.Controls.Add(Me.bExit)
        Me.Controls.Add(Me.bSaveGame)
        Me.Controls.Add(Me.bLoadGame)
        Me.Controls.Add(Me.bNewGame)
        Me.Controls.Add(Me.bIndicators)
        Me.Controls.Add(Me.bBudget)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "SplashWindow"
        Me.Text = "CS300 Ampla Project"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents bBudget As System.Windows.Forms.Button
    Friend WithEvents bIndicators As System.Windows.Forms.Button
    Friend WithEvents bNewGame As System.Windows.Forms.Button
    Friend WithEvents bLoadGame As System.Windows.Forms.Button
    Friend WithEvents bSaveGame As System.Windows.Forms.Button
    Friend WithEvents bExit As System.Windows.Forms.Button
    Friend WithEvents labelIteration As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
