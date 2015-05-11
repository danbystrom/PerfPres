using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using ProtoBuf;

namespace JimmyPresentation
{
    // protobuf refuses to serialize List<TestStruct[]>.... :-(
    [ProtoContract]
    [Serializable]
    public class SerializeTestClass
    {
        [ProtoMember(1)]
        public TestStruct[] W = Hej.PlayStruct();
    }


    [ProtoContract]
    public class TestClass
    {
        public decimal A;
        public decimal B;
        public decimal C;
        public decimal D;
        public decimal E;
        public decimal F;
        public decimal G;
        public decimal H;
    }

    [Serializable]
    [ProtoContract]
    public struct TestStruct
    {
        [ProtoMember(1)]
        public decimal A;
        [ProtoMember(2)]
        public decimal B;
        [ProtoMember(3)]
        public decimal C;
        [ProtoMember(4)]
        public decimal D;
        [ProtoMember(5)]
        public decimal E;
        [ProtoMember(6)]
        public decimal F;
        [ProtoMember(7)]
        public decimal G;
        [ProtoMember(8)]
        public decimal H;
    }

    public static class Hej
    {
        public static TestClass[] PlayClass()
        {
            var array = new TestClass[1000000];
            for (var i = 0; i < array.Length; i++)
                array[i] = new TestClass {H = 1000000*i+100};
            for (var i = array.Length - 2; i >= 0; i--)
                array[i].G = array[i + 1].H;
            for (var i = array.Length - 2; i >= 0; i--)
                array[i].F = array[i + 1].G;
            for (var i = array.Length - 2; i >= 0; i--)
                array[i].E = array[i + 1].F;
            for (var i = array.Length - 2; i >= 0; i--)
                array[i].D = array[i + 1].E;
            for (var i = array.Length - 2; i >= 0; i--)
                array[i].C = array[i + 1].D;
            for (var i = array.Length - 2; i >= 0; i--)
                array[i].B = array[i + 1].C;
            for (var i = array.Length - 2; i >= 0; i--)
                array[i].A = array[i + 1].B;
            return array;
        }

        public static TestStruct[] PlayStruct()
        {
            var array = new TestStruct[1000000];
            for (var i = 0; i < array.Length; i++)
                array[i].H = 1000000*i+100;
            for (var i = array.Length - 2; i >= 0; i--)
                array[i].G = array[i + 1].H;
            for (var i = array.Length - 2; i >= 0; i--)
                array[i].F = array[i + 1].G;
            for (var i = array.Length - 2; i >= 0; i--)
                array[i].E = array[i + 1].F;
            for (var i = array.Length - 2; i >= 0; i--)
                array[i].D = array[i + 1].E;
            for (var i = array.Length - 2; i >= 0; i--)
                array[i].C = array[i + 1].D;
            for (var i = array.Length - 2; i >= 0; i--)
                array[i].B = array[i + 1].C;
            for (var i = array.Length - 2; i >= 0; i--)
                array[i].A = array[i + 1].B;
            return array;
        }

        public static void SaveTestStructArrayListToStream(List<TestStruct[]> list, BinaryWriter bw)
        {
            byte[] buffer = null;
            foreach( var x in list)
            {
                bw.Write(x.Length);
                var size = x.CopyToByteArray(ref buffer);
                bw.Write(buffer, 0, size);
            }
            bw.Write(0);
        }

        public static void LoadTestStructArrayListFromStream(List<TestStruct[]> list, BinaryReader br)
        {
            int length;
            while((length = br.ReadInt32())!=0)
            {
                var x = new TestStruct[length];
                var size = length*Marshal.SizeOf(typeof (TestStruct));
                x.CopyFromByteArray(br.ReadBytes(size));
                list.Add(x);
            }
        }

    }

}
