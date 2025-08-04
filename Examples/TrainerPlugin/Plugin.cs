using System;
using NoMod.Assembling;
using NoMod.Loader;

public sealed class Plugin : IRunnable
{
    public IAssembler Assembler { get; }

    public void Run(IServiceProvider services)
    {
        Assembler.Assemble("[ENABLE] gdfgdfg", true);
    }
}
