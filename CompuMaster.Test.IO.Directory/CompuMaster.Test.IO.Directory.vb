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
    End Class

    <TestFixture()> Public Class Directory

            <Test()> Sub GetFiles()
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
        End Sub

    End Class

    <TestFixture()> Public Class DirectoryInfo

        <Test()> Sub GetFiles()
            Assert.AreEqual(1, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "*.Asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #1")
            Assert.AreEqual(3, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "???.Aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #2")
            Assert.AreEqual(5, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "???.Aspx", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length, "Test #3")
            Assert.AreEqual(0, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "???.Aspx", System.IO.SearchOption.TopDirectoryOnly, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #4")
            Assert.AreEqual(1, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "???.Aspx", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length, "Test #5")
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
        End Sub
    End Class

End Namespace