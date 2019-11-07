Option Explicit On
Option Strict On

Namespace CompuMaster.IO

    ''' <summary>
    ''' Provides missing features for System.IO.Directory
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Directory
        Public Shared Function GetFiles(ByVal path As String, ByVal searchPattern As String, ByVal compareOption As FilterUtils.CaseSensitivity) As String()
            If searchPattern = Nothing Then searchPattern = "*"
            If IsLinuxEnvironment() Then
                Return FilterUtils.ApplyFileFilter(System.IO.Directory.GetFiles(path, "*"), searchPattern, compareOption, path)
            Else
                Return FilterUtils.ApplyFileFilter(System.IO.Directory.GetFiles(path, searchPattern), searchPattern, compareOption, path)
            End If
        End Function

        Public Shared Function GetFiles(ByVal path As String, ByVal searchPattern As String, searchOptions As System.IO.SearchOption, ByVal compareOption As FilterUtils.CaseSensitivity) As String()
            If searchPattern = Nothing Then searchPattern = "*"
            If IsLinuxEnvironment() Then
                Return FilterUtils.ApplyFileFilter(System.IO.Directory.GetFiles(path, "*", searchOptions), searchPattern, compareOption, path)
            Else
                Return FilterUtils.ApplyFileFilter(System.IO.Directory.GetFiles(path, searchPattern, searchOptions), searchPattern, compareOption, path)
            End If
        End Function

        Public Shared Function GetFileSystemEntries(ByVal path As String, ByVal searchPattern As String, ByVal compareOption As FilterUtils.CaseSensitivity) As String()
            If searchPattern = Nothing Then searchPattern = "*"
            If IsLinuxEnvironment() Then
                Return FilterUtils.ApplyFileFilter(System.IO.Directory.GetFileSystemEntries(path, "*"), searchPattern, compareOption, path)
            Else
                Return FilterUtils.ApplyFileFilter(System.IO.Directory.GetFileSystemEntries(path, searchPattern), searchPattern, compareOption, path)
            End If
        End Function

        Public Shared Function GetDirectories(ByVal path As String, ByVal searchPattern As String, ByVal compareOption As FilterUtils.CaseSensitivity) As String()
            If searchPattern = Nothing Then searchPattern = "*"
            If IsLinuxEnvironment() Then
                Return FilterUtils.ApplyFileFilter(System.IO.Directory.GetDirectories(path, "*"), searchPattern, compareOption, path)
            Else
                Return FilterUtils.ApplyFileFilter(System.IO.Directory.GetDirectories(path, searchPattern), searchPattern, compareOption, path)
            End If
        End Function

        Public Shared Function GetDirectories(ByVal path As String, ByVal searchPattern As String, ByVal searchOptions As System.IO.SearchOption, ByVal compareOption As FilterUtils.CaseSensitivity) As String()
            If searchPattern = Nothing Then searchPattern = "*"
            If IsLinuxEnvironment() Then
                Return FilterUtils.ApplyFileFilter(System.IO.Directory.GetDirectories(path, "*", searchOptions), searchPattern, compareOption, path)
            Else
                Return FilterUtils.ApplyFileFilter(System.IO.Directory.GetDirectories(path, searchPattern, searchOptions), searchPattern, compareOption, path)
            End If
        End Function

        Private Shared Function IsLinuxEnvironment() As Boolean
            Return System.Environment.OSVersion.Platform = PlatformID.MacOSX OrElse System.Environment.OSVersion.Platform = PlatformID.Unix
        End Function

    End Class

End Namespace
