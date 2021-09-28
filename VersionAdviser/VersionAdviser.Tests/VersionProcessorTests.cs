using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using VersionAdviser.Helpers;

namespace VersionAdviser.Tests
{
    [TestClass]
    public class VersionProcessorTests
    {
        VersionProcessor vp;
        Version defaultVersion;

        [TestInitialize]
        public void SetUp()
        {
            vp = new VersionProcessor();
            defaultVersion = new Version(1, 0, 0, 0);
        }

        [TestMethod]
        public void InitEmpty_VersionIs1dot0dot0dot0()
        {
            Assert.AreEqual(vp.SoftwareVersionToCompareWith, defaultVersion);
        }

        [TestMethod]
        public void InitEmpty_AssertExceptionMessageIsNull()
        {
            Assert.IsTrue(String.IsNullOrEmpty(vp.ExceptionMessage));
        }

        [TestMethod]
        public void SetVersionJustByInteger_ProcessedFine()
        {
            vp.UserProvidedVersion = "7";

            Assert.AreEqual(vp.SoftwareVersionToCompareWith, new Version(7, 0, 0, 0));
            Assert.IsTrue(String.IsNullOrEmpty(vp.ExceptionMessage));
        }

        [TestMethod]
        public void SetVersionWithTwoParams_ProcessedFine()
        {
            vp.UserProvidedVersion = "8.1";

            Assert.AreEqual(vp.SoftwareVersionToCompareWith, new Version(8, 1));
            Assert.IsTrue(String.IsNullOrEmpty(vp.ExceptionMessage));
        }

        [TestMethod]
        public void SetVersionWithThreeParams_ProcessedFine()
        {
            vp.UserProvidedVersion = "8.1.2";

            Assert.AreEqual(vp.SoftwareVersionToCompareWith, new Version(8, 1, 2));
            Assert.IsTrue(String.IsNullOrEmpty(vp.ExceptionMessage));
        }

        [TestMethod]
        public void TestVersion1_ExpectSevenResults()
        {
            vp.UserProvidedVersion = "1.0.0";
            Assert.AreEqual(7, vp.NewerVersions().Count);
            Assert.IsTrue(String.IsNullOrEmpty(vp.ExceptionMessage));
        }

        [TestMethod]
        public void TestVersion0dot0dot3_ExpectNineResults()
        {
            vp.UserProvidedVersion = "0.0.3";
            Assert.AreEqual(9, vp.NewerVersions().Count);
            Assert.IsTrue(String.IsNullOrEmpty(vp.ExceptionMessage));
        }

        [TestMethod]
        public void TestVersion123_ExpectTwoResults()
        {
            vp.UserProvidedVersion = "123";
            Assert.AreEqual(2, vp.NewerVersions().Count);
            Assert.IsTrue(String.IsNullOrEmpty(vp.ExceptionMessage));
        }
    }
}
