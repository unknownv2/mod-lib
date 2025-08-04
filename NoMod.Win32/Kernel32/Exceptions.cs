using System;
using System.Runtime.InteropServices;

namespace NoMod.Win32
{
    public static partial class Kernel32
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ExceptionResult VectoredExceptionHandler32T(ref EXCEPTION_POINTERS32 exceptionInfo);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ExceptionResult VectoredExceptionHandler64T(ref EXCEPTION_POINTERS64 exceptionInfo);

        // AddVectoredExceptionHandler
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr AddVectoredExceptionHandler(uint firstHandler, VectoredExceptionHandler32T vectoredHandler);

        // AddVectoredExceptionHandler
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr AddVectoredExceptionHandler(uint firstHandler, VectoredExceptionHandler64T vectoredHandler);

        private const int EXCEPTION_MAXIMUM_PARAMETERS = 15;

        public enum ExceptionCode : uint
        {
            WX86SingleStep = 0x4000001E,
            Breakpoint = 0x80000003,
            SingleStep = 0x80000004,
            AccessViolation = 0xC0000005,
            PrivilegedInstruction = 0xC0000096,
        }

        public enum ExceptionResult : int
        {
            ContinueExecution = -1,
            ContinueSearch = 0,
            ExecuteHandler = 1,
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct EXCEPTION_POINTERS32
        {
            public EXCEPTION_RECORD32* ExceptionRecord;
            public CONTEXT* ContextRecord;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct EXCEPTION_POINTERS64
        {
            public EXCEPTION_RECORD64* ExceptionRecord;
            public CONTEXT64* ContextRecord;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct EXCEPTION_RECORD32
        {
            public ExceptionCode ExceptionCode;
            public uint ExceptionFlags;
            public EXCEPTION_RECORD32* ExceptionRecord;
            public IntPtr ExceptionAddress;
            public uint NumberParameters;
            public fixed uint ExceptionInformation[EXCEPTION_MAXIMUM_PARAMETERS];
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct EXCEPTION_RECORD64
        {
            public ExceptionCode ExceptionCode;
            public uint ExceptionFlags;
            public EXCEPTION_RECORD64* ExceptionRecord;
            public IntPtr ExceptionAddress;
            public uint NumberParameters;
            public fixed ulong ExceptionInformation[EXCEPTION_MAXIMUM_PARAMETERS];
        }
    }
}
