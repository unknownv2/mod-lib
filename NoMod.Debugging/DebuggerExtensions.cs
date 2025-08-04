using System;

namespace NoMod.Debugging
{
    public static class DebuggerExtensions
    {
        public static IBreakpoint SetBreakpoint(this IDebugger debugger, object address, Trigger trigger)
        {
            return debugger.SetBreakpoint(new IntPtr((long)address), trigger);
        }

        public static void Detour(this BreakpointEventArgs e, object detour)
        {
            e.Detour(new IntPtr((long)detour));
        }

        public static void Detour(this BreakpointEventArgs e, IntPtr detour)
        {
            if (e.IsX64)
            {
                e.ContextX64.Rip = (ulong)detour.ToInt64();
            }
            else
            {
                e.ContextX86.Eip = (uint)detour.ToInt32();
            }
        }

        public static IDisposable Detour(this IBreakpoint breakpoint, IntPtr detour)
        {
            return new BreakpointDetour(breakpoint, detour);
        }

        private sealed class BreakpointDetour : IDisposable
        {
            private readonly IBreakpoint _breakpoint;
            private readonly IntPtr _detour;

            public BreakpointDetour(IBreakpoint breakpoint, IntPtr detour)
            {
                _breakpoint = breakpoint;
                _detour = detour;
                breakpoint.Hit += OnHit;
            }

            public void Dispose()
            {
                _breakpoint.Hit -= OnHit;
            }

            public void OnHit(object s, BreakpointEventArgs e)
            {
                e.Detour(_detour);
            }
        }
    }
}
