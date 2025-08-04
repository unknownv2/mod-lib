using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace NoMod.Loader.Managed
{
    public sealed class ManagedPluginLoader : IPluginLoader
    {
        private static readonly Type PluginType = typeof(IPlugin);

        private readonly IEnumerable<string> _files;

        public ManagedPluginLoader(IEnumerable<string> files)
        {
            _files = files;
        }

        private static bool IsCompatibleAssembly(string file)
        {
            try
            {
                var name = AssemblyName.GetAssemblyName(file);
                switch (name.ProcessorArchitecture)
                {
                    case ProcessorArchitecture.Amd64:
                        return Environment.Is64BitProcess;
                    case ProcessorArchitecture.X86:
                        return !Environment.Is64BitProcess;
                    case ProcessorArchitecture.MSIL:
                        return true;
                    default:
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<IPlugin> LoadPlugins()
        {
            foreach (var file in _files.Where(IsCompatibleAssembly))
            {
                foreach (var plugin in Load(Assembly.LoadFrom(file)))
                {
                    yield return plugin;
                }
            }
        }

        private IEnumerable<IPlugin> Load(Assembly assembly)
        {
            foreach (var pluginType in EnumeratePluginTypes(assembly))
            {
                yield return (IPlugin)Activator.CreateInstance(pluginType);
            }
        }

        private IEnumerable<Type> EnumeratePluginTypes(Assembly assembly)
        {
            foreach (var type in assembly.ExportedTypes)
            {
                if (type.IsClass && type.GetInterfaces().Contains(PluginType))
                {
                    yield return type;
                }
            }
        }
    }
}
