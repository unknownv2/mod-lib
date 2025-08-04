using Microsoft.Extensions.DependencyInjection;
using NoMod.Loader;

namespace NoMod.ModUI.Unmanaged
{
    public sealed class Plugin : IPlugin, IConfigurable
    {
        public void Configure(IServiceCollection services)
        {
            services.AddSingleton<IUnmanagedModUI>(p => new UnmanagedModUI(p.GetService<IModUI>()));
        }
    }
}
