using System;
using NoMod.Unmanaged;
using NoMod.Win32;

namespace NoMod.Hooking
{
    public interface IHook<T> : IHook where T : class
    {
        T Original { get; }
    }

    public static class HookerExtensions
    {
        public static IHook<T> CreateHook<T>(this IHooker hooker, IntPtr target, T detour) where T : class
        {
            return new TypedHook<T>(hooker.CreateHook(target, detour as Delegate));
        }

        public static IHook<T> CreateHook<T>(this IHooker hooker, string module, string export, T detour) where T : class
        {
            return CreateHook(hooker, Kernel32.GetProcAddress(Kernel32.GetModuleHandle(module), export), detour);
        }

        private sealed class TypedHook<T> : IHook<T> where T : class
        {
            private readonly IHook _hook;
            private T _original;

            public TypedHook(IHook hook)
            {
                _hook = hook;
            }

            public T Original => _original ?? (_original = OriginalAddress.ToDelegate<T>());

            public IntPtr Target => _hook.Target;
            public IntPtr OriginalAddress => _hook.OriginalAddress;

            public bool Enabled
            {
                get => _hook.Enabled;
                set => _hook.Enabled = value;
            }

            public void Dispose()
            {
                _hook.Dispose();
            }
        }
    }
}
