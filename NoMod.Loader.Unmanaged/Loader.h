#pragma once

namespace NoMod
{
	namespace Loader
	{
		class IServiceProvider
		{
		public:
			virtual void* GetService(const char* fullName) = 0;
		};

		class IPlugin
		{
		public:
			virtual void Boot(IServiceProvider* serviceProvider) = 0;
			virtual void Run(IServiceProvider* serviceProvider) = 0;
			virtual ~IPlugin() = 0 {};
		};
	}
}

#define GetPluginInterface() extern "C" __declspec(dllexport) NoMod::Loader::IPlugin* GetPluginInterface()