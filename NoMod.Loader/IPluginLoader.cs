using System.Collections.Generic;

namespace NoMod.Loader
{
    public interface IPluginLoader
    {
        /// <summary>
        /// Load plugins into the process.
        /// </summary>
        IEnumerable<IPlugin> LoadPlugins();
    }
}
