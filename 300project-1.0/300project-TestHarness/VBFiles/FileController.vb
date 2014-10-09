Option Strict On

Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

'Author: Jong Seong Lee
'Version: 1.1
'Date: 08/21/13
'Interface: Controller
'This class helps saving/loading a file object
Public Class FileController
    Dim m_fileDialog As FileDialog
    Dim m_lastFileName As String 'Records the last file name used
    Dim m_binaryFormatter As New BinaryFormatter()

    Public Const FILE_FILTER = "Ampla Project Save Files (*.sav)|*.sav"
    Public Const FILE_DEFAULT_EXT = "*.sav"

    'Saves a file object
    'Returns true if save was successful, false otherwise
    Public Function save(ByVal file As File) As Boolean
        Dim ret As Boolean = False
        m_fileDialog = New SaveFileDialog

        If (runDialog(m_fileDialog)) Then
            m_lastFileName = m_fileDialog.FileName 'Get the whole path
            ret = serialize(file, m_lastFileName)
            trimFileName(m_lastFileName) 'Get only file name from whole path
        End If

        Return ret
    End Function

    'Saves a file object to the destination specified by a user
    'Returns true if no error happens, false otherwise
    Private Function serialize(ByVal file As File, ByVal filePath As String) As Boolean
        Dim ret As Boolean = False

        If (Not file Is Nothing) And (Not file.BudgetManager Is Nothing) _
            And (Not file.World Is Nothing) And (file.PointArray.Length > 0) And (Not String.IsNullOrEmpty(filePath)) Then

            Dim fs As FileStream = New FileStream(filePath, FileMode.OpenOrCreate)

            Try
                m_binaryFormatter.Serialize(fs, file)
                ret = True
            Catch ex As Exception
            Finally
                fs.Close() 'Close File
            End Try
        End If

        Return ret
    End Function

    'Load a file object
    'Returns null if loading was not successful, or returns file if successful
    Public Function load() As File
        Dim ret As File = Nothing
        Dim check As Boolean = False
        m_fileDialog = New OpenFileDialog

        While check = False 'While loop makes this program keep asking a user to load a file if failed to load
            Dim result As Boolean = runDialog(m_fileDialog)

            If (result = True) Then 'If a user selected file from a load file dialog
                m_lastFileName = m_fileDialog.FileName
                ret = deserialize(m_lastFileName)

                If (Not ret Is Nothing) Then 'If loading is successful
                    check = True 'End while loop
                    m_lastFileName = trimFileName(m_lastFileName) 'Get rid of directories and slashes
                End If
            Else
                check = True 'If user cancels, end while loop
            End If

        End While

        Return ret
    End Function

    'Load a file object from a specified destination
    'Return a file object if successful, nothing otherwise
    Private Function deserialize(ByVal filePath As String) As File
        Dim ret As File
        Dim fs As New FileStream(filePath, FileMode.Open)

        Try 'Check for any failure in the middle of loading process
            ret = CType(m_binaryFormatter.Deserialize(fs), File)
        Catch ex As Exception
            MsgBox("The file deserialization failed. Please choose another file.")
            ret = Nothing
        Finally
            fs.Close() 'Close file
        End Try

        Return ret
    End Function

    'Trims the directories from a path string
    'Takes a whole directory path including file name at the end as a parameter
    'Returns file name if successful, empty string otherwise
    Private Function trimFileName(ByVal lastFilePath As String) As String
        Dim ret As String = String.Empty

        If (Not String.IsNullOrEmpty(lastFilePath)) Then 'If it is not an empty string
            ret = Path.GetFileName(lastFilePath)
        End If

        Return ret
    End Function

    'Runs file dialog window, this function is designed to be used in load and save functions as a common part
    'Returns true if dialog was successful, false otherwise
    Private Function runDialog(ByVal dialog As FileDialog) As Boolean
        Dim ret As Boolean

        dialog.Filter = FILE_FILTER
        dialog.DefaultExt = FILE_DEFAULT_EXT

        If (Not IsNothing(m_lastFileName)) Then 'If there is already default name saved, bring it to the file dialog
            dialog.FileName = m_lastFileName
        End If

        If (dialog.ShowDialog() = System.Windows.Forms.DialogResult.OK) And (dialog.FileName.Length > 0) Then
            ret = True 'If directory path is successfully obtained, this function was successful
        End If

        Return ret
    End Function
End Class
