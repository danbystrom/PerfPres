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
    public class LengthTests
    {
        [Test]
        public static void BasicTest()
        {
            var oneMeter = Length.FromMeters(1);
            var oneHundresCemtimeters = Length.FromCentimeters(100);
            Assert.AreEqual(oneMeter.Meters, oneHundresCemtimeters.Meters, 0.00000000001);
        }

        [Test]
        public static void TestMarathon()
        {
            var marathonDistance = 26.Miles() + 385.Yards();
            Assert.AreEqual(42195, marathonDistance.Meters, 0.5);
        }
    }
}
