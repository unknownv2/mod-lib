using System;
using System.Runtime.InteropServices;
using NoMod.Unmanaged;

namespace NoMod.Win32
{
    public static partial class Kernel32
    {
        // GetModuleHandle
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        // GetProcAddress
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        // LoadLibrary
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
        public static extern IntPtr LoadLibrary(string lpFileName);

        // LoadLibraryEx
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, LoadLibraryFlags dwFlags);

        // FreeLibrary
        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);

        public static IntPtr GetProcAddress(string moduleName, string procName)
        {
            var hModule = GetModuleHandle(moduleName);
            return hModule == IntPtr.Zero ? hModule : GetProcAddress(hModule, procName);
        }

        public static T GetExportedFunction<T>(string moduleName, string exportName) where T : class
        {
            var hModule = GetModuleHandle(moduleName);
            return hModule == IntPtr.Zero ? null : GetExportedFunction<T>(hModule, exportName);
        }

        private static T GetExportedFunction<T>(IntPtr hModule, string exportName) where T : class
        {
            var procAddress = GetProcAddress(hModule, exportName);
            return procAddress != IntPtr.Zero
                ? procAddress.ToDelegate<T>()
                : null;
        }

        [Flags]
        public enum LoadLibraryFlags : uint
        {
            DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
            LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,
            LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
            LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
            LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
            LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200,
            LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000,
            LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100,
            LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800,
            LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400,
            LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008
        }
    }
}
