using System.Runtime.InteropServices;

namespace NoMod.SpeedHack.Unmanaged
{
    [ComVisible(true)]
    internal interface IUnmanagedSpeedHacker
    {
        void SetSpeedMultiplier(double value);
    }

    internal sealed class UnmanagedSpeedHacker : IUnmanagedSpeedHacker
    {
        private readonly ISpeedHacker _speedHacker;

        public UnmanagedSpeedHacker(ISpeedHacker speedHacker)
        {
            _speedHacker = speedHacker;
        }

        public void SetSpeedMultiplier(double value)
        {
            _speedHacker.SetSpeedMultiplier(value);
        }
    }
}
