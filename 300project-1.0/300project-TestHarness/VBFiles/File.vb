Option Strict On

'Author: Jong Seong Lee
'Version: 1.1
'Date: 08/19/13
'Interface: Controller
'File class is a simple container of objects that are going to be saved.
<System.Serializable()>
Public Class File
    Dim m_world As world
    Dim m_budget As budget_manager
    Dim m_points() As Point

    Public ReadOnly Property World As world
        Get
            World = m_world
        End Get
    End Property

    Public ReadOnly Property BudgetManager As budget_manager
        Get
            BudgetManager = m_budget
        End Get
    End Property

    Public ReadOnly Property PointArray As Point()
        Get
            PointArray = m_points
        End Get
    End Property

    'Simply accepts world and budget object
    Sub New(ByVal worldState As world, ByVal budgetState As budget_manager, ByVal points() As Point)
        m_world = New world(worldState)
        m_budget = New budget_manager(budgetState)

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '''''''''modification'''''''''''''''''''''''''''''''''''

        ''points.

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


        m_points = points
    End Sub

End Class