Option Explicit On
Option Strict On

Imports NUnit.Framework

Namespace CompuMaster.Tests.IO

    <SetUpFixture()> Public Class TestSetup

        <NUnit.Framework.OneTimeSetUp> Public Sub InitialSetupForTests()
            System.Console.Write("TestData Directory=" & GlobalTestSetup.PathToTestFiles("testdata"))
            If System.IO.Directory.Exists(GlobalTestSetup.PathToTestFiles("testdata")) = False Then
                System.Console.WriteLine(": NOT FOUND => FAILURE")
            Else
                System.Console.WriteLine(": FOUND => SUCCESS")
            End If
            LogDirectoryContent(GlobalTestSetup.PathToTestFiles("testdata"), 0)
        End Sub

        Private Sub LogDirectoryContent(path As String, indentLevel As Integer)
            For Each item As String In System.IO.Directory.GetDirectories(path)
                System.Console.Write(Space(4 * indentLevel))
                System.Console.WriteLine("Dir:  " & item)
                LogDirectoryContent(item, indentLevel + 1)
            Next
            For Each item As String In System.IO.Directory.GetFiles(path)
                System.Console.Write(Space(4 * indentLevel))
                System.Console.WriteLine("File: " & item)
            Next
        End Sub

        Friend Shared Function IsLinuxEnvironment() As Boolean
            Return System.Environment.OSVersion.Platform = PlatformID.Unix
        End Function

        Friend Shared Function IsMacEnvironment() As Boolean
            Return System.Environment.OSVersion.Platform = PlatformID.MacOSX
        End Function

    End Class

    <TestFixture()> Public Class Directory

        <Test> Sub GetFilesStepByStep()
            Dim searchPattern As String
            Dim NativeResultsCount As Integer
            Dim path As String = GlobalTestSetup.PathToTestFiles("testdata")
            System.Console.WriteLine("TestData Directory=" & path)

            searchPattern = "*.asp"
            System.Console.WriteLine("SearchPattern=" & searchPattern)
            System.Console.WriteLine("Native results")
            NativeResultsCount = System.IO.Directory.GetFiles(path, searchPattern).Length
            System.Console.WriteLine("=> Native results of Sys.IO.Dir.GetFiles:" & NativeResultsCount)
            For Each item As String In System.IO.Directory.GetFiles(path, searchPattern)
                System.Console.WriteLine("    File: " & item)
            Next
            Assert.AreEqual(1, NativeResultsCount, "Native results count")
            System.Console.WriteLine()
            System.Console.WriteLine()

            searchPattern = "*.Asp"
            System.Console.WriteLine("SearchPattern=" & searchPattern)
            System.Console.WriteLine("Native results")
            NativeResultsCount = System.IO.Directory.GetFiles(path, searchPattern).Length
            System.Console.WriteLine("=> Native results of Sys.IO.Dir.GetFiles:" & NativeResultsCount)
            For Each item As String In System.IO.Directory.GetFiles(path, searchPattern)
                System.Console.WriteLine("    File: " & item)
            Next
            If TestSetup.IsLinuxEnvironment Then
                'file systems are case-sensitive
                Assert.AreEqual(0, NativeResultsCount, "Native results count (Linux Env -> case-sensitive file systems)")
            Else
                'file systems are case-insensitive
                Assert.AreEqual(1, NativeResultsCount, "Native results count (Win/Mac Env -> case-insensitive file systems)")
            End If
            System.Console.WriteLine()
            System.Console.WriteLine()

            searchPattern = "*.asp"
            System.Console.WriteLine("SearchPattern=" & searchPattern)
            System.Console.WriteLine("WinMode: Results after applied filter")
            For Each item As String In CompuMaster.IO.FilterUtils.ApplyFileFilter(System.IO.Directory.GetFiles(path, searchPattern), searchPattern, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows)
                System.Console.WriteLine("    File: " & item)
            Next
            Assert.AreEqual(1, CompuMaster.IO.FilterUtils.ApplyFileFilter(System.IO.Directory.GetFiles(path, searchPattern), searchPattern, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Applied filter " & searchPattern & ": Win")
            System.Console.WriteLine()
            System.Console.WriteLine("LinuxMode: Results after applied filter")
            For Each item As String In CompuMaster.IO.FilterUtils.ApplyFileFilter(System.IO.Directory.GetFiles(path, searchPattern), searchPattern, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix)
                System.Console.WriteLine("    File: " & item)
            Next
            Assert.AreEqual(1, CompuMaster.IO.FilterUtils.ApplyFileFilter(System.IO.Directory.GetFiles(path, searchPattern), searchPattern, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Applied filter " & searchPattern & ": Unix")
            System.Console.WriteLine()
            System.Console.WriteLine()

            searchPattern = "*.Asp"
            System.Console.WriteLine("WinMode: Results after applied filter")
            System.Console.WriteLine("SearchPattern=" & searchPattern)
            If TestSetup.IsLinuxEnvironment Then
                For Each item As String In CompuMaster.IO.FilterUtils.ApplyFileFilter(System.IO.Directory.GetFiles(path, "*"), searchPattern, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows)
                    System.Console.WriteLine("    File: " & item)
                Next
                Assert.AreEqual(1, CompuMaster.IO.FilterUtils.ApplyFileFilter(System.IO.Directory.GetFiles(path, "*"), searchPattern, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Applied filter " & searchPattern & ": Win")
            Else
                For Each item As String In CompuMaster.IO.FilterUtils.ApplyFileFilter(System.IO.Directory.GetFiles(path, searchPattern), searchPattern, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows)
                    System.Console.WriteLine("    File: " & item)
                Next
                Assert.AreEqual(1, CompuMaster.IO.FilterUtils.ApplyFileFilter(System.IO.Directory.GetFiles(path, searchPattern), searchPattern, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Applied filter " & searchPattern & ": Win")
            End If
            System.Console.WriteLine()
            System.Console.WriteLine("LinuxMode: Results after applied filter")
            For Each item As String In CompuMaster.IO.FilterUtils.ApplyFileFilter(System.IO.Directory.GetFiles(path, searchPattern), searchPattern, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix)
                System.Console.WriteLine("    File: " & item)
            Next
            Assert.AreEqual(0, CompuMaster.IO.FilterUtils.ApplyFileFilter(System.IO.Directory.GetFiles(path, searchPattern), searchPattern, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Applied filter " & searchPattern & ": Unix")
            System.Console.WriteLine()
            System.Console.WriteLine()

        End Sub

        <Test()> Sub GetFiles()
            System.Console.WriteLine("TestData Directory=" & GlobalTestSetup.PathToTestFiles("testdata"))
            System.Console.WriteLine()
            System.Console.WriteLine("WinMode: GetFiles search pattern: *.*")
            For Each item As String In CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.*", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows)
                System.Console.WriteLine("File: " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("LinuxMode: GetFiles search pattern: *.*")
            For Each item As String In CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.*", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix)
                System.Console.WriteLine("File: " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("WinMode: GetFiles search pattern: *")
            For Each item As String In CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows)
                System.Console.WriteLine("File: " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("LinuxMode: GetFiles search pattern: *")
            For Each item As String In CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix)
                System.Console.WriteLine("File: " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("WinMode: GetFiles search pattern: *.Asp")
            For Each item As String In CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.Asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows)
                System.Console.WriteLine("File: " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("LinuxMode: GetFiles search pattern: *.Asp")
            For Each item As String In CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.Asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix)
                System.Console.WriteLine("File: " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("WinMode: GetFiles search pattern: *.asp")
            For Each item As String In CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows)
                System.Console.WriteLine("File: " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("LinuxMode: GetFiles search pattern: *.asp")
            For Each item As String In CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix)
                System.Console.WriteLine("File: " & item)
            Next
            System.Console.WriteLine()

            Assert.AreEqual(1, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.Asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #1")
            Assert.AreEqual(1, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #2")
            Assert.AreEqual(0, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.Asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #3")
            Assert.AreEqual(1, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #4")
            Assert.True(CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.Asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows)(0).EndsWith(".asp"), "Test #5 - wrong file found: " & CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.Asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows)(0))
            Assert.AreEqual(0, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.Asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #11")
            Assert.AreEqual(1, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #12")
            Assert.AreEqual(3, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #13")
            Assert.AreEqual(0, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.Aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #14")
            Assert.AreEqual(1, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata\subdir"), "*.Aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #21")
            Assert.AreEqual(1, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata\subdir"), "*.aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #22")
            Assert.AreEqual(2, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata\subdir"), "*.aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #23")

            Assert.AreEqual(1, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.Asp", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #31w")
            Assert.AreEqual(0, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.Asp", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #31u")
            Assert.AreEqual(1, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.asp", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #32w")
            Assert.AreEqual(1, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.asp", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #32u")
            Assert.AreEqual(5, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.aspx", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #33w")
            Assert.AreEqual(4, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.aspx", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #33u")
            Assert.AreEqual(5, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.Aspx", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #34w")
            Assert.AreEqual(1, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.Aspx", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #34u")

            Assert.AreEqual(1, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "xyz*", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #41w")
            Assert.AreEqual(1, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "xyz*", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #41u")
            Assert.AreEqual(1, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "readme", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #42w")
            Assert.AreEqual(1, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "readme", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #42u")
            Assert.AreEqual(1, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "ReadMe", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #43w")
            Assert.AreEqual(0, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "ReadMe", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #43u")

            Assert.AreEqual(7, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), CType(Nothing, String), System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #50w")
            Assert.AreEqual(7, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), CType(Nothing, String), System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #50u")
            Assert.AreEqual(7, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #50w")
            Assert.AreEqual(7, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #50u")
            Assert.AreEqual(7, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #51w")
            Assert.AreEqual(7, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #51u")
            Assert.AreEqual(0, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #52w")
            Assert.AreEqual(0, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #52u")

        End Sub

        <Test()> Sub GetFiles_MultipleFilters()
            System.Console.WriteLine("TestData Directory=" & GlobalTestSetup.PathToTestFiles("testdata"))
            System.Console.WriteLine()
            System.Console.WriteLine("WinMode: GetFiles search pattern: *.*")
            For Each item As String In CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.*", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows)
                System.Console.WriteLine("File: " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("LinuxMode: GetFiles search pattern: *.*")
            For Each item As String In CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.*", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix)
                System.Console.WriteLine("File: " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("WinMode: GetFiles search pattern: *")
            For Each item As String In CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows)
                System.Console.WriteLine("File: " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("LinuxMode: GetFiles search pattern: *")
            For Each item As String In CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix)
                System.Console.WriteLine("File: " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("WinMode: GetFiles search pattern: *.Asp")
            For Each item As String In CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.Asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows)
                System.Console.WriteLine("File: " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("LinuxMode: GetFiles search pattern: *.Asp")
            For Each item As String In CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.Asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix)
                System.Console.WriteLine("File: " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("WinMode: GetFiles search pattern: *.asp")
            For Each item As String In CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows)
                System.Console.WriteLine("File: " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("LinuxMode: GetFiles search pattern: *.asp")
            For Each item As String In CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix)
                System.Console.WriteLine("File: " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("WinMode: GetFiles search pattern: readme")
            For Each item As String In CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "readme", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows)
                System.Console.WriteLine("File: " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("LinuxMode: GetFiles search pattern: readme")
            For Each item As String In CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "readme", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix)
                System.Console.WriteLine("File: " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("WinMode: GetFiles search pattern: ReadMe")
            For Each item As String In CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "ReadMe", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows)
                System.Console.WriteLine("File: " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("LinuxMode: GetFiles search pattern: ReadMe")
            For Each item As String In CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "ReadMe", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix)
                System.Console.WriteLine("File: " & item)
            Next
            System.Console.WriteLine()

            Assert.AreEqual(3, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), New String() {"xyz*", "readme", "ReadMe", "def*", "DEF*"}, System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #11w")
            Assert.AreEqual(3, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), New String() {"xyz*", "readme", "ReadMe", "def*", "DEF*"}, System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #11u")
            Assert.AreEqual(6, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), New String() {"*.asp", "*.aspx"}, System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #12w")
            Assert.AreEqual(5, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), New String() {"*.asp", "*.aspx"}, System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #12u")
            Assert.AreEqual(6, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), New String() {"*.Asp", "*.Aspx"}, System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #13w")
            Assert.AreEqual(1, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), New String() {"*.Asp", "*.Aspx"}, System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #13u")
            Assert.AreEqual(6, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), New String() {"*.asp", "*.aspx", "*.Asp", "*.Aspx"}, System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #14w")
            Assert.AreEqual(6, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), New String() {"*.asp", "*.aspx", "*.Asp", "*.Aspx"}, System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #14u")
            Assert.AreEqual(7, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), New String() {"*"}, System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #15w")
            Assert.AreEqual(7, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), New String() {"*"}, System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #15u")
            Assert.AreEqual(7, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), New String() {""}, System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #16w")
            Assert.AreEqual(7, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), New String() {""}, System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #16u")
            Assert.AreEqual(7, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), New String() {}, System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #17w")
            Assert.AreEqual(7, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), New String() {}, System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #17u")
            Assert.AreEqual(7, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), CType(Nothing, String()), System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #18w")
            Assert.AreEqual(7, CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), CType(Nothing, String()), System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #18u")
        End Sub

        <Test()> Sub GetDirectories()
            System.Console.WriteLine("TestData Directory=" & GlobalTestSetup.PathToTestFiles("testdata"))
            System.Console.WriteLine()
            System.Console.WriteLine("WinMode: GetDirectories search pattern: *.*")
            For Each item As String In CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "*.*", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows)
                System.Console.WriteLine("Dir:  " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("LinuxMode: GetDirectories search pattern: *.*")
            For Each item As String In CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "*.*", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix)
                System.Console.WriteLine("Dir:  " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("WinMode: GetFiGetDirectoriesles search pattern: *")
            For Each item As String In CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "*", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows)
                System.Console.WriteLine("Dir: " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("LinuxMode: GetDirectories search pattern: *")
            For Each item As String In CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "*", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix)
                System.Console.WriteLine("Dir:  " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("WinMode: GetDirectories search pattern: *.Asp")
            For Each item As String In CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "*.Asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows)
                System.Console.WriteLine("Dir:  " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("LinuxMode: GetDirectories search pattern: *.Asp")
            For Each item As String In CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "*.Asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix)
                System.Console.WriteLine("Dir:  " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("WinMode: GetDirectories search pattern: *.asp")
            For Each item As String In CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "*.asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows)
                System.Console.WriteLine("Dir:  " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("LinuxMode: GetDirectories search pattern: *.asp")
            For Each item As String In CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "*.asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix)
                System.Console.WriteLine("Dir:  " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("WinMode: GetDirectories search pattern: sub*")
            For Each item As String In CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "sub*", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows)
                System.Console.WriteLine("Dir:  " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("LinuxMode: GetDirectories search pattern: sub*")
            For Each item As String In CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "sub*", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix)
                System.Console.WriteLine("Dir:  " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("WinMode: GetDirectories search pattern: Sub*")
            For Each item As String In CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "Sub*", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows)
                System.Console.WriteLine("Dir:  " & item)
            Next
            System.Console.WriteLine()
            System.Console.WriteLine("LinuxMode: GetDirectories search pattern: Sub*")
            For Each item As String In CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "Sub*", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix)
                System.Console.WriteLine("Dir:  " & item)
            Next
            System.Console.WriteLine()

            Assert.AreEqual(1, CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "sub*", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #1w")
            Assert.AreEqual(1, CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "sub*", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #1u")
            Assert.AreEqual(1, CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "Sub*", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #2w")
            Assert.AreEqual(0, CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "Sub*", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #2u")
            Assert.AreEqual(0, CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "*.Asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #11")
            Assert.AreEqual(0, CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "*.asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #12")
            Assert.AreEqual(0, CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "*.aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #13")
            Assert.AreEqual(0, CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "*.Aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #14")
            Assert.AreEqual(0, CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata\subdir"), "", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #21")
            Assert.AreEqual(0, CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata\subdir"), CType(Nothing, String), CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #22")
            Assert.AreEqual(0, CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata\subdir"), "*", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #23")
            Assert.AreEqual(0, CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata\subdir"), "*.aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #23")

            Assert.AreEqual(1, CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), CType(Nothing, String), System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #30w")
            Assert.AreEqual(1, CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), CType(Nothing, String), System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #30u")
            'Assert.AreEqual(1, System.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "*.*", System.IO.SearchOption.AllDirectories).Length, "Test #31net")
            Assert.AreEqual(0, CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "*.*", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #31w")
            Assert.AreEqual(0, CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "*.*", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #31u")
            Assert.AreEqual(1, System.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "*", System.IO.SearchOption.AllDirectories).Length, "Test #32net")
            Assert.AreEqual(1, CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "*", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #32w")
            Assert.AreEqual(1, CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "*", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #32u")
            Assert.AreEqual(1, CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #33w")
            Assert.AreEqual(1, CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #33u")
            Assert.AreEqual(0, CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "*.Aspx", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #34w")
            Assert.AreEqual(0, CompuMaster.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"), "*.Aspx", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #34u")
        End Sub

    End Class

    <TestFixture()> Public Class DirectoryInfo

        <Test()> Sub GetFiles()
            Dim IsLinuxEnvironment As Boolean = System.Environment.OSVersion.Platform = PlatformID.MacOSX OrElse System.Environment.OSVersion.Platform = PlatformID.Unix
            Assert.AreEqual(1, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "*.Asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #1aw")
            Assert.AreEqual(0, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "*.Asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #1au")
            Assert.AreEqual(1, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "*.asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #1bw")
            Assert.AreEqual(1, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "*.asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #1bu")
            Assert.AreEqual(3, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "???.Aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #2aw")
            Assert.AreEqual(0, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "???.Aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #2au")
            Assert.AreEqual(3, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "???.aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #2bw")
            Assert.AreEqual(3, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "???.aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #2bu")
            Assert.AreEqual(3, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "???.Aspx", System.IO.SearchOption.TopDirectoryOnly, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #3a1")
            Assert.AreEqual(5, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "???.Aspx", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #3a2")
            Assert.AreEqual(3, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "???.aspx", System.IO.SearchOption.TopDirectoryOnly, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #3b")
            Assert.AreEqual(5, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "???.aspx", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #3b")
            Assert.AreEqual(0, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "???.Aspx", System.IO.SearchOption.TopDirectoryOnly, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #4a")
            Assert.AreEqual(3, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "???.aspx", System.IO.SearchOption.TopDirectoryOnly, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #4b")
            Assert.AreEqual(1, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "???.Aspx", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #5a")
            Assert.AreEqual(4, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "???.aspx", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #5b")

            Assert.AreEqual(1, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "*.Asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #11")
            Assert.AreEqual(3, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "???.Aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #12")
            Assert.AreEqual(5, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "???.Aspx", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #13")
            Assert.AreEqual(0, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "???.Aspx", System.IO.SearchOption.TopDirectoryOnly, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #14")
            Assert.AreEqual(1, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "???.Aspx", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #15")

            Assert.AreEqual(1, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "xyz*", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #21w")
            Assert.AreEqual(1, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "xyz*", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #21u")
            Assert.AreEqual(1, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "readme", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #23w")
            Assert.AreEqual(1, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "readme", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #23u")
            Assert.AreEqual(1, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "ReadMe", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #25w")
            Assert.AreEqual(0, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "ReadMe", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #25u")
        End Sub

    End Class

    <TestFixture()> Public Class FilterUtils

        <Test()> Sub IsMatch()
            Assert.AreEqual(False, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.aspx", "abc.asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(False, CompuMaster.IO.FilterUtils.IsFilterMatch("aBc.aspx", "abc.aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix))
            Assert.AreEqual(False, CompuMaster.IO.FilterUtils.IsFilterMatch("bbc.aspx", "a?c.aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(False, CompuMaster.IO.FilterUtils.IsFilterMatch("cba.aspx", "??c.aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(False, CompuMaster.IO.FilterUtils.IsFilterMatch("acd.aspx", "??c.aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(False, CompuMaster.IO.FilterUtils.IsFilterMatch("abd.aspx", "??c.aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(False, CompuMaster.IO.FilterUtils.IsFilterMatch("ab_c.aspx", "??c.aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(False, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.aspx.", "abc.asp?", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(False, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.aspxd", "abc.asp?", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(False, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.aap", "abc.asp*", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(False, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.aapx", "abc.asp*", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))

            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.aspx", "abc.aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("aBc.aspx", "abc.aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.aspx", "a?c.aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("c.aspx", "??c.aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("ac.aspx", "??c.aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.aspx", "??c.aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.aspx", "abc.asp?", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.asp", "abc.asp?", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.asp", "abc.asp*", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.aspx", "abc.asp*", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))

            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.aspx", "abc.aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix))
            Assert.AreEqual(False, CompuMaster.IO.FilterUtils.IsFilterMatch("aBc.aspx", "abc.aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.aspx", "a?c.aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("c.aspx", "??c.aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("ac.aspx", "??c.aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.aspx", "??c.aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.aspx", "abc.asp?", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.asp", "abc.asp?", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.asp", "abc.asp*", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.aspx", "abc.asp*", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix))

            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("abc", "*", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.", "*", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.txt", "*", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch(".abc", "*", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch(".abc.txt", "*", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("abc", "*", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.", "*", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.txt", "*", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch(".abc", "*", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch(".abc.txt", "*", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix))

            Assert.AreEqual(False, CompuMaster.IO.FilterUtils.IsFilterMatch("abc", "*.", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.", "*.", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(False, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.txt", "*.", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(False, CompuMaster.IO.FilterUtils.IsFilterMatch(".abc", "*.", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(False, CompuMaster.IO.FilterUtils.IsFilterMatch(".abc.txt", "*.", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows))
            Assert.AreEqual(False, CompuMaster.IO.FilterUtils.IsFilterMatch("abc", "*.", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix))
            Assert.AreEqual(True, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.", "*.", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix))
            Assert.AreEqual(False, CompuMaster.IO.FilterUtils.IsFilterMatch("abc.txt", "*.", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix))
            Assert.AreEqual(False, CompuMaster.IO.FilterUtils.IsFilterMatch(".abc", "*.", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix))
            Assert.AreEqual(False, CompuMaster.IO.FilterUtils.IsFilterMatch(".abc.txt", "*.", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix))
        End Sub
    End Class

End Namespace