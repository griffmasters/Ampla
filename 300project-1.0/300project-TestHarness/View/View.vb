Imports System.Drawing.Drawing2D

''Griff Masters
''CS 300 project
''View system
''
''Displays numarical data about the game-state in an aesthetically pleasing manner


<System.Serializable()>
Public Class View
    Dim population As Integer   ''
    Dim income As Integer       ''world-state information.  Not all of this is actually used
    Dim food As Integer         ''
    Dim environment As Integer  ''

    Dim window As Rectangle
    Dim CellSide As Integer
    Dim outline As Pen
    Dim PollutionCoefficient As Integer  ''tied to environment indicator
    Dim random_generator As Random

    Public mines As Point() ''points stored are the upper left corner of each mine

    <NonSerialized()>
    Dim cells As Map


    ''set the location and size when creating the object because they will not change through the game without screwing up calculations
    Sub New(ByRef graphicSize As Size, ByRef graphicorigin As Point, difficulty As Integer, CellSideLength As Integer)
        outline = New Pen(Brushes.Black, 1)
        window = New Rectangle(graphicorigin, graphicSize)

        random_generator = New Random()

        ''map difficulty to number of mines
        Dim NumberOfMines As Integer = difficulty + 2

        ReDim mines(NumberOfMines)   ''set the size of the mines array to the modified number passed in by difficulty

        CellSide = CellSideLength
        For M As Integer = 0 To NumberOfMines  ''fill up the mine array with random points


            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ''this line defines the range of acceptable values for a mine's location'''
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            mines(M) = New Point(Math.Floor((graphicSize.Width() / CellSide) * random_generator.NextDouble()) * CellSide + graphicorigin.X, Math.Floor((graphicSize.Height() / CellSide) * random_generator.NextDouble()) * CellSide + graphicorigin.Y)

        Next


        cells = New Map(CellSide, graphicSize.Height, graphicorigin, graphicSize.Width)
        cells.CalculateDistances(mines)

    End Sub

    ''draw the map with the graphics given (with the specs given at object creation
    Sub drawGraphic(ByRef panelGraphic As Graphics, pollution As Integer)


        ''map environmental index (60bad-140good, starts at 100) to useable pollution coefficient (5 good-60 bad)
        Dim EI As Integer = Math.Max(1, 65 - (pollution / 2))

        ''uncomment to include a frame
        ''panelGraphic.DrawRectangle(outline, window)


        ''draw cells
        cells.Draw(panelGraphic, EI)


        ''draw mines
        For M As Integer = 0 To (mines.GetUpperBound(0))
            panelGraphic.FillRectangle(Brushes.Black, mines(M).X, mines(M).Y, CellSide, CellSide)
        Next

    End Sub
    ''read in a world state and save it in the view object
    Sub takeResult(ByRef worldObj As world)

        population = worldObj.population_indicator
        income = worldObj.income_indicator
        food = worldObj.food_indicator
        environment = worldObj.environment_indicator

    End Sub

    '''''''end-game splash graphic in window (call drawgraph first to make final graphic as accurate as possible
    Sub DrawLastRound(ByRef panelGraphic As Graphics, graphicOrigin As Point, graphicSize As Size)

        Dim window As Rectangle = New Rectangle(graphicOrigin, graphicSize)
        panelGraphic.DrawRectangle(outline, window)

        Dim e As New PaintEventArgs(panelGraphic, window)
        Dim path As New GraphicsPath()

        Dim center As New Point(graphicOrigin.X + graphicSize.Width / 2, graphicOrigin.Y + graphicSize.Height - graphicSize.Width / 2)
        Dim quarter As New Size(graphicSize.Width, graphicSize.Width)
        Dim OrbitalView As New Rectangle(center, quarter)

        Dim StarSize As Integer = 3


        path.AddRectangle(window)
        path.AddPie(OrbitalView, 180, 90)
        e.Graphics.FillPath(Brushes.Black, path)
        panelGraphic.SetClip(path)
        For stars As Integer = 0 To 500  ''500 stars total (including those obscured by the indicators frame)


            panelGraphic.FillRectangle(Brushes.White, random_generator.Next(graphicSize.Width) + graphicOrigin.X, random_generator.Next(graphicSize.Height) + graphicOrigin.Y, random_generator.Next(StarSize + 1), random_generator.Next(StarSize + 1))

        Next

        panelGraphic.FillRectangle(Brushes.White, graphicOrigin.X + 20, graphicOrigin.Y + 20, 150, 150)
        Dim exampleFont As New Font("Ariel", 12)
        panelGraphic.DrawString("Final indicators:" & vbCrLf & vbCrLf & "population: " & population & vbCrLf & "income: " & income & vbCrLf & "food: " & food & vbCrLf & "environment: " & environment, exampleFont, Brushes.Black, graphicOrigin.X + 20, graphicOrigin.Y + 20)



    End Sub

    ''pop-up window with budget allocation history throughout game
    Sub drawChart(history As budget_manager)

        '''''''''''''''''''''''budget chart''''''''''''''''''''

        Dim rounds As Integer = history.budgets.GetUpperBound(0)

        Dim agriculture As Double()
        Dim science As Double()
        Dim education As Double()
        Dim pollution As Double()
        Dim industry As Double()

        ReDim agriculture(rounds - 1)
        ReDim science(rounds - 1)
        ReDim education(rounds - 1)
        ReDim pollution(rounds - 1)
        ReDim industry(rounds - 1)


        For T As Integer = 0 To history.budgets.GetLength(0) - 2
            agriculture(T) = history.budgets(T).ag
            science(T) = history.budgets(T).sci
            education(T) = history.budgets(T).edu
            pollution(T) = history.budgets(T).pol
            industry(T) = history.budgets(T).ind

        Next


        BudgetChart.Chart1.Series(0).Points.DataBindY(pollution)
        BudgetChart.Chart1.Series(1).Points.DataBindY(agriculture)
        BudgetChart.Chart1.Series(2).Points.DataBindY(science)
        BudgetChart.Chart1.Series(3).Points.DataBindY(education)
        BudgetChart.Chart1.Series(4).Points.DataBindY(industry)




        BudgetChart.Show()



        '''''''''''''''''''''''''''''''''
    End Sub


    ''change difficulty (requires graphics info for calculating new mine locations)

    Sub ChangeDifficulty(NewDifficulty As Integer, graphicSize As Size, graphicorigin As Point)

        Dim TempMines As Point()
        ReDim TempMines(NewDifficulty + 2)

        For MineCount As Integer = 1 To Math.Min(NewDifficulty + 3, mines.Length)
            TempMines(MineCount - 1) = mines(MineCount - 1)
        Next

        If NewDifficulty + 2 > mines.Length Then
            For MoreMines As Integer = mines.Length To NewDifficulty + 2
                TempMines(MoreMines) = New Point(Math.Floor((graphicSize.Width() / CellSide) * random_generator.NextDouble()) * CellSide + graphicorigin.X, Math.Floor((graphicSize.Height() / CellSide) * random_generator.NextDouble()) * CellSide + graphicorigin.Y)

            Next

        End If

        ReDim mines(TempMines.Length - 1)
        TempMines.CopyTo(mines, 0)

        cells.CalculateDistances(mines)

    End Sub


    ''overloaded constructor for loading game from file
    Sub New(ByRef graphicSize As Size, ByRef graphicorigin As Point, difficulty As Integer, CellSideLength As Integer, mineLocations As Point())

        outline = New Pen(Brushes.Black, 1)
        window = New Rectangle(graphicorigin, graphicSize)

        CellSide = CellSideLength

        ReDim mines(mineLocations.Length - 1)
        mineLocations.CopyTo(mines, 0)

        
        random_generator = New Random()

        cells = New Map(CellSide, graphicSize.Height, graphicorigin, graphicSize.Width)
        cells.CalculateDistances(mines)



    End Sub

    ''this doesn't actually get used right now, but it may be useful later
    Private Function Get_Random()
        Dim random_num As Integer
        Randomize()
        random_num = random_generator.Next(0, 1)
        Return random_num
    End Function
End Class

