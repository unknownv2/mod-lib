using System;

namespace NoMod.Debugging
{
    public enum Trigger
    {
        Execute,
        Write,
        ReadWrite,
        Int3,
        PrivilegedOpcode,
        Dereference,
    }

    public interface IDebugger : IDisposable
    {
        IBreakpoint SetBreakpoint(IntPtr address, Trigger trigger);
    }

    public interface IBreakpoint : IDisposable
    {
        event EventHandler<BreakpointEventArgs> Hit;
        event EventHandler<BreakpointEventArgs> PostHit;
        Trigger Trigger { get; }
        IntPtr Address { get; }
    }

    public sealed class BreakpointEventArgs : EventArgs
    {
        public object Context { get; }
        public IThreadContextX86 ContextX86 => Context as IThreadContextX86;
        public IThreadContextX64 ContextX64 => Context as IThreadContextX64;

        public bool IsX86 => Context is IThreadContextX86;
        public bool IsX64 => Context is IThreadContextX64;
    }
}
