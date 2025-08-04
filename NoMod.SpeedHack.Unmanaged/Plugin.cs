using Microsoft.Extensions.DependencyInjection;
using NoMod.Loader;

namespace NoMod.SpeedHack.Unmanaged
{
    public sealed class Plugin : IPlugin, IConfigurable
    {
        public void Configure(IServiceCollection services)
        {
            services.AddSingleton<IUnmanagedSpeedHacker>(p => new UnmanagedSpeedHacker(p.GetService<ISpeedHacker>()));
        }
    }
}
