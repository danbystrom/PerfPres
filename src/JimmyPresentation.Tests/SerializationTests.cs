using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using ProtoBuf;

namespace JimmyPresentation.Tests
{
    [TestFixture]
    public class SerializationTests
    {
        [Test]
        public void TestRawSerializerInMemory()
        {
            var thisData = new List<LargeValueObjectAsStruct[]> {SerializeLargeValueObject.New(1000).Data};
            var ms = new MemoryStream();
            LargeValueObjectHelper.SaveTestStructArrayListToStream(thisData, new BinaryWriter(ms));

            ms.Seek(0, SeekOrigin.Begin);
            var boomerang = new List<LargeValueObjectAsStruct[]>();
            LargeValueObjectHelper.LoadTestStructArrayListFromStream(boomerang, new BinaryReader(ms));

            Assert.AreEqual(thisData.First().First().A, boomerang.First().First().A);
            Assert.AreEqual(thisData.Last().Last().A, boomerang.Last().Last().A);
        }

        [Test]
        public void TestProtobufSerializerInMemory()
        {
            var thisData = SerializeLargeValueObject.New(1000).Data;
            
            using (var stream = File.Create(@"c:\temp\hej3.$$$", 0x10000))
                    Serializer.Serialize(stream, SerializeLargeValueObject.New(1000));
            SerializeLargeValueObject boomerang;
                using (var stream = new FileStream(@"c:\temp\hej3.$$$", FileMode.Open, FileAccess.Read, FileShare.Read, 0x10000))
                    boomerang = Serializer.Deserialize<SerializeLargeValueObject>(stream);

                Assert.AreEqual(thisData.First().A, boomerang.Data.First().A);
                Assert.AreEqual(thisData.Last().A, boomerang.Data.Last().A);
        }

    }

}