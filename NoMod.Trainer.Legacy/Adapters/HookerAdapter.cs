using NoMod.Hooking;
using System;
using System.Runtime.InteropServices;

namespace NoMod.Trainer.Legacy.Adapters
{
    [ComVisible(true)]
    public interface IHookAdapter
    {
        IntPtr TargetAddress();
        IntPtr DetourAddress();
        IntPtr OriginalAddress();
        bool Enable();
        bool Disable();
        void Remove();
    }

    [ComVisible(true)]
    internal interface IHookerAdapter
    {
        IntPtr CreateHook(IntPtr target, IntPtr detour);
        void BeginTransaction();
        bool CommitTransaction();
    }

    public class HookerAdapter : IHookerAdapter
    {
        private readonly IHooker _hooker;

        public HookerAdapter(IHooker hooker)
        {
            _hooker = hooker;
        }

        public void BeginTransaction()
        {
            // Not supported.
        }

        public bool CommitTransaction()
        {
            // Not supported.
            return true;
        }

        public IntPtr CreateHook(IntPtr target, IntPtr detour)
        {
            throw new NotImplementedException();
        }
    }

    public class HookAdapter : IHookAdapter
    {
        private readonly IHook _hook;

        public HookAdapter(IHook hook)
        {
            _hook = hook;
        }

        public IntPtr DetourAddress()
        {
            throw new NotImplementedException();
        }

        public bool Disable()
        {
            _hook.Enabled = false;
            return true;
        }

        public bool Enable()
        {
            _hook.Enabled = true;
            return true;
        }

        public IntPtr OriginalAddress()
        {
            throw new NotImplementedException();
        }

        public void Remove()
        {
            _hook.Dispose();
        }

        public IntPtr TargetAddress()
        {
            return _hook.Target;
        }
    }
}
