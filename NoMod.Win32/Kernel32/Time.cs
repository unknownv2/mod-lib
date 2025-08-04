using System.Runtime.InteropServices;

namespace NoMod.Win32
{
    public static partial class Kernel32
    {
        // GetTickCount
        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
        public delegate uint GetTickCountT();
        [DllImport("kernel32.dll")]
        public static extern uint GetTickCount();

        // GetTickCount64
        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
        public delegate ulong GetTickCount64T();
        [DllImport("kernel32.dll")]
        public static extern ulong GetTickCount64();

        // QueryPerformanceCounter
        [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
        public delegate bool QueryPerformanceCounterT(out long lpPerformanceCount);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool QueryPerformanceCounter(out long lpPerformanceCount);
    }
}
