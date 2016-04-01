Option Explicit On
Option Strict On

Namespace CompuMaster.IO

    ''' <summary>
    ''' Provides missing features for filtering files by their full name (instead of default windows behaviour with compatibility for short file names with 8.3-limitations)
    ''' </summary>
    ''' <remarks>
    ''' <para>By default, windows searches (so the .NET search as well) find files which match to your search pattern with their full names as well as their short names.</para>
    ''' <para>These search filters the files again, so the result is that you'll receive only those results which are valid with their full name.</para>
    ''' <example>Given are the files abc.doc and abc.docx. Searching for *.doc with default windows/.NET behaviour will find both files because the file abc.docx is represented in 8.3-style with abc~1.doc. Using the methods in this class will reduce this result to the correct result with only abc.doc.</example>
    ''' </remarks>
    Public Class FilterUtils

        Public Enum CaseSensitivity
            Windows = 0
            Unix = 1
        End Enum

        Public Shared Function ApplyFileFilter(ByVal paths As String(), ByVal fileSystemSearchPattern As String, ByVal compareOption As CaseSensitivity) As String()
            If paths Is Nothing Then Throw New ArgumentNullException("paths")
            If fileSystemSearchPattern = Nothing Then Return paths

            Dim Result As New ArrayList(paths)
            Dim regEx As System.Text.RegularExpressions.Regex = CreateFileRegEx(fileSystemSearchPattern, compareOption)
            For MyCounter As Integer = Result.Count - 1 To 0 Step -1
                If IsFilterMatch(regEx, (CType(Result(MyCounter), String))) = False Then
                    Result.RemoveAt(MyCounter)
                End If
            Next
            Return CType(Result.ToArray(GetType(String)), String())
        End Function

        Public Shared Function ConvertFileFilterIntoRegularExpression(ByVal searchExpression As String) As String
            'Example: searchExpression = "adslkfjd(.){}??[]?\/*.doc"
            Dim encoded As String = System.Text.RegularExpressions.Regex.Escape(searchExpression) '"adslkfjd\(\.\)\{}\?\?\[]\?\\/\*\.doc"
            encoded = encoded.Replace("\?", ".?") '? stands for zero or one character
            encoded = encoded.Replace("\*", ".*") '* stands for any number of characters
            Return encoded
        End Function

        Public Shared Function IsFilterMatch(ByVal fileName As String, ByVal fileSystemSearchPattern As String, ByVal compareOption As CaseSensitivity) As Boolean
            Return IsFilterMatch(CreateFileRegEx(fileSystemSearchPattern, compareOption), fileName)
        End Function

        Friend Shared Function IsFilterMatch(ByVal fileSystemRegEx As System.Text.RegularExpressions.Regex, ByVal fileName As String) As Boolean
            Dim ResultMatch As System.Text.RegularExpressions.Match = fileSystemRegEx.Match(fileName)
            If ResultMatch.Value <> fileName Then
                Return False
            Else
                Return True
            End If
        End Function

        Friend Shared Function CreateFileRegEx(ByVal fileSystemSearchPattern As String, ByVal compareOption As CaseSensitivity) As System.Text.RegularExpressions.Regex
            If compareOption = CaseSensitivity.Windows Then
                Return New System.Text.RegularExpressions.Regex(ConvertFileFilterIntoRegularExpression(fileSystemSearchPattern), Text.RegularExpressions.RegexOptions.CultureInvariant Or Text.RegularExpressions.RegexOptions.IgnoreCase)
            Else
                Return New System.Text.RegularExpressions.Regex(ConvertFileFilterIntoRegularExpression(fileSystemSearchPattern), Text.RegularExpressions.RegexOptions.CultureInvariant)
            End If
        End Function

    End Class

End Namespace
