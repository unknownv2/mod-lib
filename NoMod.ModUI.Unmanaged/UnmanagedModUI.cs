using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NoMod.Unmanaged;

namespace NoMod.ModUI.Unmanaged
{
    [ComVisible(true)]
    public interface IUnmanagedModUI
    {
        double GetValue([MarshalAs(UnmanagedType.LPStr)] string name);
        void SetValue([MarshalAs(UnmanagedType.LPStr)] string name, double value);
        void AddValueChangedHandler(IntPtr handler);
        void RemoveValueChangedHandler(IntPtr handler);
    }

    internal sealed class UnmanagedModUI : IUnmanagedModUI
    {
        [UnmanagedFunctionPointer(CallingConvention.ThisCall, CharSet = CharSet.Ansi, SetLastError = true)]
        public delegate void ValueChangedHandlerT(IntPtr instance, string name, double value);

        private readonly IModUI _ui;
        private readonly Dictionary<IntPtr, ValueChangedHandlerT> _handlers;

        public UnmanagedModUI(IModUI ui)
        {
            _ui = ui;
            _ui.Values.Changed += OnValueChanged;
            _handlers = new Dictionary<IntPtr, ValueChangedHandlerT>(4);
        }

        private void OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            foreach (var handler in _handlers)
            {
                handler.Value(handler.Key, e.Name, e.Value);
            }
        }

        public double GetValue(string name)
        {
            return _ui.Values[name];
        }

        public void SetValue(string name, double value)
        {
            _ui.Values[name] = value;
        }

        public void AddValueChangedHandler(IntPtr handler)
        {
            _handlers[handler] = Marshal.ReadIntPtr(handler).ToDelegate<ValueChangedHandlerT>();
        }

        public void RemoveValueChangedHandler(IntPtr handler)
        {
            _handlers.Remove(handler);
        }
    }
}
