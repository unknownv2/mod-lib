namespace NoMod
{
	namespace ModUI
	{
		const char* ServiceKey = "NoMod.ModUI.Unmanaged.IUnmanagedSpeedHacker";

		class IModUI
		{
		public:
			virtual double GetValue(const char* name) = 0;
			virtual void SetValue(const char* name, double value) = 0;
			virtual void AddValueChangedHandler(IValueChangedHandler* handler) = 0;
			virtual void RemoveValueChangedHandler(IValueChangedHandler* handler) = 0;
		};

		class IValueChangedHandler
		{
		public:
			virtual void HandleValueChanged(const char* name, double value) = 0;
		};
	}
}