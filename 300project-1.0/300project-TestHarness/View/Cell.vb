''Griff Masters
''CS 300 project
''Cell class, works with View
''
''Each cell object represents one small square in the graphics window
''each one knows nothing beyond its location and proximity to mines


Public Class Cell
    Dim color As Brush
    Dim distance As Double  ''encoded, combined distances to all mines on the board
    Dim dimension As Integer

    Public Property UL As Point

    Sub New(XCoordinate As Integer, YCoordinate As Integer, SideLength As Integer)

        UL = New Point(XCoordinate, YCoordinate)  ''the upper left corner of the cell
        distance = 0
        dimension = SideLength

    End Sub

    ''store a value that represents the distance to local mines from 0(way far from mines) to 1(right on top of a mine)
    Sub CalculateDistance(ByRef MineField As Point())

        Dim XDist As Integer = 0
        Dim YDist As Integer = 0

        distance = 0

        For M As Integer = 0 To MineField.GetUpperBound(0)
            XDist = Math.Abs(MineField(M).X - UL.X)
            YDist = Math.Abs(MineField(M).Y - UL.Y)

            ''modifying this equation will almost always break how pollution spreads on the game map
            distance = 1 / (Math.Sqrt(XDist ^ 2 + YDist ^ 2) + 1) + distance
        Next
        distance = Math.Min(distance, 1)

    End Sub

    ''color yourself in, shift to red proportional to the polution index (mapping from pollution index to pollution argument in 
    ''Draw method is enocded in View:DrawMap
    Sub Draw(CGR As Graphics, pollution As Integer)


        Dim colorCoefficient As Double = Math.Min(1, pollution * distance)

        Dim shade As New SolidBrush(Drawing.Color.FromArgb(255 * colorCoefficient, 255 - 255 * colorCoefficient, 0))

        CGR.FillRectangle(shade, UL.X, UL.Y, dimension, dimension)


    End Sub


End Class
