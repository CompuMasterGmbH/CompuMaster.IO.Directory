Imports NUnit.Framework
Namespace CompuMaster.Tests.IO

    <TestFixture()> Public Class Directory

        <Test()> Sub GetFiles()
            Assert.True(CompuMaster.IO.Directory.GetFiles("c:\temp", "*.Asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows)(0).EndsWith(".asp"))
            Assert.True(CompuMaster.IO.Directory.GetFiles("c:\temp", "*.Asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Unix).Length = 0)
        End Sub

    End Class

    <TestFixture()> Public Class DirectoryInfo

        <Test()> Sub GetFiles()
            Assert.AreEqual(1, CompuMaster.IO.DirectoryInfo.GetFileInfos("c:\temp", "*.Asp", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length)
            Assert.AreEqual(3, CompuMaster.IO.DirectoryInfo.GetFileInfos("c:\temp", "??.Aspx", CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length)
            Assert.AreEqual(4, CompuMaster.IO.DirectoryInfo.GetFileInfos("c:\temp", "??.Aspx", System.IO.SearchOption.AllDirectories, CompuMaster.IO.FilterUtils.CaseSensitivity.Windows).Length)
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