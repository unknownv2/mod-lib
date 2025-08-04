using System.Runtime.InteropServices;

namespace NoMod.Debugging
{
    public interface IThreadContextX64
    {
        ulong P1Home { get; set; }
        ulong P2Home { get; set; }
        ulong P3Home { get; set; }
        ulong P4Home { get; set; }
        ulong P5Home { get; set; }
        ulong P6Home { get; set; }

        uint MxCsr { get; set; }

        ushort Cs { get; set; }
        ushort Ds { get; set; }
        ushort Es { get; set; }
        ushort Fs { get; set; }
        ushort Gs { get; set; }
        ushort Ss { get; set; }
        uint EFlags { get; set; }

        // DebugRegisters
        ulong Dr0 { get; set; }
        ulong Dr1 { get; set; }
        ulong Dr2 { get; set; }
        ulong Dr3 { get; set; }
        ulong Dr6 { get; set; }
        ulong Dr7 { get; set; }

        // Integer
        ulong Rax { get; set; }
        ulong Rcx { get; set; }
        ulong Rdx { get; set; }
        ulong Rbx { get; set; }
        ulong Rsp { get; set; }
        ulong Rbp { get; set; }
        ulong Rsi { get; set; }
        ulong Rdi { get; set; }
        ulong R8 { get; set; }
        ulong R9 { get; set; }
        ulong R10 { get; set; }
        ulong R11 { get; set; }
        ulong R12 { get; set; }
        ulong R13 { get; set; }
        ulong R14 { get; set; }
        ulong R15 { get; set; }
        ulong Rip { get; set; }

        M128A[] VectorRegister { get; }
        ulong VectorControl { get; set; }

        ulong DebugControl { get; set; }
        ulong LastBranchToRip { get; set; }
        ulong LastBranchFromRip { get; set; }
        ulong LastExceptionToRip { get; set; }
        ulong LastExceptionFromRip { get; set; }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct M128A
    {
        public ulong High { get; set; }
        public long Low { get; set; }
    }
}
