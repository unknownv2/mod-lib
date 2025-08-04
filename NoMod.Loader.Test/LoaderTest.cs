using NUnit.Framework;
using System.Collections.Generic;

namespace NoMod.Loader.Test
{
    internal class FakeLoader : IPluginLoader
    {
        public IEnumerable<IPlugin> LoadPlugins()
        {
            yield return new TestPlugin();
        }
    }

    public class LoaderTest
    {
        [Test]
        public void ShouldLoadPlugins()
        {
            var manager = new PluginManager(new FakeLoader());
            manager.Run();

            Assert.IsTrue(TestPlugin.Instance.Configured, "Plugin not configured.");
            Assert.IsTrue(TestPlugin.Instance.Booted, "Plugin not booted.");
            Assert.IsTrue(TestPlugin.Instance.Ran, "Plugin not ran.");

            Assert.AreEqual(TestPlugin.Instance, TestPlugin.Instance.Field, "Plugin field not injected.");
            Assert.AreEqual(TestPlugin.Instance, TestPlugin.Instance.Property, "Plugin property not injected.");
        }
    }
}
