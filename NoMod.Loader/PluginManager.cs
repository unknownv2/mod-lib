using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace NoMod.Loader
{
    public sealed class PluginManager
    {
        private readonly object Mutex = new object();

        private readonly IEnumerable<IPluginLoader> _loaders;
        private readonly List<IPlugin> _plugins;
        private IServiceProvider _serviceProvider;

        public PluginManager(IPluginLoader loader)
            : this(new [] { loader })
        {

        }

        public PluginManager(IEnumerable<IPluginLoader> loaders)
        {
            _loaders = loaders;
            _plugins = new List<IPlugin>(16);
        }

        public void Run()
        {
            LoadPlugins();
            InjectDependencies();
            BootPlugins();
            RunPlugins();
        }

        private void LoadPlugins()
        {
            var services = new ServiceCollection();

            foreach (var loader in _loaders)
            {
                foreach (var plugin in loader.LoadPlugins())
                {
                    (plugin as IConfigurable)?.Configure(services);
                    _plugins.Add(plugin);
                    services.AddSingleton(plugin.GetType(), plugin);
                }
            }

            _serviceProvider = services.BuildServiceProvider();
        }

        private void BootPlugins()
        {
            foreach (var plugin in _plugins.OfType<IBootable>())
            {
                plugin.Boot(_serviceProvider);
            }
        }

        private void InjectDependencies()
        {
            foreach (var plugin in _plugins)
            {
                _serviceProvider.FillObjectFields(plugin);
                _serviceProvider.FillObjectProperties(plugin);
            }
        }

        private void RunPlugins()
        {
            foreach (var thread in CreatePluginThreads())
            {
                thread.Join();
            }
        }

        private List<Thread> CreatePluginThreads()
        {
            var threads = new List<Thread>(5);

            foreach (var plugin in _plugins.OfType<IRunnable>())
            {
                var thread = new Thread(RunPluginThread);
                thread.IsBackground = true;
                thread.Start(plugin);
                threads.Add(thread);
            }

            return threads;
        }

        private void RunPluginThread(object plugin)
        {
            ((IRunnable)plugin).Run(_serviceProvider);
        }
    }
}
