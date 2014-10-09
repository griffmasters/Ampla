
' Chris Schmitz
' CS 300
' schmit2@pdx.edu

' This is a class to keep track of the world indicators, such as food, and calculate 
' new values based on parameters such as budget allocation and difficulty level.
<System.Serializable()>
Public Class world
    ' Rates for how various budget allocations will affect indicators.
    Dim food_rates = {0.01, 0.01, 0.0, 0.0, 0.0}
    Dim income_rates = {0.0, 0.01, 0.01, 0.01, 0.0}
    Dim population_rates = {0.03, 0.01, 0.0, -0.012, 0.0}
    Dim environment_rates = {-0.01, 0.0, -0.02, 0.01, 0.01}
    Dim COEFF = {food_rates, income_rates, population_rates, environment_rates}

    ' Budget statistics and random generator
    Private random_generator As New Random()
    Private RANDOM_SCALING As Double = 200
    Shared PERMITTED_BUDGET As Double = 1000

    ' Indicator variables to monitor the current world status.
    Public food_indicator As Double
    Public income_indicator As Double
    Public population_indicator As Double
    Public environment_indicator As Double

    ' Miscellaneous variables describing game state.
    Public difficulty As Integer
    Public advanced_multiplier As Integer

    ' These values are set as integers such that if an indicator drops below their value,
    ' an appropriate message is displayed.
    Public warning_cutoff As Integer
    Public failure_cutoff As Integer

    ' Randomly generated integer to increase scaling effects of model calculations
    Public max_random_effect As Double

    ' If this variable was not included, allocating 0 for a budget would cause no change to an indicator.
    ' Since it does not make sense for that to happen, this variable is included in the random effect
    ' to make the calculations more realistic.
    ' Public deterioration As Double

    ' Function to confirm coefficients are correct, not used in program.
    'Public Sub show()
    '   For Each n As Double In food_rates
    '        Console.WriteLine(Str(n))
    '   Next
    'End Sub

    ' Initialize a new world object, with optional arguments for loading game, specified difficulty, or specified coefficient multiplier
    Public Sub New(Optional ByVal game As world = Nothing, Optional ByVal difficulty As Integer = 2,
                   Optional ByVal advanced_multiplier As Integer = 1)

        ' Load from previous game if possible
        If Not IsNothing(game) Then
            food_indicator = game.food_indicator
            income_indicator = game.income_indicator
            population_indicator = game.population_indicator
            environment_indicator = game.environment_indicator

            Change_Difficulty(game.difficulty)
            Change_Multiplier(game.advanced_multiplier)

        Else
            ' Otherwise just load a new one, initializing with default indicator variables
            food_indicator = 100
            income_indicator = 100
            population_indicator = 100
            environment_indicator = 100

            Change_Difficulty(difficulty)
            Change_Multiplier(advanced_multiplier)
        End If
    End Sub


    ' Change the coefficient multipliers used in the model calculations
    Public Sub Change_Multiplier(ByVal multiplier As Integer)
        For Each rate_array In COEFF
            For i = 0 To 4
                rate_array(i) *= multiplier
            Next
        Next
    End Sub


    ' Change the difficulty level to edit the cutoffs and random effect size
    Public Sub Change_Difficulty(ByVal new_difficulty As Integer)
        Select Case new_difficulty
            Case 1  ' Easy
                difficulty = 1
                warning_cutoff = 75
                failure_cutoff = 60
                max_random_effect = 40

            Case 3  ' Hard
                difficulty = 3
                warning_cutoff = 85
                failure_cutoff = 75
                max_random_effect = 100

            Case Else   ' Normal (Default)
                difficulty = 2
                warning_cutoff = 80
                failure_cutoff = 70
                max_random_effect = 70

        End Select
    End Sub


    ' Generate a random number to determine the effect on various indicators
    Private Function Get_Random()
        Dim random_num As Integer
        Randomize()
        random_num = random_generator.Next(0, max_random_effect)
        Return random_num
    End Function


    ' Show indicators that have failed
    Public Function Show_Failure() As Boolean
        Dim failure As Boolean = False
        Dim failure_message As String = ("The following indicators have dropped below the failure cutoff of " + Str(failure_cutoff) + "." & vbCrLf)
        Dim indicator_dict As New Dictionary(Of String, Double)

        ' Map the indicator label to a value
        indicator_dict.Add("Food indicator: ", food_indicator)
        indicator_dict.Add("Income indicator: ", income_indicator)
        indicator_dict.Add("Population indicator: ", population_indicator)
        indicator_dict.Add("Environment indicator: ", environment_indicator)

        ' If the indicator value is less than the cutoff, set the failure flag and append to the
        ' failure message.
        Dim pair As KeyValuePair(Of String, Double)
        For Each pair In indicator_dict
            If pair.Value < failure_cutoff Then
                failure = True
                failure_message = failure_message & vbCrLf & (pair.Key + " " + Str(pair.Value))
            End If
        Next

        ' Display the message for each failed indicator.
        If failure Then
            MessageBox.Show(failure_message, "Failure", MessageBoxButtons.OK)
        End If

        Return failure
    End Function


    ' Same as the above function regarding failures, but uses warnings instead.
    Public Function Show_Warnings() As Boolean
        Dim warning As Boolean = False
        Dim warning_message As String = ("The following indicators have dropped below the warning cutoff of " + Str(warning_cutoff) + "." & vbCrLf)

        Dim indicator_dict As New Dictionary(Of String, Double)
        indicator_dict.Add("Food indicator: ", food_indicator)
        indicator_dict.Add("Income indicator: ", income_indicator)
        indicator_dict.Add("Population indicator: ", population_indicator)
        indicator_dict.Add("Environment indicator: ", environment_indicator)

        Dim pair As KeyValuePair(Of String, Double)
        For Each pair In indicator_dict
            If pair.Value < warning_cutoff Then
                warning = True
                warning_message = warning_message & vbCrLf & (pair.Key + " " + Str(pair.Value))
            End If
        Next

        If warning Then
            warning_message = warning_message + vbCrLf + vbCrLf + "Budget funds accordingly before failure occurs. "
            warning_message += ("(" + Str(failure_cutoff) + ")")
            MessageBox.Show(warning_message, "WARNING!", MessageBoxButtons.OK)
        End If

        Return warning
    End Function


    ' Calculate the new world state by taking a budget argument, then pass that budget
    ' to the Calculate_Indicator function, which will recalculate the new index based on
    ' the current indicator, budget allocation, and category.
    Public Sub Calculate_New_World(ByVal new_budget As budget)
        food_indicator = Calculate_Indicator(food_indicator, new_budget, "FOOD")
        income_indicator = Calculate_Indicator(income_indicator, new_budget, "INCOME")
        population_indicator = Calculate_Indicator(population_indicator, new_budget, "POPULATION")
        environment_indicator = Calculate_Indicator(environment_indicator, new_budget, "ENV")
    End Sub


    ' Calculate the new indicator based on the previous indicator, the budget allocations, and random scaling
    ' based on difficulty
    Private Function Calculate_Indicator(ByVal indicator As Double, ByVal new_budget As budget, ByVal category As String)
        Dim multiplier As Double
        Dim rand_effect As Double
        Dim rates(5) As Double

        If category = "FOOD" Then
            rates = food_rates
        ElseIf category = "INCOME" Then
            rates = income_rates
        ElseIf category = "POPULATION" Then
            rates = population_rates
        ElseIf category = "ENV" Then
            rates = environment_rates
        Else
            ' Error
            Return -1
        End If

        ' Recalculate based on budget allocations, then include a random scaling effect
        multiplier = (1 + (rates(0) * (new_budget.ag / PERMITTED_BUDGET)) +
                          (rates(1) * (new_budget.sci / PERMITTED_BUDGET)) +
                          (rates(2) * (new_budget.ind / PERMITTED_BUDGET)) +
                          (rates(3) * (new_budget.edu / PERMITTED_BUDGET)) +
                          (rates(4) * (new_budget.pol / PERMITTED_BUDGET)))

        ' Refactor randomness to include possible +- deterioration
        rand_effect = (Get_Random() - (0.4 * max_random_effect)) / RANDOM_SCALING
        multiplier += rand_effect

        Return (indicator * multiplier)
    End Function

End Class
