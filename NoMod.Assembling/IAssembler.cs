using System;

namespace NoMod.Assembling
{
    public interface IAssembler
    {
        /// <summary>
        /// The ID of the process the assembler targets.
        /// </summary>
        int TargetProcessId { get; }

        /// <summary>
        /// True if AOB scans should scan non-executable pages.
        /// </summary>
        bool DataScans { get; set; }

        /// <summary>
        /// Globally-allocated symbols.
        /// </summary>
        ISymbolCollection Symbols { get; }

        /// <summary>
        /// Assemble and execute a script.
        /// </summary>
        /// <param name="script">The script.</param>
        /// <param name="enable">True to execute the [ENABLE] section. False to execute [DISABLE].</param>
        void Assemble(string script, bool enable);
    }

    public interface ISymbolCollection
    {
        /// <summary>
        /// Gets a symbol by name.
        /// </summary>
        /// <param name="name">The name of the symbol.</param>
        /// <returns>The symbol, or null if it does not exist.</returns>
        ISymbol this[string name] { get; }

        /// <summary>
        /// Allocates a new symbol.
        /// </summary>
        /// <param name="name">The name of the new symbol.</param>
        /// <param name="size">The number of bytes to allocate.</param>
        /// <param name="near">The address to allocate the symbol near.</param>
        /// <returns></returns>
        ISymbol Create(string name, int size, IntPtr near);

        /// <summary>
        /// Deallocates a symbol.
        /// </summary>
        /// <param name="name">The name of the symbol.</param>
        void Delete(string name);
    }

    public interface ISymbol
    {
        /// <summary>
        /// The unique name of the symbol.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The address of the symbol.
        /// </summary>
        IntPtr Address { get; set; }

        /// <summary>
        /// The size of the data at <see cref="Address"/>.
        /// </summary>
        int Size { get; }
    }
}
