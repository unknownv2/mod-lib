using EasyHook;
using System;

namespace NoMod.Hooking.EasyHook
{
    public sealed class EasyHooker : IHooker
    {
        public IHook CreateHook(IntPtr target, Delegate detour)
        {
            return new EasyHook(target, LocalHook.Create(target, detour, null));
        }

        public IHook CreateHook(IntPtr target, IntPtr detour)
        {
            return new EasyHook(target, LocalHook.CreateUnmanaged(target, detour, IntPtr.Zero));
        }

        public void Dispose()
        {
            // Nothing to do.
        }

        private class EasyHook : IHook
        {
            private static readonly int[] EmptyACL = new int[0];

            private readonly LocalHook _localHook;

            public EasyHook(IntPtr target, LocalHook localHook)
            {
                _localHook = localHook;
                Target = target;
            }

            public IntPtr Target { get; }
            public IntPtr OriginalAddress => _localHook.HookBypassAddress;

            public bool Enabled
            {
                get => _localHook.ThreadACL.IsExclusive;
                set
                {
                    if (value)
                    {
                        _localHook.ThreadACL.SetExclusiveACL(EmptyACL);
                    }
                    else
                    {
                        _localHook.ThreadACL.SetInclusiveACL(EmptyACL);
                    }
                }
            }

            public void Dispose()
            {
                _localHook.Dispose();
            }
        }
    }
}
