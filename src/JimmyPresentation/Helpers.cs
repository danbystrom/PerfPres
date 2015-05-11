using System;
using System.Runtime.InteropServices;

namespace JimmyPresentation
{
    public unsafe static class Helpers
    {
        public static int CopyToByteArray(this TestStruct[] array, ref byte[] buffer)
        {
            var size = array.Length * Marshal.SizeOf(typeof(TestStruct));
            if (buffer == null || buffer.Length < size)
                buffer = new byte[size];

            fixed(void* x = array)
                Marshal.Copy(new IntPtr(x), buffer, 0, size);
            return size;
        }

        public static void CopyFromByteArray(this TestStruct[] array, byte[] buffer, int? elements = null)
        {
            var size = elements.GetValueOrDefault(array.Length) * Marshal.SizeOf(typeof(TestStruct));
            if(size > buffer.Length)
                throw new IndexOutOfRangeException();

            fixed(void* x = array)
                Marshal.Copy(buffer, 0, new IntPtr(x), size);
        }

    }

}
