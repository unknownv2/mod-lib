using NoMod.Debugging;
using System;
using System.Runtime.InteropServices;

namespace NoMod.Trainer.Legacy.Adapters
{
    [ComVisible(true)]
    internal interface IDebuggerAdapter
    {
        void AddBreakpointHandler(IntPtr handler);
        void RemoveBreakpointHandler(IntPtr handler);
        bool SetBreakpoint(IntPtr address, byte trigger);
        bool UnsetBreakpoint(IntPtr address);
        bool IsBreakpointSet(IntPtr address, out byte trigger);
    }

    public class DebuggerAdapter : IDebuggerAdapter
    {
        private readonly IDebugger _debugger;

        public DebuggerAdapter(IDebugger debugger)
        {
            _debugger = debugger;
        }

        public void AddBreakpointHandler(IntPtr handler)
        {
            throw new NotImplementedException();
        }

        public bool IsBreakpointSet(IntPtr address, out byte trigger)
        {
            throw new NotImplementedException();
        }

        public void RemoveBreakpointHandler(IntPtr handler)
        {
            throw new NotImplementedException();
        }

        public bool SetBreakpoint(IntPtr address, byte trigger)
        {
            throw new NotImplementedException();
        }

        public bool UnsetBreakpoint(IntPtr address)
        {
            throw new NotImplementedException();
        }
    }
}
