using System;

namespace NoMod.Debugging.Vectored
{
    using static NoMod.Win32.Kernel32;

    public class ThreadContextX86 : IThreadContextX86, IThreadContext
    {
        private CONTEXT _context;

        public ThreadContextX86(ref CONTEXT context)
        {
            _context = context;
        }

        public void ToNative(ref CONTEXT context)
        {
            context = _context;
        }

        public uint Dr0 { get => _context.Dr0; set => _context.Dr0 = value; }
        public uint Dr1 { get => _context.Dr1; set => _context.Dr1 = value; }
        public uint Dr2 { get => _context.Dr2; set => _context.Dr2 = value; }
        public uint Dr3 { get => _context.Dr3; set => _context.Dr3 = value; }
        public uint Dr6 { get => _context.Dr6; set => _context.Dr6 = value; }
        public uint Dr7 { get => _context.Dr7; set => _context.Dr7 = value; }

        IntPtr IThreadContext.Dr0 { get => new IntPtr(Dr0); set => Dr0 = (uint)value; }
        IntPtr IThreadContext.Dr1 { get => new IntPtr(Dr1); set => Dr1 = (uint)value; }
        IntPtr IThreadContext.Dr2 { get => new IntPtr(Dr2); set => Dr2 = (uint)value; }
        IntPtr IThreadContext.Dr3 { get => new IntPtr(Dr3); set => Dr3 = (uint)value; }
        IntPtr IThreadContext.Dr6 { get => new IntPtr(Dr6); set => Dr6 = (uint)value; }
        IntPtr IThreadContext.Dr7 { get => new IntPtr(Dr7); set => Dr7 = (uint)value; }

        public IFloatingSaveArea FloatSave => throw new NotImplementedException();

        public uint Gs { get => _context.SegGs; set => _context.SegGs = value; }
        public uint Fs { get => _context.SegFs; set => _context.SegFs = value; }
        public uint Es { get => _context.SegEs; set => _context.SegEs = value; }
        public uint Ds { get => _context.SegDs; set => _context.SegDs = value; }
        public uint Edi { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public uint Esi { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public uint Ebx { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public uint Edx { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public uint Ecx { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public uint Eax { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public uint Ebp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public uint Eip { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public uint Cs { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public uint EFlags { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public uint Esp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public uint Ss { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public byte[] ExtendedRegisters => throw new NotImplementedException();
    }
}
