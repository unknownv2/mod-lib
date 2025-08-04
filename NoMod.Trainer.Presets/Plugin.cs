using Microsoft.Extensions.DependencyInjection;
using NoMod.Assembling;
using NoMod.Assembling.CETack;
using NoMod.Debugging;
using NoMod.Debugging.Vectored;
using NoMod.Hooking;
using NoMod.Hooking.EasyHook;
using NoMod.Loader;

namespace NoMod.Trainer.Presets
{
    public sealed class Plugin : IConfigurable
    {
        public void Configure(IServiceCollection services)
        {
            services
                .AddSingleton<IHooker, EasyHooker>()
                .AddSingleton<IDebugger, VectoredDebugger>()
                .AddSingleton<IAssembler, CETackAssembler>();
        }
    }
}
