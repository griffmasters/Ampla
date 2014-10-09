
' Chris Schmitz
' CS 300
' schmit2@pdx.edu

' This class manages a collection of budget allocations per project.
' ag = agriculture, sci = scientific, ind = industry, edu = education, pol = pollution

<System.Serializable()>
Public Structure budget
    Public ag As Double
    Public sci As Double
    Public ind As Double
    Public edu As Double
    Public pol As Double
End Structure

<System.Serializable()>
Public Class budget_manager
    ' Variables related to budget and game management
    Public BUDGET_PER_CYCLE As Integer = 1000
    Private MAX_BUDGET As Integer = 10 * BUDGET_PER_CYCLE
    Private NUM_TURNS As Integer = 10

    ' Variables related to the current budget and game state
    Public remaining_funds As Integer = MAX_BUDGET
    Public current_turn As Integer = 1

    ' Array of budget instances to represent number of turns.
    Public budgets(NUM_TURNS) As budget

    ' Initialize a new budget manager object, with the option of loading with a previous budget_manager object.
    Public Sub New(Optional ByVal saved_budget As budget_manager = Nothing)
        If Not IsNothing(saved_budget) Then
            ' Initialize from loaded game.
            For i = 0 To NUM_TURNS - 1
                budgets(i).ag = saved_budget.budgets(i).ag
                budgets(i).sci = saved_budget.budgets(i).sci
                budgets(i).ind = saved_budget.budgets(i).ind
                budgets(i).edu = saved_budget.budgets(i).edu
                budgets(i).pol = saved_budget.budgets(i).pol

                current_turn = saved_budget.current_turn
                remaining_funds = saved_budget.remaining_funds
            Next
        Else
            ' Initialize new game.
            For i = 0 To NUM_TURNS - 1
                budgets(i).ag = 0
                budgets(i).sci = 0
                budgets(i).ind = 0
                budgets(i).edu = 0
                budgets(i).pol = 0
            Next
        End If

    End Sub


    ' Returns a budget Structure described above
    Public Function Get_Previous_Budget() As budget
        ' Return the previous budget allocation if not on the first turn, otherwise return an empty budget.
        If current_turn > 1 Then
            Return budgets(current_turn - 2)
        Else
            Dim empty_budget As budget
            empty_budget.ag = empty_budget.sci = empty_budget.ind = empty_budget.edu = empty_budget.pol = 0
            Return empty_budget
        End If
    End Function


    ' Pass the game object and budget structure as arguments, returns an integer ( 0 = success, 1 = not enough funds, 2 = game over)
    Public Function Submit_Budget(ByRef game As world, ByVal new_budget As budget) As Integer
        ' Submit the new budget for calculating a new world state, if there are sufficient remaining funds,
        ' and the game is not over.
        Dim sum As Integer
        sum = new_budget.ag + new_budget.sci + new_budget.ind + new_budget.edu + new_budget.pol

        If sum > remaining_funds Then    ' Overbudget, return error
            Return 1
        End If

        Dim i As Integer = current_turn - 1
        budgets(i).ag = new_budget.ag
        budgets(i).sci = new_budget.sci
        budgets(i).ind = new_budget.ind
        budgets(i).edu = new_budget.edu
        budgets(i).pol = new_budget.pol

        game.Calculate_New_World(new_budget)
        ' Subtract the allocated funds and increment the current turn after successful calculation.
        remaining_funds -= sum
        current_turn += 1

        ' Game over
        If current_turn > NUM_TURNS Then
            Return 2
        End If

        Return 0
    End Function

    ' Calculate funds remaining based on previous budget allocations.
    Public Function Calculate_Remaining_Funds()
        Dim remaining As Integer = MAX_BUDGET

        For i = 0 To NUM_TURNS - 1
            remaining -= budgets(i).ag
            remaining -= budgets(i).sci
            remaining -= budgets(i).ind
            remaining -= budgets(i).edu
            remaining -= budgets(i).pol
        Next

        Return remaining
    End Function

End Class

