using System;

namespace NoMod.Engines.UnrealEngine
{
    public interface IUnrealEngine
    {
        IDisposable HookEvent(string eventName, IntPtr hookAddress);
    }
}
