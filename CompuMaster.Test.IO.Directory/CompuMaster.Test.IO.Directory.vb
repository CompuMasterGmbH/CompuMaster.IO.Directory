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
            For Each item As String In System.IO.Directory.GetDirectories(GlobalTestSetup.PathToTestFiles("testdata"))
                System.Console.WriteLine("Dir:  " & item)
            Next
            For Each item As String In System.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"))
                System.Console.WriteLine("File: " & item)
            Next
        End Sub

    End Class

    <TestFixture()> Public Class Directory

            <Test()> Sub GetFiles()
                Assert.True(CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.Asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows)(0).EndsWith(".asp"))
                Assert.True(CompuMaster.IO.Directory.GetFiles(GlobalTestSetup.PathToTestFiles("testdata"), "*.Asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length = 0)
            End Sub

    End Class

    <TestFixture()> Public Class DirectoryInfo

        <Test()> Sub GetFiles()
            Assert.AreEqual(1, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "*.Asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length)
            Assert.AreEqual(3, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "???.Aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length)
            Assert.AreEqual(5, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "???.Aspx", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length)
            Assert.AreEqual(0, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "???.Aspx", System.IO.SearchOption.TopDirectoryOnly, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length)
            Assert.AreEqual(1, CompuMaster.IO.DirectoryInfo.GetFileInfos(GlobalTestSetup.PathToTestFiles("testdata"), "???.Aspx", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length)
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