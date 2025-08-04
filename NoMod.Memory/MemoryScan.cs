using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace NoMod.Memory
{
    public sealed class MemoryScan : IEnumerable<IntPtr>
    {
        internal static readonly IntPtr End = new IntPtr(-1);

        public int ProcessId { get; }
        public byte?[] Bytes { get; }
        public IntPtr StartAddress { get; }
        public IntPtr EndAddress { get; }

        public MemoryScan(byte?[] bytes, IntPtr startAddress, IntPtr endAddress)
            : this(Process.GetCurrentProcess().Id, bytes, startAddress, endAddress)
        {
            
        }

        public MemoryScan(int processId, byte?[] bytes, IntPtr startAddress, IntPtr endAddress)
        {
            ProcessId = processId;
            Bytes = bytes;
            StartAddress = startAddress;
            EndAddress = endAddress;
        }

        public IEnumerator<IntPtr> GetEnumerator()
        {
            return Process.GetCurrentProcess().Id == ProcessId
                ? (Scanner)new LocalScanner(Bytes, StartAddress, EndAddress)
                : new RemoteScanner(ProcessId, Bytes, StartAddress, EndAddress);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
