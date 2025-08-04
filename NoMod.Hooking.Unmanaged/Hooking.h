namespace NoMod
{
	namespace Hooking
	{
		const char* ServiceKey = "NoMod.Hooking.Unmanaged.IUnmanagedHooker";

		class IHooker
		{
		public:
			virtual IHook* Create(void* target, void* detour) = 0;
		};

		class IHook
		{
		public:
			virtual void* GetTargetAddress() = 0;
			virtual void* GetOriginalAddress() = 0;
			virtual bool Enable() = 0;
			virtual bool Disable() = 0;
			virtual void Remove() = 0;
		};
	}
}
