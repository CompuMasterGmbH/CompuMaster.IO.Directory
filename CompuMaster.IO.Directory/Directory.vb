Option Explicit On
Option Strict On

Namespace CompuMaster.IO

    ''' <summary>
    ''' Provides missing features for System.IO.Directory
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Directory
        Public Shared Function GetFiles(ByVal path As String, ByVal searchPattern As String, ByVal compareOption As FilterUtils.CaseSensitivity) As String()
            Return FilterUtils.ApplyFileFilter(System.IO.Directory.GetFiles(path, searchPattern), searchPattern, compareOption)
        End Function

        Public Shared Function GetFileSystemEntries(ByVal path As String, ByVal searchPattern As String, ByVal compareOption As FilterUtils.CaseSensitivity) As String()
            Return FilterUtils.ApplyFileFilter(System.IO.Directory.GetFileSystemEntries(path, searchPattern), searchPattern, compareOption)
        End Function

        Public Shared Function GetDirectories(ByVal path As String, ByVal searchPattern As String, ByVal compareOption As FilterUtils.CaseSensitivity) As String()
            Return FilterUtils.ApplyFileFilter(System.IO.Directory.GetDirectories(path, searchPattern), searchPattern, compareOption)
        End Function

        Public Shared Function GetDirectories(ByVal path As String, ByVal searchPattern As String, ByVal searchOptions As System.IO.SearchOption, ByVal compareOption As FilterUtils.CaseSensitivity) As String()
            Return FilterUtils.ApplyFileFilter(System.IO.Directory.GetDirectories(path, searchPattern, searchOptions), searchPattern, compareOption)
        End Function

    End Class

End Namespace
