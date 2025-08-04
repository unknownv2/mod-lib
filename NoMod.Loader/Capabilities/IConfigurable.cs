using Microsoft.Extensions.DependencyInjection;

namespace NoMod.Loader
{
    public interface IConfigurable : IPlugin
    {
        void Configure(IServiceCollection services);
    }
}
