using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace JimmyPresentation.Tests
{
    [TestFixture]
    public class SerializationTests
    {
        [Test]
        public void X()
        {
            var thisData = new List<LargeValueObjectAsStruct[]> { new SerializeLargeValueObject().W };
            var ms = new MemoryStream();
            LargeValueObjectHelper.SaveTestStructArrayListToStream(thisData, new BinaryWriter(ms));

            ms.Seek(0, SeekOrigin.Begin);
            var boomerang = new List<LargeValueObjectAsStruct[]>();
            LargeValueObjectHelper.LoadTestStructArrayListFromStream(boomerang, new BinaryReader(ms));

            Assert.AreEqual(thisData.First().First().A, boomerang.First().First().A);
            Assert.AreEqual(thisData.Last().Last().A, boomerang.Last().Last().A);
        }
    }
}