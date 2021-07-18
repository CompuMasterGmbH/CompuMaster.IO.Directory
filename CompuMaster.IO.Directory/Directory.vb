Option Explicit On
Option Strict On

Namespace CompuMaster.IO

    ''' <summary>
    ''' Provides missing features for System.IO.Directory
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class Directory
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

        Public Shared Function GetFiles(ByVal path As String, ByVal searchPattern As String(), ByVal compareOption As FilterUtils.CaseSensitivity) As String()
            If searchPattern Is Nothing OrElse searchPattern.Length = 0 Then searchPattern = New String() {""}
            Dim Result As New System.Collections.Generic.List(Of String)
            For MyPatternCounter As Integer = 0 To searchPattern.Length - 1
                ListOfStringAddItemsIfNotAlreadyPresent(Result, GetFiles(path, searchPattern(MyPatternCounter), compareOption))
            Next
            Return Result.ToArray
        End Function

        Public Shared Function GetFiles(ByVal path As String, ByVal searchPattern As String(), searchOptions As System.IO.SearchOption, ByVal compareOption As FilterUtils.CaseSensitivity) As String()
            If searchPattern Is Nothing OrElse searchPattern.Length = 0 Then searchPattern = New String() {""}
            Dim Result As New System.Collections.Generic.List(Of String)
            For MyPatternCounter As Integer = 0 To searchPattern.Length - 1
                ListOfStringAddItemsIfNotAlreadyPresent(Result, GetFiles(path, searchPattern(MyPatternCounter), searchOptions, compareOption))
            Next
            Return Result.ToArray
        End Function

        Public Shared Function GetFileSystemEntries(ByVal path As String, ByVal searchPattern As String, ByVal compareOption As FilterUtils.CaseSensitivity) As String()
            If searchPattern = Nothing Then searchPattern = "*"
            If IsLinuxEnvironment() Then
                Return FilterUtils.ApplyFileFilter(System.IO.Directory.GetFileSystemEntries(path, "*"), searchPattern, compareOption, path)
            Else
                Return FilterUtils.ApplyFileFilter(System.IO.Directory.GetFileSystemEntries(path, searchPattern), searchPattern, compareOption, path)
            End If
        End Function

        Public Shared Function GetFileSystemEntries(ByVal path As String, ByVal searchPattern As String(), ByVal compareOption As FilterUtils.CaseSensitivity) As String()
            If searchPattern Is Nothing OrElse searchPattern.Length = 0 Then searchPattern = New String() {""}
            Dim Result As New System.Collections.Generic.List(Of String)
            For MyPatternCounter As Integer = 0 To searchPattern.Length - 1
                ListOfStringAddItemsIfNotAlreadyPresent(Result, GetFileSystemEntries(path, searchPattern(MyPatternCounter), compareOption))
            Next
            Return Result.ToArray
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

        Public Shared Function GetDirectories(ByVal path As String, ByVal searchPattern As String(), ByVal compareOption As FilterUtils.CaseSensitivity) As String()
            If searchPattern Is Nothing OrElse searchPattern.Length = 0 Then searchPattern = New String() {""}
            Dim Result As New System.Collections.Generic.List(Of String)
            For MyPatternCounter As Integer = 0 To searchPattern.Length - 1
                ListOfStringAddItemsIfNotAlreadyPresent(Result, GetDirectories(path, searchPattern(MyPatternCounter), compareOption))
            Next
            Return Result.ToArray
        End Function

        Public Shared Function GetDirectories(ByVal path As String, ByVal searchPattern As String(), searchOptions As System.IO.SearchOption, ByVal compareOption As FilterUtils.CaseSensitivity) As String()
            If searchPattern Is Nothing OrElse searchPattern.Length = 0 Then searchPattern = New String() {""}
            Dim Result As New System.Collections.Generic.List(Of String)
            For MyPatternCounter As Integer = 0 To searchPattern.Length - 1
                ListOfStringAddItemsIfNotAlreadyPresent(Result, GetDirectories(path, searchPattern(MyPatternCounter), searchOptions, compareOption))
            Next
            Return Result.ToArray
        End Function

        Private Shared Function IsLinuxEnvironment() As Boolean
            Return System.Environment.OSVersion.Platform = PlatformID.MacOSX OrElse System.Environment.OSVersion.Platform = PlatformID.Unix
        End Function

        Private Shared Sub ListOfStringAddItemsIfNotAlreadyPresent(list As System.Collections.Generic.List(Of String), values As String())
            If values Is Nothing OrElse values.Length = 0 Then Return
            For MyCounter As Integer = 0 To values.Length - 1
                If list.Contains(values(MyCounter)) = False Then list.Add(values(MyCounter))
            Next
        End Sub

        ''' <summary>
        ''' If a directory path exists, return it else search for the next parent directory which exists
        ''' </summary>
        ''' <param name="path"></param>
        ''' <returns></returns>
        Public Shared Function FindExistingDirectoryOrParentDirectory(path As String) As String
            Dim Result As String = path
            Do While System.IO.Directory.Exists(Result) = False
                Result = System.IO.Path.GetDirectoryName(Result)
            Loop
            Return Result
        End Function
    End Class

End Namespace
