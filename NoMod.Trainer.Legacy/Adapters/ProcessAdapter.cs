using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace NoMod.Trainer.Legacy.Adapters
{
    [ComVisible(true)]
    internal interface IProcessAdapter
    {
        IntPtr GetModuleBaseAddress([MarshalAs(UnmanagedType.LPWStr)] string module);
        uint GetModuleTimestamp([MarshalAs(UnmanagedType.LPWStr)] string module);
        IntPtr ScanProcess([MarshalAs(UnmanagedType.LPStr)] string terms, IntPtr startAddress, IntPtr endAddress);
        IntPtr ScanModule([MarshalAs(UnmanagedType.LPStr)] string terms, [MarshalAs(UnmanagedType.LPWStr)] string module, IntPtr startAddress);
        [return: MarshalAs(UnmanagedType.LPWStr)]
        string GetMainModuleBaseName();
        bool IsImmersive();
    }

    public class ProcessAdapter : IProcessAdapter
    {
        [return: MarshalAs(UnmanagedType.LPWStr)]
        public string GetMainModuleBaseName()
        {
            return Process.GetCurrentProcess().MainModule.ModuleName;
        }

        public IntPtr GetModuleBaseAddress([MarshalAs(UnmanagedType.LPWStr)] string module)
        {
            if (module == null)
            {
                return Process.GetCurrentProcess().MainModule.BaseAddress;
            }

            var mod = Process.GetCurrentProcess().Modules
                .Cast<ProcessModule>()
                .FirstOrDefault(m => m.ModuleName == module);
            return mod == null ? IntPtr.Zero : mod.BaseAddress;
        }

        public uint GetModuleTimestamp([MarshalAs(UnmanagedType.LPWStr)] string module)
        {
            throw new NotImplementedException();
        }

        public bool IsImmersive()
        {
            throw new NotImplementedException();
        }

        public IntPtr ScanModule([MarshalAs(UnmanagedType.LPStr)] string terms, [MarshalAs(UnmanagedType.LPWStr)] string module, IntPtr startAddress)
        {
            throw new NotImplementedException();
        }

        public IntPtr ScanProcess([MarshalAs(UnmanagedType.LPStr)] string terms, IntPtr startAddress, IntPtr endAddress)
        {
            throw new NotImplementedException();
        }
    }
}
