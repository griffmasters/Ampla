Option Strict On

'Author: Jong Seong Lee
'Version: 1.1
'Date: 08/21/13
'Interface: Controller
'This class allows a user to allocate budgets for each individual project
Public Class BudgetWindow
    Dim m_budget As budget 'The final result
    Dim m_textboxes() As TextBox 'This is for dynamic textboxes
    Dim m_budgetMax As Integer 'total budget allowed
    Dim m_prevBudgets() As Double 'Last budget values in double array

    'these are the names of labels for each textbox
    Dim m_arr_labelTxt() As String
    Dim m_arr_size As Integer

    Public ReadOnly Property Budget_Result As budget
        Get
            Budget_Result = m_budget
        End Get
    End Property

    'Takes a budget object containing previous information and max budget allowed per cycle
    Public Sub New(ByVal prevBudget As budget, ByVal maxBudget As Integer)
        ' This call is required by the designer.
        InitializeComponent()

        'Setup array elements
        m_arr_labelTxt = {"Agricultural Project", "Educational Project", "Industrial Project", "Pollution Control Project", "Science Project"}
        m_arr_size = m_arr_labelTxt.Length
        ReDim m_textboxes(m_arr_size)
        ReDim m_prevBudgets(m_arr_size)

        'Setup budget
        m_budgetMax = maxBudget

        'Get previous budget
        m_prevBudgets(0) = prevBudget.ag
        m_prevBudgets(1) = prevBudget.edu
        m_prevBudgets(2) = prevBudget.ind
        m_prevBudgets(3) = prevBudget.pol
        m_prevBudgets(4) = prevBudget.sci
    End Sub

    'Get how much money spent for projects
    'Returns -1 if values in textboxes are empty strings, or characters
    'Returns positive integer including 0 if they are valid, depends on user inputs
    Private Function getTotalBudgetSpent() As Integer
        Dim ret As Integer = 0

        If (checkValues() = False) Then 'If any of value is a character, empty string, or a ngative number
            ret = -1
        Else
            Dim index As Integer

            'Add up all user inputs from each textbox
            For index = 0 To m_arr_size - 1
                Try 'Checking was done above, but exception is handled here for safety
                    ret += CInt(m_textboxes(index).Text)
                Catch ex As Exception
                    ret = -1 'If anything happens, set return value -1
                    Exit For
                End Try
            Next index
        End If

        Return ret
    End Function

    'This method will provide all necessary information and textboxes printed on the window 
    Private Sub displayBudgetInterface()
        'Print max budget and remaining budget
        labelTotal_value.Text = m_budgetMax.ToString
        labelRemained_value.Text = m_budgetMax.ToString

        Dim yLoc As Integer = 60 'location of y point
        Dim xTxt As Integer = 220
        Dim ySpacing As Integer = 25

        Dim sizeTextbox As Size = New Size(80, 40)
        Dim txtboxDefault As Integer = 0

        Dim index As Integer

        For index = 0 To m_arr_size - 1

            'Setup label attributes
            Dim labels As New Label
            labels.Text = m_arr_labelTxt(index)
            labels.AutoSize = True
            labels.Location = New Point(20, yLoc)
            Me.Controls.Add(labels) 'Add to this window

            'Setup textbox attributes
            m_textboxes(index) = New TextBox
            m_textboxes(index).Size = sizeTextbox
            m_textboxes(index).Text = (txtboxDefault).ToString
            m_textboxes(index).Location = New Point(xTxt, yLoc)
            AddHandler m_textboxes(index).TextChanged, AddressOf updateUnallocBudget 'Include textchanged event handler
            Me.Controls.Add(m_textboxes(index)) 'Add to this window

            'Setup prev label attributes
            Dim prev As New Label
            prev.Text = (m_prevBudgets(index)).ToString
            prev.AutoSize = True
            prev.Location = New Point(310, yLoc)
            Me.Controls.Add(prev) 'Add to this window

            'increase y location
            yLoc += ySpacing
        Next index
    End Sub

    'This event handler will keep update the amount of unallocated budget.
    Private Sub updateUnallocBudget(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If getTotalBudgetSpent() >= 0 Then

            labelRemained_value.Text = (m_budgetMax - getTotalBudgetSpent()).ToString 'Update value

            'Changes text color depending on the value
            If (CInt(labelRemained_value.Text) < 0) Then
                labelRemained_value.ForeColor = Color.Red 'Red if negative
            Else
                labelRemained_value.ForeColor = Color.Black 'Black if positive
            End If
        End If
    End Sub

    'Check if a string variable can be changed into an integer value
    'Return true if the string can be an integer value, false otherwise
    Private Function isValidNum(ByVal checkingValue As String) As Boolean
        Dim ret As Boolean = False

        If (Not String.IsNullOrEmpty(checkingValue)) Or (IsNumeric(checkingValue)) Then
            ret = True
        End If

        Return ret
    End Function

    'Handles the cancel button click event; closes this window
    Private Sub bCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    'Checks if values are all integers
    'Returns true if they are, false otherwise
    Private Function checkValues() As Boolean
        Dim ret As Boolean = True
        Dim index As Integer = 0
        Dim result As String

        While (ret = True) And (index < m_arr_size)
            result = m_textboxes(index).Text 'Get text value from each textbox

            If isValidNum(result) = False Then
                ret = False
            End If

            index += 1
        End While

        Return ret
    End Function

    'Process the final stage with user inputs
    Private Sub bCommit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bCommit.Click
        Dim doCommit As Boolean = True

        If checkZero() = True Then 'Check if any of values in each textbox is 0
            Dim msgboxResult As MsgBoxResult = MsgBox("Unallocated budget(s) found. Would you like to continue?", MsgBoxStyle.YesNo, "Continue?")

            If (msgboxResult = Microsoft.VisualBasic.MsgBoxResult.No) Then 'Do not process commitment if they want to stay
                doCommit = False
            End If
        End If

        If doCommit = True Then
            If commitBudget() = True Then
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close() 'If successful, closes the window
            Else
                MsgBox("Please check your values!") 'If not, just print out an error message
            End If
        End If
    End Sub

    'Checks all values in textboxes and copy them to the budget object
    'This will not process if value is empty, character, negative number, or bigger than max
    'Return true if successfully allocated, false otherwise
    Private Function commitBudget() As Boolean
        Dim ret As Boolean = False
        Dim totalSpent As Integer = getTotalBudgetSpent()

        If totalSpent >= 0 And totalSpent <= m_budgetMax Then
            'Copy inputs to the budget object
            m_budget.ag = CInt(m_textboxes(0).Text)
            m_budget.edu = CInt(m_textboxes(1).Text)
            m_budget.ind = CInt(m_textboxes(2).Text)
            m_budget.pol = CInt(m_textboxes(3).Text)
            m_budget.sci = CInt(m_textboxes(4).Text)

            ret = True
        End If

        Return ret
    End Function

    'Checks if there is any 0 found in user inputs
    'Return true if there is any 0 in textbox values, false otherwise
    Private Function checkZero() As Boolean
        Dim ret As Boolean = False
        Dim index As Integer

        For index = 0 To m_arr_size - 1
            Dim result As String = m_textboxes(index).Text

            If (isValidNum(result) = True) Then
                Try
                    Dim value As Integer = CInt(m_textboxes(index).Text)

                    If (value = 0) Then
                        ret = True
                    End If
                Catch ex As Exception
                    ret = False
                    Exit For
                End Try
            End If
        Next index

        Return ret
    End Function

    'Resets all values in textboxes to 0
    Private Sub bReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bReset.Click
        Dim index As Integer

        For index = 0 To m_arr_size - 1
            m_textboxes(index).Text = (0).ToString 'Set to 0 here
        Next index
    End Sub

    Private Sub BudgetWindow_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        displayBudgetInterface() 'Display user interface
    End Sub
End Class