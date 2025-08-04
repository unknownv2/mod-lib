using NoMod.Trainer.Legacy.Adapters;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NoMod.Trainer.Legacy
{
    [ComVisible(true)]
    public interface ITrainerLib
    {
        IntPtr GetInterface([MarshalAs(UnmanagedType.LPStr)] string name);
    }

    public class TrainerLib : ITrainerLib, IDisposable
    {
        private readonly Dictionary<string, AdapterConfig> _adapters = new Dictionary<string, AdapterConfig>
        {
            { "ILogger_001", CreateAdapter<ILoggerAdapter, LoggerAdapter>() },
            { "IProcess_001", CreateAdapter<IProcessAdapter, ProcessAdapter>() },
        };

        private readonly IServiceProvider _services;

        public TrainerLib(IServiceProvider services)
        {
            _services = services;
        }

        public IntPtr GetInterface(string name)
        {
            return _adapters.TryGetValue(name, out AdapterConfig adapter) 
                ? adapter.GetUnmanagedInterface(_services) 
                : IntPtr.Zero;
        }

        private static AdapterConfig CreateAdapter<TInterface, TImplementation>()
        {
            return new AdapterConfig(typeof(TInterface), typeof(TImplementation));
        }

        public void Dispose()
        {
            // TODO: Free unmanaged interfaces.
        }
    }
}
