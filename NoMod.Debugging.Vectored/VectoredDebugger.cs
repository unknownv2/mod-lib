using NoMod.Win32;
using System;
using System.Collections.Concurrent;

namespace NoMod.Debugging.Vectored
{
    using static Kernel32;

    public class VectoredDebugger : IDebugger
    {
        private const byte PrivilegedOpcode = 0xf4; // HALT

        private readonly ConcurrentDictionary<IntPtr, Breakpoint> _breakpoints;

        public VectoredDebugger()
        {
            if (Environment.Is64BitProcess)
            {
                AddVectoredExceptionHandler(1, VectoredExceptionHandler64);
            }
            else
            {
                AddVectoredExceptionHandler(1, VectoredExceptionHandler32);
            }
        }

        private unsafe ExceptionResult VectoredExceptionHandler32(ref EXCEPTION_POINTERS32 exceptionInfo)
        {
            return VectoredExceptionHandler(
                exceptionInfo.ExceptionRecord->ExceptionCode, 
                exceptionInfo.ExceptionRecord->ExceptionAddress, 
                new ThreadContextX86(ref *exceptionInfo.ContextRecord));
        }

        private unsafe ExceptionResult VectoredExceptionHandler64(ref EXCEPTION_POINTERS64 exceptionInfo)
        {
            return VectoredExceptionHandler(
                exceptionInfo.ExceptionRecord->ExceptionCode,
                exceptionInfo.ExceptionRecord->ExceptionAddress,
                new ThreadContextX64(ref *exceptionInfo.ContextRecord));
        }

        private ExceptionResult VectoredExceptionHandler(ExceptionCode exceptionCode, IntPtr exceptionAddress, IThreadContext context)
        {
            if (exceptionCode != ExceptionCode.AccessViolation &&
                exceptionCode != ExceptionCode.PrivilegedInstruction &&
                exceptionCode != ExceptionCode.Breakpoint &&
                exceptionCode != ExceptionCode.SingleStep &&
                exceptionCode != ExceptionCode.WX86SingleStep)
            {
                return ExceptionResult.ContinueSearch;
            }

            if (exceptionCode == ExceptionCode.AccessViolation)
            {
                //if (exceptionAddress >= _libStart && exceptionAddress <= _libEnd)
                {
                    return ExceptionResult.ContinueSearch;
                }
            }

            var threadId = GetCurrentThreadId();

            if (exceptionCode == ExceptionCode.WX86SingleStep)
            {
                //
                return 0;
            }

            var realBreakpointAddress = exceptionAddress;

            if (exceptionCode == ExceptionCode.AccessViolation)
            {
                //var addr = exceptionInfo.ExceptionRecord->ExceptionInformation[1];
                var addr = 0;
                if ((int)addr >= 0)
                {
                    return ExceptionResult.ContinueSearch;
                }

                var ptr = new IntPtr(-addr);

                //

                if (realBreakpointAddress == exceptionAddress)
                {
                    return ExceptionResult.ContinueSearch;
                }
            }
            else if (exceptionCode != ExceptionCode.PrivilegedInstruction)
            {
                // For data breakpoints, we need to get the address of the data, not the exception.
                var dr6 = context.Dr6.ToInt64();

                if ((dr6 & 1) != 0)
                {
                    realBreakpointAddress = context.Dr0;
                }
                else if ((dr6 & 2) != 0)
                {
                    realBreakpointAddress = context.Dr1;
                }
                else if ((dr6 & 4) != 0)
                {
                    realBreakpointAddress = context.Dr2;
                }
                else if ((dr6 & 8) != 0)
                {
                    realBreakpointAddress = context.Dr3;
                }
            }

            context.Dr6 = IntPtr.Zero;

            bool result = false;

            return result ? ExceptionResult.ContinueExecution : ExceptionResult.ContinueSearch;
        }

        public IBreakpoint SetBreakpoint(IntPtr address, Trigger trigger)
        {
            var context = GetThreadContext(IntPtr.Zero);
            


            
            throw new NotImplementedException();
        }

        private IThreadContext GetThreadContext(IntPtr hThread)
        {
            if (Environment.Is64BitProcess)
            {
                var x64Context = new CONTEXT64();
                Kernel32.GetThreadContext(hThread, ref x64Context);
                return new ThreadContextX64(ref x64Context);
            }
            else
            {
                var x86Context = new CONTEXT();
                Kernel32.GetThreadContext(hThread, ref x86Context);
                return new ThreadContextX86(ref x86Context);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public class Breakpoint : IBreakpoint
    {
        internal Trigger PreferredTrigger;

        public Trigger Trigger { get; set; }
        public IntPtr Address { get; set; }

        public event EventHandler<BreakpointEventArgs> Hit;
        public event EventHandler<BreakpointEventArgs> PostHit;

        internal bool HasHitListener => Hit != null;
        internal bool HasPostHitListener => PostHit != null;

        internal void RaiseHit(BreakpointEventArgs e)
        {
            Hit?.Invoke(this, e);
        }

        internal void RaisePostHit(BreakpointEventArgs e)
        {
            PostHit?.Invoke(this, e);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    internal interface IThreadContext
    {
        IntPtr Dr0 { get; set; }
        IntPtr Dr1 { get; set; }
        IntPtr Dr2 { get; set; }
        IntPtr Dr3 { get; set; }
        IntPtr Dr6 { get; set; }
        IntPtr Dr7 { get; set; }

        uint EFlags { get; set; }
    }
}
