using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using NoMod.Unmanaged;

namespace NoMod.Loader.Unmanaged
{
    [ComVisible(true)]
    internal interface IUnmanagedServiceProvider
    {
        IntPtr GetService(string fullName);
    }

    internal class UnmanagedServiceProvider : IUnmanagedServiceProvider
    {
        private readonly IServiceProvider _provider;
        private readonly Dictionary<object, IntPtr> _cachedInstances;

        public UnmanagedServiceProvider(IServiceProvider provider)
        {
            _provider = provider;
            _cachedInstances = new Dictionary<object, IntPtr>(32);
        }

        public IntPtr GetService(string fullName)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var ptr = GetService(assembly, fullName);
                if (ptr != IntPtr.Zero)
                {
                    return ptr;
                }
            }

            return IntPtr.Zero;
        }

        private IntPtr GetService(Assembly assembly, string fullName)
        {
            var type = assembly.GetType(fullName);
            if (type == null)
            {
                return IntPtr.Zero;
            }

            var service = _provider.GetService(type);
            if (service == null)
            {
                return IntPtr.Zero;
            }

            lock (_cachedInstances)
            {
                if (!_cachedInstances.TryGetValue(service, out IntPtr ptr))
                {
                    ptr = service.GetUnmanagedInterface(type);
                    _cachedInstances.Add(service, ptr);
                }
                return ptr;
            }
        }
    }
}
