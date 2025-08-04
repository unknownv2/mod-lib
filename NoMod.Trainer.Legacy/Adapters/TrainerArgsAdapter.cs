using System.Runtime.InteropServices;

namespace NoMod.Trainer.Legacy.Adapters
{
    [ComVisible(true)]
    internal interface ITrainerArgsAdapter
    {
        [return: MarshalAs(UnmanagedType.LPStr)]
        string GetFlags();
        uint GetGameVersion();
    }

    public class TrainerArgsAdapter : ITrainerArgsAdapter
    {
        private string _gameVersion;

        public TrainerArgsAdapter(string gameVersion)
        {
            _gameVersion = gameVersion;
        }

        public string GetFlags()
        {
            return "";
        }

        public uint GetGameVersion()
        {
            return uint.TryParse(_gameVersion, out uint version) ? version : 0;
        }
    }
}
