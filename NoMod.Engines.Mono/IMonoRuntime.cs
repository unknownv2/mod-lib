using System;

namespace NoMod.Engines.Mono
{
    public interface IMonoRuntime : IDisposable
    {
        bool Loaded { get; }
        IMonoAssembly GetAssembly(string name);
    }

    public interface IMonoAssembly
    {
        IMonoClass GetClass(string name);
        void Compile();
    }

    public interface IMonoClass
    {
        IMonoMethod GetMethod(string name, int paramCount = -1);
        void Compile();
    }

    public interface IMonoMethod
    {
        IntPtr Compile();
    }
}
