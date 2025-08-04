using System;

namespace NoMod.SpeedHack
{
    public interface ISpeedHacker : IDisposable
    {
        void SetSpeedMultiplier(double value);
    }
}
