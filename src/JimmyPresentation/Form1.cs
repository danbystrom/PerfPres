using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using ProtoBuf;

namespace JimmyPresentation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void benchmark(string text, Func<object> action)
        {
            var mem = GC.GetTotalMemory(true);
            var sw = Stopwatch.StartNew();
            var result = action();
            textBox1.AppendText(string.Format("{0}: {1:0.000}  {2:0.00}Mb\r\n", text, sw.Elapsed.TotalSeconds, (GC.GetTotalMemory(false) - mem) / (1024 * 1024.0)));
            if (result.ToString() == "kalle")
                textBox1.AppendText("Hej Kalle!\r\n");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var x = new TestStruct[100];
            var y = Marshal.SizeOf(typeof(TestStruct));
            benchmark("class[]", () =>
            {
                var list = new List<TestClass[]>();
                for (var i = 0; i < 5; i++)
                    list.Add(Hej.PlayClass());
                return list;
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            benchmark("struct[]", () =>
            {
                var list = new List<TestStruct[]>();
                for (var i = 0; i < 5; i++)
                    list.Add(Hej.PlayStruct());
                return list;
            });
        }

        private void button4_Click(object sender, EventArgs e)
        {
            benchmark("double[]", () =>
            {
                var list = new List<double[]>();
                for (var i = 0; i < 100; i++)
                {
                    var array = new double[100000];
                    list.Add(array);
                    for (var j = 0; j < 100; j++)
                        TestLength.FillMetersArray(array);
                }
                return list.Sum(array => TestLength.ConverToMiles(array).Sum());
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            benchmark("LengthClass[]", () =>
            {
                var list = new List<LengthClass[]>();
                for (var i = 0; i < 100; i++)
                {
                    var array = new LengthClass[100000];
                    list.Add(array);
                    for (var j = 0; j < 100; j++)
                        TestLength.FillLengthClassArray(array);
                }
                return list.Sum(array => TestLength.ConverToMiles(array).Sum());
            });
        }

        private void button5_Click(object sender, EventArgs e)
        {
            benchmark("Length[]", () =>
            {
                var list = new List<Length[]>();
                for (var i = 0; i < 100; i++)
                {
                    var array = new Length[100000];
                    list.Add(array);
                    for (var j = 0; j < 100; j++)
                        TestLength.FillLengthArray(array);
                }
                return list.Sum(array => TestLength.ConverToMiles(array).Sum());
            });
        }

        private void btnSerializeSql_Click(object sender, EventArgs e)
        {
            benchmark("serialize SQL", () =>
            {
                SqlPersistance.Save(new SerializeTestClass().W);
                return 0;
            });
        }

        private void btnDeserializeSql_Click(object sender, EventArgs e)
        {
            benchmark("deserialize SQL", () =>
            {
                var data = SqlPersistance.Load(1000000);
                return 0;
            });
        }

        private void btnSerializeJson_Click(object sender, EventArgs e)
        {
            benchmark("serialize Json", () =>
            {
                using (var stream = File.Create(@"c:\temp\hej1.$$$", 0x10000))
                using (var tw = new StreamWriter(stream, Encoding.UTF8, 0x10000))
                    JsonSerializer.CreateDefault().Serialize(tw, new SerializeTestClass());
                return 0;
            });
        }

        private void btnDeserializeJson_Click(object sender, EventArgs e)
        {
            benchmark("deserialize Json", () =>
            {
                SerializeTestClass data;
                using (var stream = new FileStream(@"c:\temp\hej1.$$$", FileMode.Open, FileAccess.Read, FileShare.Read, 0x10000))
                using (var tr = new StreamReader(stream))
                    data = new JsonSerializer().Deserialize<SerializeTestClass>(new JsonTextReader(tr));
                return data;
            });
        }

        private void btnSerializeBinaryFormatter_Click(object sender, EventArgs e)
        {
            benchmark("serialize BinaryFormatter", () =>
            {
                using (var stream = File.Create(@"c:\temp\hej2.$$$", 0x10000))
                    new BinaryFormatter().Serialize(stream, new SerializeTestClass());
                return 0;
            });
        }

        private void btnDeserializeBinaryFormatter_Click(object sender, EventArgs e)
        {
            benchmark("deserialize BinaryFormatter", () =>
            {
                SerializeTestClass data;
                using (var stream = new FileStream(@"c:\temp\hej2.$$$", FileMode.Open, FileAccess.Read, FileShare.Read, 0x10000))
                    data = (SerializeTestClass)(new BinaryFormatter().Deserialize(stream));
                return data;
            });
        }

        private void btnSerializeProtoBuf_Click(object sender, EventArgs e)
        {
            benchmark("serialize ProtoBuf", () =>
            {
                using (var stream = File.Create(@"c:\temp\hej3.$$$", 0x10000))
                    Serializer.Serialize(stream, new SerializeTestClass());
                return 0;
            });
        }

        private void btnDeserializeProtoBuf_Click(object sender, EventArgs e)
        {
            benchmark("deserialize ProtoBuf", () =>
            {
                SerializeTestClass data;
                using (var stream = new FileStream(@"c:\temp\hej3.$$$", FileMode.Open, FileAccess.Read, FileShare.Read, 0x10000))
                    data = Serializer.Deserialize<SerializeTestClass>(stream);
                return data.W[0].A;
            });

        }

        private void btnSerializeStructArray_Click(object sender, EventArgs e)
        {
            var thisData = new List<TestStruct[]> {new SerializeTestClass().W};
            benchmark("serialize struct[]", () =>
            {
                using (var stream = File.Create(@"c:\temp\hej4.$$$", 0x10000))
                using (var bw = new BinaryWriter(stream))
                    Hej.SaveTestStructArrayListToStream(thisData, bw);
                return 0;
            });
        }

        private void btnDeserializeStructArray_Click(object sender, EventArgs e)
        {
            benchmark("deserialize struct[]", () =>
            {
                var data = new List<TestStruct[]>();
                using (var stream = new FileStream(@"c:\temp\hej4.$$$", FileMode.Open, FileAccess.Read, FileShare.Read, 0x10000))
                using (var br = new BinaryReader(stream))
                    Hej.LoadTestStructArrayListFromStream(data, br);
                return data;
            });
        }

    }
}
