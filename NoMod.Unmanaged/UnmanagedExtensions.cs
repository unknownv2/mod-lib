using System;
using System.Runtime.InteropServices;

namespace NoMod.Unmanaged
{
    public static class UnmanagedExtensions
    {
        public static IntPtr GetUnmanagedInterface<TInterface>(this object obj)
        {
            return GetUnmanagedInterface(obj, typeof(TInterface));
        }

        public static IntPtr GetUnmanagedInterface(this object obj, Type interfaceType)
        {
            return Marshal.ReadIntPtr(Marshal.GetComInterfaceForObject(obj, interfaceType))
                + Marshal.GetStartComSlot(interfaceType) * IntPtr.Size;
        }

        public static TDelegate ToDelegate<TDelegate>(this IntPtr ptr) where TDelegate : class
        {
            return ToDelegate(ptr, typeof(TDelegate)) as TDelegate;
        }

        public static Delegate ToDelegate(this IntPtr ptr, Type delegateType)
        {
            return Marshal.GetDelegateForFunctionPointer(ptr, delegateType);
        }
    }
}
