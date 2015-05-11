using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace JimmyPresentation.Tests
{
    [TestFixture]
    public class ZipCodeTests
    {
        [Test]
        public static void TestWillFormatNicely()
        {
            var zipCode = (ZipCode) "12345";
            Assert.AreEqual("123 45", zipCode.ToString());
            Assert.AreEqual(12345, (int) zipCode);
        }

        [Test]
        public static void TestZeroPresentsAsEmptyString()
        {
            var zipCode = (ZipCode) 0;
            Assert.AreEqual("", (string) zipCode);
            Assert.AreEqual(0, (int) zipCode);
        }

    }

}
