using System;

namespace NoMod.Loader
{
    public interface IRunnable : IPlugin
    {
        /// <summary>
        /// Runs in a new thread. Once all plugins are done running,
        /// they are disposed and the AppDomain is unloaded.
        /// </summary>
        /// <param name="services">Global service container.</param>
        void Run(IServiceProvider services);
    }
}
