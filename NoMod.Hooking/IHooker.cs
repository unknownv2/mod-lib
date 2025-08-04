using System;

namespace NoMod.Hooking
{
    public interface IHooker : IDisposable
    {
        /// <summary>
        /// Creates a hook at the target address.
        /// </summary>
        /// <param name="target">The address of a function.</param>
        /// <param name="detour">The function that will be called instead.</param>
        /// <returns>The resulting hook.</returns>
        IHook CreateHook(IntPtr target, Delegate detour);

        /// <summary>
        /// Creates an unmanaged hook at the target address.
        /// </summary>
        /// <param name="target">The address of a function.</param>
        /// <param name="detour">A pointer to the function that will be called instead.</param>
        /// <returns>The resulting hook.</returns>
        IHook CreateHook(IntPtr target, IntPtr detour);
    }

    public interface IHook : IDisposable
    {
        /// <summary>
        /// True if the hook is enabled.
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// The address that is hooked.
        /// </summary>
        IntPtr Target { get; }

        /// <summary>
        /// The address of a function that calls the original function.
        /// </summary>
        IntPtr OriginalAddress { get; }
    }
}
