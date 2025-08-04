using Microsoft.Extensions.DependencyInjection;
using System;

namespace NoMod.Loader.Test
{
    public class TestPlugin : IConfigurable, IBootable, IRunnable
    {
        public static TestPlugin Instance;

        public bool Configured;
        public bool Booted;
        public bool Ran;

        public TestPlugin Field;
        public TestPlugin Property { get; private set; }

        public TestPlugin()
        {
            Instance = this;
        }

        public void Configure(IServiceCollection services)
        {
            Configured = true;
        }

        public void Boot(IServiceProvider services)
        {
            Booted = true;
        }

        public void Run(IServiceProvider services)
        {
            Ran = true;
        }
    }
}
