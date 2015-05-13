using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace JimmyPresentation.Tests
{
    [TestFixture]
    public class TestArrayCopying
    {
        [Test]
        public void TestThatStructArrayCanBeCopiedToByteArrayAndThenBackAgain1()
        {
            var arrayOfDoubles = Enumerable.Range(0, 100).Select(_ => _/10.0).ToArray();
            byte[] byteArray = null;
            var returnedSize = arrayOfDoubles.CopyToByteArray(ref byteArray);

            // should be allocated to the exact length needed
            Assert.AreEqual(returnedSize, byteArray.Length);
            // we know that a double is 8 bytes
            Assert.AreEqual(8*100, byteArray.Length);

            double[] copiedArrayOfDoubles = null;
            var returnedElements = byteArray.CopyToStructArray(ref copiedArrayOfDoubles);

            // should be allocated to the exact length needed
            Assert.AreEqual(returnedElements, copiedArrayOfDoubles.Length);
            // we know that a double is 8 bytes
            Assert.AreEqual(arrayOfDoubles.Length, copiedArrayOfDoubles.Length);

            for (var i = 0; i < 100; i++)
                Assert.AreEqual(copiedArrayOfDoubles[i], arrayOfDoubles[i]);
        }

        [Test]
        public void TestThatStructArrayCanBeCopiedToByteArrayAndThenBackAgain2()
        {
            var arrayOfDoubles = Enumerable.Range(0, 100).Select(_ => _ / 10.0).ToArray();
            var byteArray = new byte[100000];
            var returnedSize = arrayOfDoubles.CopyToByteArray(ref byteArray);

            // should not have been reallocated
            Assert.AreEqual(100000, byteArray.Length);
            // we know that a double is 8 bytes
            Assert.AreEqual(8 * 100, returnedSize);

            double[] copiedArrayOfDoubles = null;
            var returnedElements = byteArray.CopyToStructArray(ref copiedArrayOfDoubles, 100);

            // should be allocated to the exact length needed
            Assert.AreEqual(returnedElements, copiedArrayOfDoubles.Length);
            // we know that a double is 8 bytes
            Assert.AreEqual(arrayOfDoubles.Length, copiedArrayOfDoubles.Length);

            for (var i = 0; i < 100; i++)
                Assert.AreEqual(copiedArrayOfDoubles[i], arrayOfDoubles[i]);
        }

        [Test]
        public void TestThatStructArrayCanBeCopiedToByteArrayAndThenBackAgain3()
        {
            var arrayOfDoubles = Enumerable.Range(0, 100).Select(_ => _ / 10.0).ToArray();
            var copiedArrayOfDoubles = arrayOfDoubles.CopyToByteArray().CopyToStructArray<double>();

            for (var i = 0; i < 100; i++)
                Assert.AreEqual(copiedArrayOfDoubles[i], arrayOfDoubles[i]);
        }

    }

}
