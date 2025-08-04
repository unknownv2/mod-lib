using System;
using System.Collections;
using System.Collections.Generic;

namespace NoMod.Memory
{
    internal abstract class Scanner : IEnumerator<IntPtr>
    {
        private static readonly IntPtr NegativeOne = new IntPtr(-1);

        protected readonly byte?[] _bytes;
        protected readonly IntPtr _startAddress;
        protected readonly IntPtr _endAddress;
        protected IntPtr _address;

        public Scanner(byte?[] bytes, IntPtr startAddress, IntPtr endAddress)
        {
            _bytes = bytes;
            _startAddress = startAddress;
            _endAddress = endAddress;
            Reset();
        }

        public IntPtr Current => _address;
        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            _address = Next();
            return _address != NegativeOne;
        }

        protected abstract IntPtr Next();

        public void Reset()
        {
            _address = _startAddress;
        }

        public virtual void Dispose()
        {

        }
    }
}
