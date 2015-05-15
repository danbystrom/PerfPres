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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text += ((IntPtr.Size == 4) ? "32-bit" : "64-bit");
        }

        private void benchmark(string description, object button, Func<object> action)
        {
            ((Button)button).Text = description;
            Cursor.Current = Cursors.WaitCursor;
            var mem = GC.GetTotalMemory(true);
            var sw = Stopwatch.StartNew();
            var result = action();
            textBox1.AppendText(string.Format("{0}: {1:0.000}  {2:0.00}MB\r\n", description, sw.Elapsed.TotalSeconds, (GC.GetTotalMemory(false) - mem) / (1024 * 1024.0)));
            if (result!=null && result.ToString() == "kalle")
                textBox1.AppendText("FillUpSomeData Kalle!\r\n");
            Cursor.Current = Cursors.Default;
        }

        private void btnLargeClassArray_Click(object sender, EventArgs e)
        {
            benchmark("Large instances in class[]", sender, () =>
            {
                return LargeValueObjectHelper.PlayWithClass(5);
            });
        }

        private void btnLargeStructArray_Click(object sender, EventArgs e)
        {
            benchmark("Large instances in struct[]", sender, () =>
            {
                return LargeValueObjectHelper.PlayWithStructList(5);
            });
        }

        private void btnDoubleArray_Click(object sender, EventArgs e)
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

        private void btnEncapsulatedDoubleInClass_Click(object sender, EventArgs e)
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

        private void btnEncapsulatedDoubleInStruct_Click(object sender, EventArgs e)
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
                SqlPersistence.Save(SerializeLargeValueObject.New().Data);
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
                    JsonSerializer.CreateDefault().Serialize(tw, SerializeLargeValueObject.New());
                return 0;
            });
            fixGroupText1();
        }

        private void btnDeserializeJson_Click(object sender, EventArgs e)
        {
            benchmark("deserialize Json", sender, () =>
            {
                SerializeLargeValueObject data;
                using (var stream = new FileStream(@"c:\temp\hej1.$$$", FileMode.Open, FileAccess.Read, FileShare.Read, 0x10000))
                using (var tr = new StreamReader(stream))
                    data = new JsonSerializer().Deserialize<SerializeLargeValueObject>(new JsonTextReader(tr));
                return data;
            });
            fixGroupText2();
        }

        private void btnSerializeBinaryFormatter_Click(object sender, EventArgs e)
        {
            benchmark("serialize BinaryFormatter", sender, () =>
            {
                using (var stream = File.Create(@"c:\temp\hej2.$$$", 0x10000))
                    new BinaryFormatter().Serialize(stream, SerializeLargeValueObject.New());
                return 0;
            });
            fixGroupText1();
        }

        private void btnDeserializeBinaryFormatter_Click(object sender, EventArgs e)
        {
            benchmark("deserialize BinaryFormatter", sender, () =>
           {
               SerializeLargeValueObject data;
                using (var stream = new FileStream(@"c:\temp\hej2.$$$", FileMode.Open, FileAccess.Read, FileShare.Read, 0x10000))
                    data = (SerializeLargeValueObject)(new BinaryFormatter().Deserialize(stream));
                return data;
            });
            fixGroupText2();
        }

        private void btnSerializeProtoBuf_Click(object sender, EventArgs e)
        {
            benchmark("serialize ProtoBuf", sender, () =>
            {
                using (var stream = File.Create(@"c:\temp\hej3.$$$", 0x10000))
                    Serializer.Serialize(stream, SerializeLargeValueObject.New());
                return 0;
            });
            fixGroupText1();
        }

        private void btnDeserializeProtoBuf_Click(object sender, EventArgs e)
        {
            benchmark("deserialize ProtoBuf", sender, () =>
           {
               SerializeLargeValueObject data;
                using (var stream = new FileStream(@"c:\temp\hej3.$$$", FileMode.Open, FileAccess.Read, FileShare.Read, 0x10000))
                    data = Serializer.Deserialize<SerializeLargeValueObject>(stream);
                return data.Data[0].A;
            });
            fixGroupText2();
        }

        private void btnSerializeStructArray_Click(object sender, EventArgs e)
        {
            var thisData = new List<LargeValueObjectAsStruct[]> { SerializeLargeValueObject.New().Data };
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
                var data = new List<LargeValueObjectAsStruct[]>();
                using (var stream = new FileStream(@"c:\temp\hej4.$$$", FileMode.Open, FileAccess.Read, FileShare.Read, 0x10000))
                using (var br = new BinaryReader(stream))
                    LargeValueObjectHelper.LoadTestStructArrayListFromStream(data, br);
                return data;
            });
            fixGroupText2();
        }

        private void btnDoubleClassArray_Click(object sender, EventArgs e)
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
            });
        }

        private void btnDoubleUnit_Click(object sender, EventArgs e)
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
            });
        }

        private void btnDoubleStructArray_Click(object sender, EventArgs e)
        {
            benchmark("double in a struct[]", sender, () =>
            {
                var list = new List<Length[]>();
                for (var i = 0; i < 10; i++)
                {
                    var array = new Length[100000];
                    list.Add(array);
                    for (var j = 0; j < 100; j++)
                        TestLength.FillLengthArray(array);
                }

                double sumOfAverages = 0;
                for (int i = 0; i < 30; i++)
                {
                    var average = list.Average(array => TestLength.ConvertToMiles(array).Average());
                    sumOfAverages += average;
                }
                return sumOfAverages;
            });

        }

    }
}
