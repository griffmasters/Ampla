''Griff Masters
''CS 300 project
''Map class, works with View

''a map is primarily a big 2x2 array simulating a coordinate system for the graphics window


<System.Serializable()>
Public Class Map
    Dim MapField As Cell(,)
    Dim sl As Integer
    Dim ArrayX As Integer
    Dim ArrayY As Integer


    Sub New(SideLength As Integer, FieldHeight As Integer, Origen As Point, FieldWidth As Integer)
        sl = SideLength

        ArrayX = FieldWidth / sl
        ArrayY = FieldHeight / sl

        ReDim MapField(ArrayX, ArrayY)

        For X As Integer = 0 To ArrayX
            For Y As Integer = 0 To ArrayY
                MapField(X, Y) = New Cell(X * sl + Origen.X, Y * sl + Origen.Y, sl)

            Next
        Next



    End Sub


    ''individual cells draw themselves, all this does is instruct them to do so
    Sub Draw(MGR As Graphics, pollution As Integer)
        For X As Integer = 0 To ArrayX - 1
            For Y As Integer = 0 To ArrayY - 1

                MapField(X, Y).Draw(MGR, pollution)

            Next
        Next
    End Sub

    ''Each cell stores a number that represents how close it is to mines in the area.  This is seperate from the constructor
    ''to make it possible to change difficulty settings without creating a new map
    Sub CalculateDistances(ByRef minefield As Point())

        For X As Integer = 0 To ArrayX - 1
            For Y As Integer = 0 To ArrayY - 1
                MapField(X, Y).CalculateDistance(minefield)
            Next
        Next

    End Sub


End Class
