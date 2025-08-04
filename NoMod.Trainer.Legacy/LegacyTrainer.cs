using NoMod.Unmanaged;
using NoMod.Win32;
using System;
using System.Runtime.InteropServices;

namespace NoMod.Trainer.Legacy
{
    internal sealed class LegacyTrainer : IDisposable
    {
        private readonly TrainerLib _trainerLib;
        private readonly IntPtr _hModule;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate bool InitializeT(IntPtr trainerLib);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate bool CleanupT();

        public LegacyTrainer(TrainerLib trainerLib, IntPtr hTrainerModule)
        {
            _trainerLib = trainerLib;
            _hModule = hTrainerModule;
        }

        public void Setup()
        {
            GetSetupFunction()(_trainerLib.GetUnmanagedInterface<ITrainerLib>());
        }

        internal static bool IsSupportedModule(IntPtr hModule)
        {
            return Kernel32.GetProcAddress(hModule, "Initialize") != IntPtr.Zero;
        }

        private InitializeT GetSetupFunction()
        {
            var setupPtr = Kernel32.GetProcAddress(_hModule, "Initialize");
            if (setupPtr == IntPtr.Zero)
            {
                throw new Exception("Trainer setup export not found.");
            }
            return setupPtr.ToDelegate<InitializeT>();
        }

        public void Dispose()
        {
            GetCleanupFunction()?.Invoke();
            Kernel32.FreeLibrary(_hModule);
            _trainerLib.Dispose();
            // TODO: Free unmanaged interfaces?
        }

        private CleanupT GetCleanupFunction()
        {
            var cleanPtr = Kernel32.GetProcAddress(_hModule, "Clean");
            return cleanPtr == IntPtr.Zero ? null : cleanPtr.ToDelegate<CleanupT>();
        }
    }
}
