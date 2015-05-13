using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProtoBuf;

namespace JimmyPresentation
{
    // protobuf refuses to serialize List<TestStruct[]>.... :-(
    [ProtoContract]
    [Serializable]
    public class SerializeLargeValueObject
    {
        [ProtoMember(1)]
        public LargeValueObjectAsStruct[] W = LargeValueObjectHelper.PlayWithStruct();
    }


    [ProtoContract]
    public class LargeValueObjectAsClass
    {
        public readonly double A;
        public readonly double B;
        public readonly double C;
        public readonly double D;
        public readonly double E;
        public readonly double F;
        public readonly double G;
        public readonly double H;

        public LargeValueObjectAsClass(double a, double b, double c, double d, double e, double f, double g, double h)
        {
            A = a;
            B = b;
            C = c;
            D = d;
            E = e;
            F = f;
            G = g;
            H = h;
        }
    }

    [Serializable]
    [ProtoContract]
    public struct LargeValueObjectAsStruct
    {
        [ProtoMember(1)] public double A;
        [ProtoMember(2)] public double B;
        [ProtoMember(3)] public double C;
        [ProtoMember(4)] public double D;
        [ProtoMember(5)] public double E;
        [ProtoMember(6)] public double F;
        [ProtoMember(7)] public double G;
        [ProtoMember(8)] public double H;

        public LargeValueObjectAsStruct(double a, double b, double c, double d, double e, double f, double g, double h)
        {
            A = a;
            B = b;
            C = c;
            D = d;
            E = e;
            F = f;
            G = g;
            H = h;
        }
    }

    public static class LargeValueObjectHelper
    {
        public static LargeValueObjectAsClass[] PlayWithClass()
        {
            var array = new LargeValueObjectAsClass[1000000];
            for (var i = 1; i < array.Length; i++)
                array[i] = new LargeValueObjectAsClass(1, 2, 3, 4, 5, 6, 7, 8);
            var sum = 0.0;
            for (var i = 1; i < array.Length; i++)
            {
                sum += array[i].A;
                sum += array[i].B;
                sum += array[i].C;
                sum += array[i].D;
                sum += array[i].E;
                sum += array[i].F;
                sum += array[i].G;
                sum += array[i].H;
            }
            array[0] = new LargeValueObjectAsClass(sum, 0, 0, 0, 0, 0, 0, 0);
            return array;
        }

        public static List<LargeValueObjectAsClass[]> PlayWithClass(int elements)
        {
            return Enumerable.Repeat(0, elements).Select(_ => PlayWithClass()).ToList();
        }

        public static LargeValueObjectAsStruct[] PlayWithStruct()
        {
            var array = new LargeValueObjectAsStruct[1000000];
            for (var i = 1; i < array.Length; i++)
                array[i] = new LargeValueObjectAsStruct(1, 2, 3, 4, 5, 6, 7, 8);
            var sum = 0.0;
            for (var i = 1; i < array.Length; i++)
            {
                sum += array[i].A;
                sum += array[i].B;
                sum += array[i].C;
                sum += array[i].D;
                sum += array[i].E;
                sum += array[i].F;
                sum += array[i].G;
                sum += array[i].H;
            }
            array[0] = new LargeValueObjectAsStruct(sum, 0, 0, 0, 0, 0, 0, 0);
            return array;
        }

        public static List<LargeValueObjectAsStruct[]> PlayWithStruct(int elements)
        {
            return Enumerable.Repeat(0, elements).Select(_ => PlayWithStruct()).ToList();
        }

        public static void SaveTestStructArrayListToStream(List<LargeValueObjectAsStruct[]> list, BinaryWriter bw)
        {
            byte[] buffer = null;
            foreach (var x in list)
            {
                bw.Write(x.Length);
                var size = x.CopyToByteArray(ref buffer);
                bw.Write(buffer, 0, size);
            }
            bw.Write(0);
        }

        public static void LoadTestStructArrayListFromStream(List<LargeValueObjectAsStruct[]> list, BinaryReader br)
        {
            int length;
            while ((length = br.ReadInt32()) != 0)
                list.Add(br.ReadBytes(length).CopyToStructArray<LargeValueObjectAsStruct>());
        }

    }

}
