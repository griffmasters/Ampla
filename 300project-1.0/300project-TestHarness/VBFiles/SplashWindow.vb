Option Strict On

'Author: Jong Seong Lee
'Version: 1.0
'Date: 08/18/13
'Interface: Controller
'This is main windows application that will manipulate other subsystems of controller and two other interfaces, model and view.
Public Class SplashWindow
    'System related attributes
    Dim m_fileContrller As FileController 'Helps saving and loading
    Dim m_panel As Panel 'For background image and clearing
    Dim m_panelGraphic As Graphics 'Graphic area for view
    Dim m_view As View 'Graphic interface
    Dim m_budgetManager As budget_manager 'Budget handler
    Dim m_world As world 'Planet status
    Dim m_color As Color 'Color used to clear graphic space

    'Tracking attributes
    Dim m_isOver As Boolean 'Determines if the game has ended
    Dim m_hasBegun As Boolean 'Determines if the game has ever begun
    Dim m_saved As Boolean 'Determines if the game state is saved, becomes false after budget alloc
    Dim m_envGraphic As Boolean 'Determines if env graphic is on
    Dim m_isClosed As Boolean 'Determines if this application is closed (testing purpose)

    'Consts - System related
    Private Const ITERATION_MAX = 10 'Number of rounds

    'Consts - Dimension related
    Private Const GRAPHIC_SIZE_X As Integer = 734
    Private Const GRAPHIC_SIZE_Y As Integer = 449
    Private Const GRAPHIC_LOC_X As Integer = 0
    Private Const GRAPHIC_LOC_Y As Integer = 0
    Private Const PANEL_LOC_X As Integer = 12
    Private Const PANEL_LOC_Y As Integer = 18

    'Const - Graphic Related
    Private Const INTRO_IMG_PATH = "\imgs\graphic_intro.jpg" 'Welcome graphic path
    Private Const CELL_SIZE_LENGTH As Integer = 2

    'Does not take any parameters, Initializes member attributes
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        'member Initializations
        m_fileContrller = New FileController
        m_panel = New Panel()
        m_color = Color.White
        m_isOver = True
        m_hasBegun = False
        m_saved = True
        m_envGraphic = False
        m_budgetManager = Nothing
        m_world = Nothing
        m_view = Nothing
        m_isClosed = False

        'Setup panel attributes
        m_panel.Size = New Size(GRAPHIC_SIZE_X, GRAPHIC_SIZE_Y)
        m_panel.Location = New Point(PANEL_LOC_X, PANEL_LOC_Y)
        m_panelGraphic = Me.CreateGraphics() 'Setup graphics variable''''''''''''''''''''''changed for testing

    End Sub

    'Handles a click event of load button, runs difficulty setting if loadgame is successful
    Private Sub bLoadGame_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bLoadGame.Click
        If loadGame() = True And m_isOver = False Then
            If (difficultySetting() = True) Then
                MsgBox("Your difficulty was changed successfully!")
            Else
                MsgBox("Your difficulty has not changed!")
            End If
        End If
    End Sub

    'Opens up a difficulty setting window and setup difficulty
    Private Function difficultySetting() As Boolean
        Dim ret As Boolean = False

        'Asks a user if he/she wants to change difficulty setting
        Dim result As MsgBoxResult = MsgBox("Would you like to change the difficulty of your game?", MsgBoxStyle.YesNo, "Change Difficulty?")

        'If a user wants to change the difficulty
        If (result = MsgBoxResult.Yes) Then

            Dim difficultyWindow As New DifficultyWindow
            Dim diffcultyResult As DialogResult = difficultyWindow.ShowDialog() 'Runs DifficultyWindow application

            'If a user confirmed the new difficulty
            If (diffcultyResult = Windows.Forms.DialogResult.OK) Then

                If (difficultyWindow.Difficulty > 0) Then 'If successfully optained the result

                    Dim difficulty As Integer = difficultyWindow.Difficulty 'Get the difficulty result
                    m_world.Change_Difficulty(difficulty) 'The world object accepts the new difficulty
                    m_view.ChangeDifficulty(difficulty, New Size(GRAPHIC_SIZE_X, GRAPHIC_SIZE_Y), New Point(GRAPHIC_LOC_X, GRAPHIC_LOC_Y)) 'Change difficulty of view

                    If (difficultyWindow.Is_Advanced = True) Then 'If a user wanted advanced setting
                        m_world.Change_Multiplier(difficultyWindow.Advanced)
                    End If

                    ret = True
                End If
            End If
        End If
        bringGraphic()
        Return ret
    End Function

    'Load a file to obtain values in a previous game
    'Return true if loading was successful, false otherwise
    Private Function loadGame() As Boolean
        'To follow the use case provided, it will be asking the question for saving after a user selects a file to load.
        'New game, save game, and exit game process will be asking for save first before their action.

        Dim ret As Boolean = False
        Dim whileCheck As Boolean = False

        While whileCheck = False
            Dim file As File = m_fileContrller.load()

            'It seems like visual basic checks all "And conditions" for if statement even though one of them fails
            'So many of the methods using if statement looks a bit ugly, otherwise it brings unexpected behaviors

            If Not file Is Nothing Then 'If file is selected from dialog
                If saveChanges() = True Then 'If user did not cancel the save changes dialog
                    Try 'This will examine if file is corrupted or invalid.

                        'Initialize model objects
                        m_world = New world(file.World)
                        m_budgetManager = New budget_manager(file.BudgetManager)
                        m_view = New View(New Size(GRAPHIC_SIZE_X, GRAPHIC_SIZE_Y), New Point(GRAPHIC_LOC_X, GRAPHIC_LOC_Y), file.PointArray.Length - 3, CELL_SIZE_LENGTH, file.PointArray)

                        '''''''''''''''''''''''''''''''''''''''changed above line''''''''''''''''''''''''''''


                        'Setup checker variables
                        loadSetupCheckers()
                        iterUpdate()

                        'Setup view object and prepare for the display
                        m_view.takeResult(m_world) 'Send the result to the view object
                        displayClear() 'Clear out graphic area

                        ''''''''''''''''''''''''''''''''''''removed bring graphic until difficulty is set''''''''''''''''''

                        ret = True
                        whileCheck = True
                        MsgBox("File load was successful!")

                    Catch ex As Exception
                        MsgBox("File must be corrupted or invalid. Please choose another file!")
                    End Try
                Else
                    whileCheck = True
                End If
            Else
                whileCheck = True
            End If
        End While

        If ret = False Then 'If load was failed, print an error message
            MsgBox("File load has failed!")
        End If

        Return ret
    End Function

    'This will setup some tracking member attributes when loading game is successful
    Private Sub loadSetupCheckers()
        m_saved = True 'Not going to ask for saving right away
        m_hasBegun = True 'User were able to save once the game has begun

        Dim iter As Integer = m_budgetManager.current_turn

        If (iter >= 0) And (iter <= ITERATION_MAX) Then 'Determines if game is over depends on the iteration value
            m_isOver = False
        Else
            m_isOver = True
        End If
    End Sub

    'Clears graphic area
    Private Sub displayClear()
        m_panel.BackgroundImage = Nothing
        m_panelGraphic.Clear(m_color)
    End Sub

    'Handles the save button click event
    Private Sub bSaveGame_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bSaveGame.Click
        saveGame()
    End Sub

    'Saves the current state
    'Returns true if successful, false otherwise
    Private Function saveGame() As Boolean
        Dim ret As Boolean = False

        If (m_hasBegun = False) Then 'If game has not started yet
            MsgBox("Please start a game before you save!")
        Else
            ''''''''''enter an array into ExportMines?'''''''''
            Dim file As File = New File(m_world, m_budgetManager, m_view.mines) 'Create a new file

            If (m_fileContrller.save(file) = True) Then 'Save file here
                m_saved = True
                ret = True
                MsgBox("File Save was successful!")
            Else
                MsgBox("File Save has failed!")
            End If
        End If

        Return ret
    End Function

    'Handles event of the exit game button
    Private Sub bExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bExit.Click
        exitGame()
    End Sub

    'Exit game, also asks if a user wants to save the current state if not saved
    Public Sub exitGame()
        If (saveChanges() = True) Then
            m_isClosed = True
            Me.Close()
        End If
    End Sub

    'Asks a user if he/she wants to save current state of the game
    'Returns false if user clicks on the cancel button or cancel file dialog; true otherwise
    Private Function saveChanges() As Boolean
        Dim ret As Boolean = True

        If (m_saved = False) Then 'If this game is not saved
            Select Case MsgBox("Would you like to save changes?", MsgBoxStyle.YesNoCancel, "Save Changes")
                Case MsgBoxResult.Yes
                    If (saveGame() = False) Then
                        ret = False
                    End If
                Case MsgBoxResult.Cancel
                    ret = False
            End Select
        End If

        Return ret
    End Function

    'Handles event of the bugdget allocation button
    Private Sub bBudget_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bBudget.Click
        openBudgetWindow()
    End Sub

    'Opens a new budget window and takes care of the result
    Private Function openBudgetWindow() As Boolean
        Dim ret As Boolean = False

        If (m_hasBegun = True) Then 'If a new game is created or a prev game is loaded
            'check if iteration is less than 10
            If (m_isOver = True) Then
                MsgBox("Your game has finished! Please start a new game or load a previous game")
            Else
                Dim frmBudget As New BudgetWindow(m_budgetManager.Get_Previous_Budget, m_budgetManager.BUDGET_PER_CYCLE)
                frmBudget.ShowDialog()

                If (frmBudget.DialogResult = Windows.Forms.DialogResult.OK) Then 'If a user has allocated budgets successfully
                    If (budgetResult(frmBudget.Budget_Result) = True) Then 'Result taken cared here
                        m_saved = False
                        ret = True
                    End If
                End If
            End If
        Else
            MsgBox("Please start a new game or load a previous game")
        End If

        Return ret
    End Function

    'Takes care of budget result given by a user. Let the buget calculated, send this calculation to the view and draw graphic
    'Returns true if successful, false otherwise
    Private Function budgetResult(ByVal newBudget As budget) As Boolean
        Dim ret As Boolean = False
        Dim result_budget As Integer = m_budgetManager.Submit_Budget(m_world, newBudget)
        m_world.Show_Warnings()

        If (m_world.Show_Failure()) Then
            MsgBox("Game Over! You have not successfully managed the Ampla planet. Please try another game to continue your journey.")
            m_isOver = True
        Else
            If (result_budget = 0 Or result_budget = 2) Then
                m_view.takeResult(m_world) 'Send the result to the view object
                displayClear() 'Clean up display before drawing graphic
                bringGraphic() 'Draw graph
                ret = True
            End If


            If ret = True Then
                MsgBox("Budget allocation was done successfully!")
            Else
                MsgBox("Budget allocation has failed!")
            End If
        End If

        iterUpdate()

        Return ret
    End Function

    'Handles the indicators button click event
    Private Sub bIndicators_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bIndicators.Click
        showIndicatorsWindow("The following is the current status of your planet.")
    End Sub

    'Draws a graphic based on a budget result on the splash page
    Private Sub bringGraphic()
        m_panelGraphic.Clear(m_color)
        Dim iter As Integer = m_budgetManager.current_turn

        MsgBox(iter)

        If (iter > 0) And (iter <= ITERATION_MAX + 1) Then 'Draws normal graphic
            m_view.drawGraphic(m_panelGraphic, CInt(m_world.environment_indicator))

            If (iter > ITERATION_MAX) Then ' Final round
                m_view.DrawLastRound(m_panelGraphic, New Point(GRAPHIC_LOC_X, GRAPHIC_LOC_Y), New Size(GRAPHIC_SIZE_X - 1, _
                                GRAPHIC_SIZE_Y - 1))

                m_view.drawChart(m_budgetManager)
            End If
        End If
    End Sub

    'Create a new indicator window and display it on the screen
    Private Function showIndicatorsWindow(ByVal message As String) As Boolean
        Dim ret As Boolean = False

        If (m_hasBegun = True) Then 'If this game is initialized
            Dim showIndicators As IndicatorWindow
            showIndicators = New IndicatorWindow(m_world, message)
            showIndicators.ShowDialog()
            ret = True
        Else 'If not initialized, let a user start a game first
            MsgBox("Please start a new game or load a previous game to access indicators!")
        End If

        Return ret
    End Function

    'Handles the new game button click event, asks for saving changes if the current game is not saved
    Private Sub bNewGame_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bNewGame.Click
        If (saveChanges() = True) Then
            newGame()

            '''''''''''''''''''''''''''''''''code added for testing'''''''''''''''''''''
            m_view.drawGraphic(m_panelGraphic, 100)


        End If
    End Sub

    'If a user selects an appropriate difficulty, setups up new game with the difficulty value given by a user
    Private Sub newGame()
        Dim diffWindow As New DifficultyWindow
        Dim result As DialogResult = diffWindow.ShowDialog()
        If (result = Windows.Forms.DialogResult.OK) Then 'If difficulty setup is successful
            m_world = New world(Nothing, diffWindow.Difficulty)

            If (diffWindow.Is_Advanced = True) Then
                m_world.Change_Multiplier(diffWindow.Advanced)
            End If

            'cellsizelength = 2
            m_view = New View(New Size(GRAPHIC_SIZE_X, GRAPHIC_SIZE_Y), New Point(GRAPHIC_LOC_X, GRAPHIC_LOC_Y), diffWindow.Difficulty, CELL_SIZE_LENGTH)
            m_budgetManager = New budget_manager
            m_saved = True
            m_hasBegun = True
            m_isOver = False
            displayClear()
            iterUpdate()
            MsgBox("New game created successfully!")
        Else 'If failed
            MsgBox("Failed to create a new game!")
        End If
    End Sub

    'This method will load a welcome image from a file
    Private Sub welcomeGraphic()
        m_panel.BackgroundImage = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & INTRO_IMG_PATH)
        Me.Controls.Add(m_panel)
    End Sub

    'Updates iteration index in the right corner at the top
    Private Sub iterUpdate()
        Dim iter As Integer = m_budgetManager.current_turn
        Dim displayValue As String

        If iter > ITERATION_MAX Then
            displayValue = "Finished" 'If it reached the max value, print "finished" instead of number
            m_isOver = True 'Set the game as finished state
        Else
            displayValue = (iter).ToString
        End If

        labelIteration.Text = "Budget Iteration: " + displayValue 'Update the index here
    End Sub

    Private Sub SplashWindow_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Show welcome graphic
        welcomeGraphic()
    End Sub
End Class
