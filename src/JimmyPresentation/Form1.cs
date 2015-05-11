using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        
        private void benchmark(string description, object button, Func<object> action)
        {
            ((Button)button).Text = description;
            Cursor.Current = Cursors.WaitCursor;
            var mem = GC.GetTotalMemory(true);
            var sw = Stopwatch.StartNew();
            var result = action();
            textBox1.AppendText(string.Format("{0}: {1:0.000}  {2:0.00}MB\r\n", description, sw.Elapsed.TotalSeconds, (GC.GetTotalMemory(false) - mem) / (1024 * 1024.0)));
            if (result.ToString() == "kalle")
                textBox1.AppendText("Hej Kalle!\r\n");
            Cursor.Current = Cursors.Default;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            benchmark("Large instances in class[]", sender, () =>
            {
                //var list = new List<TestClass[]>();
                //for (var i = 0; i < 5; i++)
                //    list.Add(Hej.PlayClass());
                //return list;

                var list = new List<LargeValueObjectAsClass[]>();
                for (var i = 0; i < 5; i++)
                    list.Add(LargeValueObjectHelper.PlayClass());
                return list;
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            benchmark("Large instances in struct[]", sender, () =>
            {
                //var list = new List<TestStruct[]>();
                //for (var i = 0; i < 5; i++)
                //    list.Add(Hej.PlayStruct());
                //return list;
                var list = new List<LargeValueObjectAsStruct[]>();
                for (var i = 0; i < 5; i++)
                    list.Add(LargeValueObjectHelper.PlayStruct());
                return list;
            });
        }

        private void button4_Click(object sender, EventArgs e)
        {
            benchmark("double[]", sender, () =>
            {
                var list = new List<double[]>();
                for (var i = 0; i < 100; i++)
                {
                    var array = new double[100000];
                    list.Add(array);
                    for (var j = 0; j < 100; j++)
                        TestLength.FillMetersArray(array);
                }
                return list.Sum(array => TestLength.ConvertToMiles(array).Sum());
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            benchmark("double in a class[]", sender, () =>
            {
                var list = new List<LengthClass[]>();
                for (var i = 0; i < 100; i++)
                {
                    var array = new LengthClass[100000];
                    list.Add(array);
                    for (var j = 0; j < 100; j++)
                        TestLength.FillLengthClassArray(array);
                }
                return list.Sum(array => TestLength.ConvertToMiles(array).Sum());
            });
        }

        private void button5_Click(object sender, EventArgs e)
        {
            benchmark("double in a struct[]", sender, () =>
            {
                var list = new List<Length[]>();
                for (var i = 0; i < 100; i++)
                {
                    var array = new Length[100000];
                    list.Add(array);
                    for (var j = 0; j < 100; j++)
                        TestLength.FillLengthArray(array);
                }
                return list.Sum(array => TestLength.ConvertToMiles(array).Sum());
            });
        }

        private void btnSerializeSql_Click(object sender, EventArgs e)
        {
            SqlPersistence.CleanUp();

            benchmark("serialize SQL", sender, () =>
            {
                SqlPersistence.Save(new SerializeTestClass().W);
                return 0;
            });
            fixGroupText1();
        }
        private void fixGroupText1()
        {
            groupBox1.Text = @"Serialize";
        }
        private void fixGroupText2()
        {
            groupBox2.Text = @"Deserialize";
        }

        private void btnDeserializeSql_Click(object sender, EventArgs e)
        {
            benchmark("deserialize SQL", sender, () =>
            {
                SqlPersistence.Load(1000000);
                return 0;
            });
            fixGroupText2();
        }

        private void btnSerializeJson_Click(object sender, EventArgs e)
        {
            benchmark("serialize Json", sender, () =>
            {
                using (var stream = File.Create(@"c:\temp\hej1.$$$", 0x10000))
                using (var tw = new StreamWriter(stream, Encoding.UTF8, 0x10000))
                    JsonSerializer.CreateDefault().Serialize(tw, new SerializeTestClass());
                return 0;
            });
            fixGroupText1();
        }

        private void btnDeserializeJson_Click(object sender, EventArgs e)
        {
            benchmark("deserialize Json", sender, () =>
            {
                SerializeTestClass data;
                using (var stream = new FileStream(@"c:\temp\hej1.$$$", FileMode.Open, FileAccess.Read, FileShare.Read, 0x10000))
                using (var tr = new StreamReader(stream))
                    data = new JsonSerializer().Deserialize<SerializeTestClass>(new JsonTextReader(tr));
                return data;
            });
            fixGroupText2();
        }

        private void btnSerializeBinaryFormatter_Click(object sender, EventArgs e)
        {
            benchmark("serialize BinaryFormatter", sender, () =>
            {
                using (var stream = File.Create(@"c:\temp\hej2.$$$", 0x10000))
                    new BinaryFormatter().Serialize(stream, new SerializeTestClass());
                return 0;
            });
            fixGroupText1();
        }

        private void btnDeserializeBinaryFormatter_Click(object sender, EventArgs e)
        {
            benchmark("deserialize BinaryFormatter", sender, () =>
            {
                SerializeTestClass data;
                using (var stream = new FileStream(@"c:\temp\hej2.$$$", FileMode.Open, FileAccess.Read, FileShare.Read, 0x10000))
                    data = (SerializeTestClass)(new BinaryFormatter().Deserialize(stream));
                return data;
            });
            fixGroupText2();
        }

        private void btnSerializeProtoBuf_Click(object sender, EventArgs e)
        {
            benchmark("serialize ProtoBuf", sender, () =>
            {
                using (var stream = File.Create(@"c:\temp\hej3.$$$", 0x10000))
                    Serializer.Serialize(stream, new SerializeTestClass());
                return 0;
            });
            fixGroupText1();
        }

        private void btnDeserializeProtoBuf_Click(object sender, EventArgs e)
        {
            benchmark("deserialize ProtoBuf", sender, () =>
            {
                SerializeTestClass data;
                using (var stream = new FileStream(@"c:\temp\hej3.$$$", FileMode.Open, FileAccess.Read, FileShare.Read, 0x10000))
                    data = Serializer.Deserialize<SerializeTestClass>(stream);
                return data.W[0].A;
            });
            fixGroupText2();
        }

        private void btnSerializeStructArray_Click(object sender, EventArgs e)
        {
            //var thisData = new List<TestStruct[]> {new SerializeTestClass().W};
            //benchmark("serialize struct[]", sender, () =>
            //{
            //    using (var stream = File.Create(@"c:\temp\hej4.$$$", 0x10000))
            //    using (var bw = new BinaryWriter(stream))
            //        Hej.SaveTestStructArrayListToStream(thisData, bw);
            //    return 0;
            //});
            var thisData = new List<LargeValueObjectAsStruct[]> { new SerializeLargeValueObject().W };
            benchmark("serialize struct[]", sender, () =>
            {
                using (var stream = File.Create(@"c:\temp\hej4.$$$", 0x10000))
                using (var bw = new BinaryWriter(stream))
                    LargeValueObjectHelper.SaveTestStructArrayListToStream(thisData, bw);
                return 0;
            });

            fixGroupText1();
        }

        private void btnDeserializeStructArray_Click(object sender, EventArgs e)
        {
            benchmark("deserialize struct[]", sender, () =>
            {
                //var data = new List<TestStruct[]>();
                //using (var stream = new FileStream(@"c:\temp\hej4.$$$", FileMode.Open, FileAccess.Read, FileShare.Read, 0x10000))
                //using (var br = new BinaryReader(stream))
                //    Hej.LoadTestStructArrayListFromStream(data, br);
                //return data;
                var data = new List<LargeValueObjectAsStruct[]>();
                using (var stream = new FileStream(@"c:\temp\hej4.$$$", FileMode.Open, FileAccess.Read, FileShare.Read, 0x10000))
                using (var br = new BinaryReader(stream))
                    LargeValueObjectHelper.LoadTestStructArrayListFromStream(data, br);
                return data;
            });
            fixGroupText2();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            benchmark("double in a class[]", sender, () =>
            {
                var list = new List<LengthClass[]>();
                for (var i = 0; i < 10; i++)
                {
                    var array = new LengthClass[100000];
                    list.Add(array);
                    for (var j = 0; j < 100; j++)
                        TestLength.FillLengthClassArray(array);               
                }

                double sumOfAverages = 0;
                for (int i = 0; i < 30; i++)
                {
                    var average = list.Average(array => TestLength.ConvertToMiles(array).Average());
                    sumOfAverages += average;
                }
                return sumOfAverages;
                //var sum = list.Sum(array => TestLength.ConvertToMiles(array).Sum());
                //var average = list.Average(array => TestLength.ConvertToMiles(array).Average());
                //var average2 = list.Average(array => TestLength.ConvertToMiles(array).Average());
                //var average3 = list.Average(array => TestLength.ConvertToMiles(array).Average());
                //var min = list.Min(array => TestLength.ConvertToMiles(array).Min());
                //var max = list.Average(array => TestLength.ConvertToMiles(array).Max());

                //return sum + average + average2 + average3 + min + max;
            });

        }

        private void button7_Click(object sender, EventArgs e)
        {
            benchmark("double+unit[]", sender, () =>
            {
                var list = new List<DoublePlusUnit[]>();
                for (var i = 0; i < 10; i++)
                {
                    var array = new DoublePlusUnit[100000];
                    list.Add(array);
                    for (var j = 0; j < 100; j++)
                        TestLength.FillDoublePlusUnitArrayWithMeters(array);
                }

                double sumOfAverages = 0;
                for (int i = 0; i < 30; i++)
                {
                    var average = list.Average(array => TestLength.ConvertToMiles(array).Average());
                    sumOfAverages += average;
                }
                return sumOfAverages;

                //var sum = list.Sum(array => TestLength.ConvertToMiles(array).Sum());
                //var average = list.Average(array => TestLength.ConvertToMiles(array).Average());
                //var average2 = list.Average(array => TestLength.ConvertToMiles(array).Average());
                //var average3 = list.Average(array => TestLength.ConvertToMiles(array).Average());
                //var min = list.Min(array => TestLength.ConvertToMiles(array).Min());
                //var max = list.Average(array => TestLength.ConvertToMiles(array).Max());

                //return sum + average + average2 + average3 + min + max;
            });
        }

    }
}
