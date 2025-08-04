using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NoMod.Memory
{
    internal sealed class LocalScanner : Scanner
    {
        public LocalScanner(byte?[] bytes, IntPtr startAddress, IntPtr endAddress)
            : base(bytes, startAddress, endAddress)
        {

        }

        protected override IntPtr Next()
        {
            var bytes = new byte[_bytes.Length];
            for (var x = 0; x < _bytes.Length; x++)
            {
                bytes[x] = _bytes[x].Value;
            }

            //var vector = Unsafe.As<Vector<byte>>(bytes);

            throw new NotImplementedException();
        }
    }
}
