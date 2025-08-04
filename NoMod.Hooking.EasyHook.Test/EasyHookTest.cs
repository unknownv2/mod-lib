using NUnit.Framework;
using NoMod.Win32;

namespace NoMod.Hooking.EasyHook.Test
{
    public class EasyHookTest
    {
        private const uint ConstantTickCount = 1234567890;

        [Test]
        public void ShouldBeDisabledByDefault()
        {
            using (var hook = HookGetTickCount())
            {
                Assert.IsFalse(hook.Enabled);
            }
        }

        [Test]
        public void ShouldHook()
        {
            using (var hook = HookGetTickCount())
            {
                hook.Enabled = true;
                Assert.AreEqual(ConstantTickCount, Kernel32.GetTickCount());
            }
        }

        [Test]
        public void ShouldDisableHook()
        {
            using (var hook = HookGetTickCount())
            {
                hook.Enabled = true;
                hook.Enabled = false;
                Assert.AreNotEqual(Kernel32.GetTickCount(), ConstantTickCount);
            }
        }

        [Test]
        public void ShouldDisposeHook()
        {
            using (var hook = HookGetTickCount())
            {
                hook.Enabled = true;
            }

            Assert.AreNotEqual(Kernel32.GetTickCount(), ConstantTickCount);
        }

        [Test]
        public void ShouldCallOriginalFunction()
        {
            using (var hook = HookGetTickCount())
            {
                hook.Enabled = true;
                Assert.AreNotEqual(hook.Original(), ConstantTickCount);
            }
        }

        IHook<Kernel32.GetTickCountT> HookGetTickCount()
        {
            return new EasyHooker().CreateHook<Kernel32.GetTickCountT>("kernel32.dll", "GetTickCount", GetTickCountDetour);
        }

        public uint GetTickCountDetour()
        {
            return ConstantTickCount;
        }
    }
}
