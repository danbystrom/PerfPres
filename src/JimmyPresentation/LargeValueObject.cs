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
    public class SerializeLargeValueObject
    {
        [ProtoMember(1)]
        public LargeValueObjectAsStruct[] W = LargeValueObjectHelper.PlayStruct();
    }


    [ProtoContract]
    public class LargeValueObjectAsClass
    {
        public readonly decimal A;
        public readonly decimal B;
        public readonly decimal C;
        public readonly decimal D;
        public readonly decimal E;
        public readonly decimal F;
        public readonly decimal G;
        public readonly decimal H;

        public LargeValueObjectAsClass(decimal a, decimal b, decimal c, decimal d, decimal e, decimal f, decimal g,
                                       decimal h)
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

        public LargeValueObjectAsStruct(decimal a, decimal b, decimal c, decimal d, decimal e, decimal f, decimal g,
                                       decimal h)
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
        public static LargeValueObjectAsClass[] PlayClass()
        {
            var array = new LargeValueObjectAsClass[1000000];
            for (var i = 0; i < array.Length; i++)
                array[i] = new LargeValueObjectAsClass(1, 2, 3, 4, 5, 6, 7, 8);
            decimal sum = 0;
            for (var i = 0; i < array.Length; i++)
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
            return array;
        }

        public static LargeValueObjectAsStruct[] PlayStruct()
        {
            var array = new LargeValueObjectAsStruct[1000000];
            for (var i = 0; i < array.Length; i++)
                array[i] = new LargeValueObjectAsStruct(1, 2, 3, 4, 5, 6, 7, 8);
            decimal sum = 0;
            for (var i = 0; i < array.Length; i++)
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
            return array;
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
            {
                var x = new LargeValueObjectAsStruct[length];
                var size = length * Marshal.SizeOf(typeof(TestStruct));
                x.CopyFromByteArray(br.ReadBytes(size));
                list.Add(x);
            }
        }

    }
}
