using System;

namespace NoMod.Debugging.Vectored
{
    using static NoMod.Win32.Kernel32;

    public class ThreadContextX64 : IThreadContextX64, IThreadContext
    {
        private CONTEXT64 _context;

        public ThreadContextX64(ref CONTEXT64 context)
        {
            _context = context;
        }

        public void ToNative(ref CONTEXT64 context)
        {
            context = _context;
        }

        public ulong P1Home { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong P2Home { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong P3Home { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong P4Home { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong P5Home { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong P6Home { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public uint MxCsr { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ushort Cs { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ushort Ds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ushort Es { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ushort Fs { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ushort Gs { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ushort Ss { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public uint EFlags { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong Dr0 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong Dr1 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong Dr2 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong Dr3 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong Dr6 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong Dr7 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong Rax { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong Rcx { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong Rdx { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong Rbx { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong Rsp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong Rbp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong Rsi { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong Rdi { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong R8 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong R9 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong R10 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong R11 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong R12 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong R13 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong R14 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong R15 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong Rip { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        IntPtr IThreadContext.Dr0 { get => new IntPtr((long)Dr0); set => Dr0 = (ulong)value; }
        IntPtr IThreadContext.Dr1 { get => new IntPtr((long)Dr1); set => Dr1 = (ulong)value; }
        IntPtr IThreadContext.Dr2 { get => new IntPtr((long)Dr2); set => Dr2 = (ulong)value; }
        IntPtr IThreadContext.Dr3 { get => new IntPtr((long)Dr3); set => Dr3 = (ulong)value; }
        IntPtr IThreadContext.Dr6 { get => new IntPtr((long)Dr6); set => Dr6 = (ulong)value; }
        IntPtr IThreadContext.Dr7 { get => new IntPtr((long)Dr7); set => Dr7 = (ulong)value; }

        public M128A[] VectorRegister => throw new NotImplementedException();

        public ulong VectorControl { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong DebugControl { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong LastBranchToRip { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong LastBranchFromRip { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong LastExceptionToRip { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ulong LastExceptionFromRip { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
