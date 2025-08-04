using Microsoft.Extensions.DependencyInjection;
using NoMod.Loader;

namespace NoMod.Hooking.Unmanaged
{
    public sealed class Plugin : IPlugin, IConfigurable
    {
        public void Configure(IServiceCollection services)
        {
            services.AddSingleton<IUnmanagedHooker>(p => new UnmanagedHooker(p.GetService<IHooker>()));
        }
    }
}
