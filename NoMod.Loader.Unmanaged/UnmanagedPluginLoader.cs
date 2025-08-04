using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NoMod.Win32;

namespace NoMod.Loader.Unmanaged
{
    public sealed class UnmanagedPluginLoader : IPluginLoader
    {
        public static readonly string ExportName = "GetPluginInterface";

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate IntPtr GetPluginInterfaceT();

        private readonly IEnumerable<string> _files;

        public UnmanagedPluginLoader(IEnumerable<string> files)
        {
            _files = files;
        }

        public IEnumerable<IPlugin> LoadPlugins()
        {
            foreach (var file in _files)
            {
                var plugin = LoadPlugin(file);
                if (plugin != null)
                {
                    yield return plugin;
                }
            }
        }

        public IPlugin LoadPlugin(string file)
        {
            var hModule = Kernel32.LoadLibrary(file);
            if (hModule == IntPtr.Zero)
            {
                return null;
            }

            var getPlugin = Kernel32.GetExportedFunction<GetPluginInterfaceT>(file, ExportName);
            if (getPlugin == null)
            {
                Kernel32.FreeLibrary(hModule);
                return null;
            }

            var pluginPtr = getPlugin();

            // TODO: Generate COM wrapper OR get vtable pointers.

            return new UnmanagedPlugin(null);
        }
    }
}
