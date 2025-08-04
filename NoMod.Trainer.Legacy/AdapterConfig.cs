using NoMod.Unmanaged;
using System;

namespace NoMod.Trainer.Legacy
{
    internal sealed class AdapterConfig
    {
        private readonly object _lock = new object();

        public Type InterfaceType { get; }
        public Type ImplementationType { get; }

        private object _instance;
        private IntPtr _unmanaged;

        public AdapterConfig(Type interfaceType, Type implementationType)
        {
            InterfaceType = interfaceType;
            ImplementationType = implementationType;
        }

        public IntPtr GetUnmanagedInterface(IServiceProvider services)
        {
            lock (_lock)
            {
                if (_unmanaged != IntPtr.Zero)
                {
                    return _unmanaged;
                }

                _instance = services.GetService(ImplementationType);
                _unmanaged = _instance.GetUnmanagedInterface(InterfaceType);

                return _unmanaged;
            }
        }
    }
}
