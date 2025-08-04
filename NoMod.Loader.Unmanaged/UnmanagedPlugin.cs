using System;
using System.Runtime.InteropServices;
using NoMod.Unmanaged;

namespace NoMod.Loader.Unmanaged
{
    [ComVisible(true)]
    internal interface IUnmanagedPlugin
    {
        void Boot(IntPtr serviceProvider);
        void Run(IntPtr serviceProvider);
        void Dispose();
    }

    internal sealed class UnmanagedPlugin : IPlugin, IBootable, IRunnable, IDisposable
    {
        private readonly IUnmanagedPlugin _plugin;

        public UnmanagedPlugin(IUnmanagedPlugin plugin)
        {
            _plugin = plugin;
        }

        public void Boot(IServiceProvider services)
        {
            _plugin.Boot(new UnmanagedServiceProvider(services)
                .GetUnmanagedInterface<IUnmanagedServiceProvider>());
        }

        public void Run(IServiceProvider services)
        {
            _plugin.Run(new UnmanagedServiceProvider(services)
                .GetUnmanagedInterface<IUnmanagedServiceProvider>());
        }

        public void Dispose()
        {
            _plugin.Dispose();
        }
    }
}
