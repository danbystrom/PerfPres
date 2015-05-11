using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

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

        private void button7_Click(object sender, EventArgs e)
        {
            var data = new List<TestStruct[]>
            {
                Hej.PlayStruct(),
                Hej.PlayStruct()
            };
            benchmark("serialize", () =>
            {
                using (var stream = File.Create(@"c:\temp\hej1.$$$", 0x10000))
                using (var bw = new BinaryWriter(stream))
                    Hej.SaveTestStructArrayListToStream(data, bw);
                return 0;
            });

            var data2 = new List<TestStruct[]>
            {
                Hej.PlayStruct(),
                Hej.PlayStruct(),
            };
            benchmark("serialize with BinaryFormatter", () =>
            {
                using (var stream = File.Create(@"c:\temp\hej2.$$$", 0x10000))
                    new BinaryFormatter().Serialize(stream, data2);
                return 0;
            });

        }

        private void button6_Click(object sender, EventArgs e)
        {
            benchmark("deserialize", () =>
            {
                var data = new List<TestStruct[]>(); 
                using (var stream = new FileStream(@"c:\temp\hej1.$$$", FileMode.Open, FileAccess.Read, FileShare.Read, 0x10000))
                using (var br = new BinaryReader(stream))
                    Hej.LoadTestStructArrayListFromStream(data, br);
                return data;
            });

            benchmark("deserialize with BinaryFormatter", () =>
            {
                List<TestStruct[]> data;
                using (var stream = new FileStream(@"c:\temp\hej2.$$$", FileMode.Open, FileAccess.Read, FileShare.Read, 0x10000))
                    data = (List<TestStruct[]>)(new BinaryFormatter().Deserialize(stream));
                return data;
            });

        }

    }
}
