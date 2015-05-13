using System.Runtime.InteropServices;

namespace JimmyPresentation
{
    public static class ByteArrayCopy
    {
        /// <summary>
        /// Copies the content of a struct array to a plain byte array. Make sure the the struct doesn't contain references. This overload takes the destination byte array as input.
        /// </summary>
        /// <typeparam name="T">A struct. Should not contain reference types.</typeparam>
        /// <param name="source">The source array.</param>
        /// <param name="destination">The destination. It will grow as necessary, but not shrink.</param>
        /// <param name="elements">Optional. Then number of elements to copy</param>
        /// <returns>The usable length of the byte array.</returns>
        public static int CopyToByteArray<T>(this T[] source, ref byte[] destination, int? elements = null) where T : struct
        {
            var size = elements.GetValueOrDefault(source.Length) * Marshal.SizeOf(typeof(T));
            if (destination == null || destination.Length < size)
                destination = new byte[size];

            var handle = GCHandle.Alloc(source, GCHandleType.Pinned); 
            Marshal.Copy(Marshal.UnsafeAddrOfPinnedArrayElement(source, 0), destination, 0, size);
            handle.Free();

            return size;
        }

        /// <summary>
        /// Copies the content of a struct array to a new plain byte array. Make sure the the struct doesn't contain references.
        /// </summary>
        /// <typeparam name="T">A struct. Should not contain reference types.</typeparam>
        /// <param name="source">The source array.</param>
        /// <param name="elements">Optional. Then number of elements to copy.</param>
        /// <returns>The newly allocated byte array.</returns>
        public static byte[] CopyToByteArray<T>(this T[] source, int? elements = null) where T : struct
        {
            byte[] result = null;
            source.CopyToByteArray(ref result, elements);
            return result;
        }

        /// <summary>
        /// Copies the content of a byte array into a struct array. Make sure the the struct doesn't contain references. This overload takes the destination struct array as input.
        /// </summary>
        /// <typeparam name="T">A struct. Should not contain reference types.</typeparam>
        /// <param name="source">A byte array.</param>
        /// <param name="destination">The struct array to receive the content of the byte array</param>
        /// <param name="elements">Optional. The number of elements to copy.</param>
        /// <returns></returns>
        public static int CopyToStructArray<T>(this byte[] source, ref T[] destination, int? elements = null) where T: struct
        {
            var realElements = elements.GetValueOrDefault(source.Length/Marshal.SizeOf(typeof (T)));
            if(destination == null || destination.Length<realElements)
                destination = new T[realElements];

            var handle = GCHandle.Alloc(destination, GCHandleType.Pinned);
            Marshal.Copy(source, 0, Marshal.UnsafeAddrOfPinnedArrayElement(destination, 0), realElements * Marshal.SizeOf(typeof(T)));
            handle.Free();

            return realElements;
        }

        /// <summary>
        /// Copies the content of a byte array to a new struct array. Make sure the the struct doesn't contain references.
        /// </summary>
        /// <typeparam name="T">A struct. Should not contain reference types.</typeparam>
        /// <param name="source">A struct array.</param>
        /// <param name="elements">Optional. The number of elements to copy.</param>
        /// <returns></returns>
        public static T[] CopyToStructArray<T>(this byte[] source, int? elements = null) where T : struct
        {
            T[] result = null;
            source.CopyToStructArray(ref result, elements);
            return result;
        }

    }

}
