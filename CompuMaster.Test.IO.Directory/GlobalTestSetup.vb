Namespace CompuMaster.Tests.IO

    Public Class GlobalTestSetup

        Public Shared Function PathToTestFiles(subPath As String) As String
            Return System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location), subPath.Replace("\", System.IO.Path.DirectorySeparatorChar))
        End Function

    End Class

End Namespace