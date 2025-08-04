using System;
using System.Runtime.InteropServices;
using NoMod.Unmanaged;

namespace NoMod.Hooking.Unmanaged
{
    [ComVisible(true)]
    internal interface IUnmanagedHook
    {
        IntPtr GetTargetAddress();
        IntPtr GetOriginalAddress();
        bool Enable();
        bool Disable();
        void Remove();
    }

    [ComVisible(true)]
    internal interface IUnmanagedHooker
    {
        IntPtr CreateHook(IntPtr target, IntPtr detour);
    }

    internal sealed class UnmanagedHooker : IUnmanagedHooker
    {
        private readonly IHooker _hooker;

        public UnmanagedHooker(IHooker hooker)
        {
            _hooker = hooker;
        }

        public IntPtr CreateHook(IntPtr target, IntPtr detour)
        {
            return new UnmanagedHook(_hooker.CreateHook(target, detour))
                .GetUnmanagedInterface<IUnmanagedHook>();
        }
    }

    internal sealed class UnmanagedHook : IUnmanagedHook
    {
        private readonly IHook _hook;

        public UnmanagedHook(IHook hook)
        {
            _hook = hook;
        }

        public IntPtr GetOriginalAddress() => _hook.OriginalAddress;
        public IntPtr GetTargetAddress() => _hook.Target;
        public void Remove() => _hook.Dispose();

        public bool Enable()
        {
            _hook.Enabled = true;
            return true;
        }

        public bool Disable()
        {
            _hook.Enabled = false;
            return true;
        }
    }
}
