#pragma once

namespace NoMod
{
	namespace SpeedHack
	{
		const char* ServiceKey = "NoMod.SpeedHack.Unmanaged.IUnmanagedSpeedHacker";

		class ISpeedHacker
		{
		public:
			virtual void SetSpeedMultiplier(double value) = 0;
		};
	}
}