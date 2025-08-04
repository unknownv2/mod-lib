using System;

namespace NoMod.ModUI
{
    public interface IValueCollection
    {
        event EventHandler<ValueChangedEventArgs> Changed;

        Value this[string name] { get; set; }

        void Reset();
    }

    public sealed class ValueChangedEventArgs : EventArgs
    {
        public string Name { get; }
        public Value Value { get; }

        public ValueChangedEventArgs(string name, Value value)
        {
            Name = name;
            Value = value;
        }
    }

    public struct Value
    {
        public event EventHandler<ValueChangedEventArgs> Changed;

        private double _value;

        internal Value(double value)
        {
            Changed = null;
            _value = value;
        }

        internal void Update(ValueChangedEventArgs e, double value)
        {
            _value = value;
            Changed?.Invoke(this, e);
        }

        public override bool Equals(object obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();

        public static bool operator ==(Value a, Value b) => a._value == b._value;
        public static bool operator !=(Value a, Value b) => a._value != b._value;
        public static bool operator >(Value a, Value b) => a._value > b._value;
        public static bool operator <(Value a, Value b) => a._value < b._value;
        public static bool operator >=(Value a, Value b) => a._value >= b._value;
        public static bool operator <=(Value a, Value b) => a._value <= b._value;

        public static implicit operator Value(bool value) => new Value(Convert.ToDouble(value));
        public static implicit operator Value(sbyte value) => new Value(Convert.ToDouble(value));
        public static implicit operator Value(byte value) => new Value(Convert.ToDouble(value));
        public static implicit operator Value(short value) => new Value(Convert.ToDouble(value));
        public static implicit operator Value(ushort value) => new Value(Convert.ToDouble(value));
        public static implicit operator Value(int value) => new Value(Convert.ToDouble(value));
        public static implicit operator Value(uint value) => new Value(Convert.ToDouble(value));
        public static implicit operator Value(long value) => new Value(Convert.ToDouble(value));
        public static implicit operator Value(ulong value) => new Value(Convert.ToDouble(value));
        public static implicit operator Value(float value) => new Value(Convert.ToDouble(value));
        public static implicit operator Value(double value) => new Value(Convert.ToDouble(value));
        public static implicit operator Value(decimal value) => new Value(Convert.ToDouble(value));

        public static implicit operator bool(Value value) => Convert.ToBoolean(value._value);
        public static implicit operator sbyte(Value value) => Convert.ToSByte(value._value);
        public static implicit operator byte(Value value) => Convert.ToByte(value._value);
        public static implicit operator short(Value value) => Convert.ToInt16(value._value);
        public static implicit operator ushort(Value value) => Convert.ToUInt16(value._value);
        public static implicit operator int(Value value) => Convert.ToInt32(value._value);
        public static implicit operator uint(Value value) => Convert.ToUInt32(value._value);
        public static implicit operator long(Value value) => Convert.ToInt64(value._value);
        public static implicit operator ulong(Value value) => Convert.ToUInt64(value._value);
        public static implicit operator float(Value value) => Convert.ToSingle(value._value);
        public static implicit operator double(Value value) => Convert.ToDouble(value._value);
        public static implicit operator decimal(Value value) => Convert.ToDecimal(value._value);
    }
}
