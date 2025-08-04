using NoMod.Assembling;
using System;
using System.Runtime.InteropServices;

namespace NoMod.Trainer.Legacy.Adapters
{
    [ComVisible(true)]
    internal interface IAssemblerAdapter
    {
        bool Assemble([MarshalAs(UnmanagedType.LPStr)] string script, bool forEnable);
        IntPtr GetSymbolAddress([MarshalAs(UnmanagedType.LPStr)] string name);
        void SetSymbolAddress([MarshalAs(UnmanagedType.LPStr)] string name, IntPtr address);
        void EnableDataScans();
        void DisableDataScans();
    }

    public class AssemblerAdapter : IAssemblerAdapter
    {
        private readonly IAssembler _assembler;

        public AssemblerAdapter(IAssembler assembler)
        {
            _assembler = assembler;
        }

        public bool Assemble(string script, bool forEnable)
        {
            return _assembler.TryAssemble(script, forEnable);
        }

        public void DisableDataScans()
        {
            _assembler.DataScans = false;
        }

        public void EnableDataScans()
        {
            _assembler.DataScans = true;
        }

        public IntPtr GetSymbolAddress(string name)
        {
            var symbol = _assembler.Symbols[name];
            return symbol != null ? symbol.Address : IntPtr.Zero;
        }

        public void SetSymbolAddress(string name, IntPtr address)
        {
            var symbol = _assembler.Symbols[name];
            if (symbol != null)
            {
                symbol.Address = address;
            }
        }
    }
}
