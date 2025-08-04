using System;
using System.Collections.Generic;

namespace NoMod.ModUI
{
    internal sealed class ValueCollection : IValueCollection
    {
        private readonly Dictionary<string, Value> _values = new Dictionary<string, Value>();

        public event EventHandler<ValueChangedEventArgs> Changed;

        public Value this[string name]
        {
            get
            {
                lock (_values)
                {
                    return _values.TryGetValue(name, out Value value) 
                        ? value
                        : AddOrUpdate(name, 0, false);
                }
            }
            set => AddOrUpdate(name, value);
        }

        private Value AddOrUpdate(string name, Value value, bool raiseEvents = true)
        {
            ValueChangedEventArgs e = null;

            lock (_values)
            {
                if (!_values.TryGetValue(name, out Value existing))
                {
                    e = new ValueChangedEventArgs(name, value);
                    _values.Add(name, value);
                    
                }
                else if (existing != value)
                {
                    e = new ValueChangedEventArgs(name, value);
                    existing.Update(e, value);
                }
            }
           
            if (e != null && raiseEvents)
            {
                Changed?.Invoke(this, e);
            }

            return value;
        }

        public void Reset()
        {
            lock (_values)
            {
                foreach (var value in _values)
                {
                    if (value.Value != 0)
                    {
                        var e = new ValueChangedEventArgs(value.Key, value.Value);
                        value.Value.Update(e, 0);
                        Changed?.Invoke(this, e);
                    }
                }
            }
        }
    }
}
