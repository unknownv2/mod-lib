using System;
using System.Runtime.InteropServices;

namespace NoMod.Assembling
{
    public interface ISymbol<T> : ISymbol where T : struct
    {
        /// <summary>
        /// The value of the symbol at <see cref="Address"/>.
        /// </summary>
        T Value { get; set; }
    }

    public static class AssemblerExtensions
    {
        public static bool TryAssemble(this IAssembler assembler, string script, bool enable)
        {
            try
            {
                assembler.Assemble(script, enable);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Contains(this ISymbolCollection symbols, string name)
        {
            return symbols[name] != null;
        }

        public static ISymbol<T> Get<T>(this ISymbolCollection symbols, string name) where T : struct
        {
            var symbol = symbols[name];
            return symbol == null ? null : new Symbol<T>(symbol);
        }

        public static ISymbol<T> Create<T>(this ISymbolCollection symbols, string name, IntPtr near) where T : struct
        {
            return new Symbol<T>(symbols.Create(name, Marshal.SizeOf<T>(), near));
        }

        private sealed class Symbol<T> : ISymbol<T> where T : struct
        {
            private readonly ISymbol _symbol;

            public Symbol(ISymbol symbol)
            {
                _symbol = symbol;

                if (Address == IntPtr.Zero)
                {
                    throw new NullReferenceException();
                }

                if (Marshal.SizeOf<T>() > Size)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }

            public string Name => _symbol.Name;
            public int Size => _symbol.Size;

            public IntPtr Address
            {
                get => _symbol.Address;
                set => _symbol.Address = value;
            }

            public T Value
            {
                get => Marshal.PtrToStructure<T>(Address);
                set => Marshal.StructureToPtr(value, Address, false);
            }
        }
    }
}
