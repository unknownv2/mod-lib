#include "../../NoMod.Loader.Unmanaged/Loader.h";
#include "../../NoMod.SpeedHack.Unmanaged/SpeedHack.h";

using namespace NoMod::Loader;
using namespace NoMod::SpeedHack;

class Plugin : public IPlugin
{
public:
	void Boot(IServiceProvider* serviceProvider) override
	{
		// Nothing to do.
	}

	void Run(IServiceProvider* serviceProvider) override
	{
		auto speedHacker = (ISpeedHacker*)serviceProvider->GetService(NoMod::SpeedHack::ServiceKey);
		speedHacker->SetSpeedMultiplier(2.0);
	}

	~Plugin() override
	{
		// Nothing to do.
	}
};

GetPluginInterface()
{
	return new Plugin();
}