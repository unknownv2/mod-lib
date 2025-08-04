using NoMod.Unmanaged;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NoMod.ModUI;

namespace NoMod.Trainer.Legacy.Adapters
{
    [ComVisible(true)]
    internal interface IRemoteClientAdapter
    {
        double GetValue([MarshalAs(UnmanagedType.LPStr)] string name);
        void SetValue([MarshalAs(UnmanagedType.LPStr)] string name, double value);
        void AddValueChangedHandler(IntPtr handler);
        void RemoveValueChangedHandler(IntPtr handler);
    }

    public class RemoteClientAdapter : IRemoteClientAdapter
    {
        [UnmanagedFunctionPointer(CallingConvention.ThisCall, CharSet = CharSet.Ansi, SetLastError = true)]
        public delegate void ValueChangedHandlerT(IntPtr instance, string name, double value);

        private readonly IValueCollection _values;
        private readonly Dictionary<IntPtr, ValueChangedHandlerT> _handlers;

        public RemoteClientAdapter(IModUI modUi)
        {
            _values = modUi.Values;
            _handlers = new Dictionary<IntPtr, ValueChangedHandlerT>();

            _values.Changed += OnValueChanged;
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
            return _values[name];
        }

        public void SetValue(string name, double value)
        {
            _values[name] = value;
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
