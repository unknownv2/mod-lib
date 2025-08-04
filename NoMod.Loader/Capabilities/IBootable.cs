using System;

namespace NoMod.Loader
{
    public interface IBootable : IPlugin
    {
        /// <summary>
        /// Called after all the plugins are registered, 
        /// in the order they were loaded.
        /// </summary>
        /// <param name="services">Global service container.</param>
        void Boot(IServiceProvider services);
    }
}
