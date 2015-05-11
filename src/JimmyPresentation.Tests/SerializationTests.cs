using System;
using System.Collections.Generic;
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
            var q = new TestStruct {A = 7};
            var z = new List<TestStruct[]> { new[] { q } };

            var ms = new MemoryStream();
            (new BinaryFormatter()).Serialize(ms, z);
            var b = ms.GetBuffer();

            ms.Seek(0, SeekOrigin.Begin);
            var c = (List<TestStruct[]>)(new BinaryFormatter()).Deserialize(ms);
        }
    }
}
