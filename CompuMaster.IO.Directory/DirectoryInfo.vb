Option Explicit On
Option Strict On

Namespace CompuMaster.IO

    ''' <summary>
    ''' Provides missing features for System.IO.Directory/System.IO.DirectoryInfo 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DirectoryInfo

        Public Shared Function GetFileInfos(ByVal fileInfos As System.IO.FileInfo(), ByVal searchPattern As String, ByVal compareOption As FilterUtils.CaseSensitivity) As System.IO.FileInfo()
            Dim Result As New ArrayList(fileInfos)
            Dim regEx As System.Text.RegularExpressions.Regex = FilterUtils.CreateFileRegEx(searchPattern, compareOption)
            For MyCounter As Integer = Result.Count - 1 To 0 Step -1
                If FilterUtils.IsFilterMatch(regEx, (CType(Result(MyCounter), System.IO.FileInfo).Name)) = False Then
                    Result.RemoveAt(MyCounter)
                End If
            Next
            Return CType(Result.ToArray(GetType(System.IO.FileInfo)), System.IO.FileInfo())
        End Function

        Public Shared Function GetFileSystemInfos(ByVal fileSystemInfos As System.IO.FileSystemInfo(), ByVal searchPattern As String, ByVal compareOption As FilterUtils.CaseSensitivity) As System.IO.FileSystemInfo()
            Dim Result As New ArrayList(fileSystemInfos)
            Dim regEx As System.Text.RegularExpressions.Regex = FilterUtils.CreateFileRegEx(searchPattern, compareOption)
            For MyCounter As Integer = Result.Count - 1 To 0 Step -1
                If FilterUtils.IsFilterMatch(regEx, (CType(Result(MyCounter), System.IO.FileSystemInfo).Name)) = False Then
                    Result.RemoveAt(MyCounter)
                End If
            Next
            Return CType(Result.ToArray(GetType(System.IO.FileSystemInfo)), System.IO.FileSystemInfo())
        End Function

        Public Shared Function GetDirectoryInfos(ByVal directoryInfos As System.IO.DirectoryInfo(), ByVal searchPattern As String, ByVal compareOption As FilterUtils.CaseSensitivity) As System.IO.DirectoryInfo()
            Dim Result As New ArrayList(directoryInfos)
            Dim regEx As System.Text.RegularExpressions.Regex = FilterUtils.CreateFileRegEx(searchPattern, compareOption)
            For MyCounter As Integer = Result.Count - 1 To 0 Step -1
                If FilterUtils.IsFilterMatch(regEx, (CType(Result(MyCounter), System.IO.DirectoryInfo).Name)) = False Then
                    Result.RemoveAt(MyCounter)
                End If
            Next
            Return CType(Result.ToArray(GetType(System.IO.DirectoryInfo)), System.IO.DirectoryInfo())
        End Function

        Public Shared Function GetFileInfos(ByVal directory As System.IO.DirectoryInfo, ByVal searchPattern As String, ByVal compareOption As FilterUtils.CaseSensitivity) As System.IO.FileInfo()
            Return GetFileInfos(directory.GetFiles(searchPattern), searchPattern, compareOption)
        End Function

        Public Shared Function GetFileInfos(ByVal directory As System.IO.DirectoryInfo, ByVal searchPattern As String, ByVal searchOptions As System.IO.SearchOption, ByVal compareOption As FilterUtils.CaseSensitivity) As System.IO.FileInfo()
            Return GetFileInfos(directory.GetFiles(searchPattern, searchOptions), searchPattern, compareOption)
        End Function

        Public Shared Function GetDirectoryInfos(ByVal directory As System.IO.DirectoryInfo, ByVal searchPattern As String, ByVal compareOption As FilterUtils.CaseSensitivity) As System.IO.DirectoryInfo()
            Return GetDirectoryInfos(directory.GetDirectories(searchPattern), searchPattern, compareOption)
        End Function

        Public Shared Function GetDirectoryInfos(ByVal directory As System.IO.DirectoryInfo, ByVal searchPattern As String, ByVal searchOptions As System.IO.SearchOption, ByVal compareOption As FilterUtils.CaseSensitivity) As System.IO.DirectoryInfo()
            Return GetDirectoryInfos(directory.GetDirectories(searchPattern, searchOptions), searchPattern, compareOption)
        End Function

        Public Shared Function GetFileSystemInfos(ByVal directory As System.IO.DirectoryInfo, ByVal searchPattern As String, ByVal compareOption As FilterUtils.CaseSensitivity) As System.IO.FileSystemInfo()
            Return GetFileSystemInfos(directory.GetFileSystemInfos(searchPattern), searchPattern, compareOption)
        End Function

        Public Shared Function GetFileInfos(ByVal path As String, ByVal searchPattern As String, ByVal compareOption As FilterUtils.CaseSensitivity) As System.IO.FileInfo()
            Return GetFileInfos(New System.IO.DirectoryInfo(path), searchPattern, compareOption)
        End Function

        Public Shared Function GetFileInfos(ByVal path As String, ByVal searchPattern As String, ByVal searchOptions As System.IO.SearchOption, ByVal compareOption As FilterUtils.CaseSensitivity) As System.IO.FileInfo()
            Return GetFileInfos(New System.IO.DirectoryInfo(path), searchPattern, searchOptions, compareOption)
        End Function

        Public Shared Function GetDirectoryInfos(ByVal path As String, ByVal searchPattern As String, ByVal compareOption As FilterUtils.CaseSensitivity) As System.IO.DirectoryInfo()
            Return GetDirectoryInfos(New System.IO.DirectoryInfo(path), searchPattern, compareOption)
        End Function

        Public Shared Function GetDirectoryInfos(ByVal path As String, ByVal searchPattern As String, ByVal searchOptions As System.IO.SearchOption, ByVal compareOption As FilterUtils.CaseSensitivity) As System.IO.DirectoryInfo()
            Return GetDirectoryInfos(New System.IO.DirectoryInfo(path), searchPattern, searchOptions, compareOption)
        End Function

        Public Shared Function GetFileSystemInfos(ByVal path As String, ByVal searchPattern As String, ByVal compareOption As FilterUtils.CaseSensitivity) As System.IO.FileSystemInfo()
            Return GetFileSystemInfos(New System.IO.DirectoryInfo(path), searchPattern, compareOption)
        End Function

    End Class

End Namespace
