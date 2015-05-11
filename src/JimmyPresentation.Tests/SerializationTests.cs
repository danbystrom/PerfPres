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
        [Test, Explicit]
        public void X()
        {
            var x = new TestStruct[1];
            x[0].E = 42;
            SqlPersistence.Save(x);
        }
    }
}