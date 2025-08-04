using EasyHook;
using NoMod.Loader.Managed;
using NoMod.Loader.Unmanaged;

namespace NoMod.Loader.EasyHook
{
    public sealed class Main : IEntryPoint
    {
        public Main(RemoteHooking.IContext context, LoaderConfig config)
        {
            // Nothing to do here.
        }

        public void Run(RemoteHooking.IContext context, LoaderConfig config)
        {
            new PluginManager(new IPluginLoader[] {
                new ManagedPluginLoader(config.Plugins),
                new UnmanagedPluginLoader(config.Plugins),
            }).Run();
        }
    }
}
