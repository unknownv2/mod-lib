using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using NoMod.Loader;
using NoMod.Win32;

namespace NoMod.Trainer.Legacy
{
    public sealed class Plugin : IPlugin, IRunnable, IDisposable
    {
        private LegacyTrainer _trainer;

        public void Run(IServiceProvider services)
        {
            var hModule = GetTrainerModule();
            if (hModule == IntPtr.Zero)
            {
                return;
            }

            _trainer = new LegacyTrainer(new TrainerLib(services), hModule);
            _trainer.Setup();

            // TODO: Loop ModUI

            Thread.Sleep(Timeout.Infinite);
        }

        public void Dispose()
        {
            if (_trainer != null)
            {
                _trainer.Dispose();
                _trainer = null;
            }
        }

        private IntPtr GetTrainerModule()
        {
            foreach (var module in Process.GetCurrentProcess().Modules.OfType<ProcessModule>())
            {
                var hModule = Kernel32.LoadLibrary(module.FileName);
                if (LegacyTrainer.IsSupportedModule(hModule))
                {
                    return hModule;
                }
                Kernel32.FreeLibrary(hModule);
            }

            return IntPtr.Zero;
        }
    }
}
