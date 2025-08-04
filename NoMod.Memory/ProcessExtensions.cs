using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace NoMod.Memory
{
    public static class ProcessExtensions
    {
        public static IEnumerable<IntPtr> ScanMemory(this Process process, byte?[] bytes, IntPtr startAddress, IntPtr endAddress)
        {
            return new MemoryScan(process.Id, bytes, startAddress, endAddress);
        }

        public static IEnumerable<IntPtr> ScanMemory(this Process process, byte?[] bytes, IntPtr startAddress)
        {
            return ScanMemory(process, bytes, startAddress, MemoryScan.End);
        }

        public static IEnumerable<IntPtr> ScanMemory(this Process process, byte?[] bytes)
        {
            return ScanMemory(process, bytes, IntPtr.Zero);
        }

        private static IEnumerable<IntPtr> ScanModuleMemory(Process process, byte?[] bytes, ProcessModule module)
        {
            return ScanMemory(process, bytes, module.BaseAddress, module.BaseAddress + module.ModuleMemorySize);
        }

        public static IEnumerable<IntPtr> ScanModuleMemory(this Process process, byte?[] bytes, string moduleName = null)
        {
            ProcessModule module;
            if (moduleName == null)
            {
                module = process.MainModule;
            }
            else
            {
                moduleName = moduleName.ToLowerInvariant();
                module = process.Modules.Cast<ProcessModule>().FirstOrDefault(m => m.ModuleName.ToLowerInvariant() == moduleName)
                    ?? throw new DllNotFoundException("Module not found in process.");
            }

            return ScanModuleMemory(process, bytes, module);
        }
    }
}
