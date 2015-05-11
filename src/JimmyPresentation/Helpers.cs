using System;
using System.Runtime.InteropServices;

namespace JimmyPresentation
{
    public static class Helpers
    {
        public static int CopyToByteArray<T>(this T[] array, ref byte[] buffer) where T : struct
        {
            var size = array.Length * Marshal.SizeOf(typeof(T));
            if (buffer == null || buffer.Length < size)
                buffer = new byte[size];

            var h = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            Marshal.Copy(h.AddrOfPinnedObject(), buffer, 0, size);
            h.Free();
            return size;
        }

        public static void CopyFromByteArray<T>(this T[] array, byte[] buffer, int? elements = null) where T : struct
        {
            var size = elements.GetValueOrDefault(array.Length) * Marshal.SizeOf(typeof(T));
            if(size > buffer.Length)
                throw new IndexOutOfRangeException();

            var h = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            Marshal.Copy(buffer, 0, h.AddrOfPinnedObject(), size);
            h.Free();
        }

    }

}
