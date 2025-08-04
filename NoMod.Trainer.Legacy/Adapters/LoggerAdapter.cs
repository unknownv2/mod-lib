using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;

namespace NoMod.Trainer.Legacy.Adapters
{
    [ComVisible(true)]
    internal interface ILoggerAdapter
    {
        bool IsConnected();
        void Log(byte logLevel, [MarshalAs(UnmanagedType.LPWStr)] string message);
    }

    public class LoggerAdapter : ILoggerAdapter
    {
        private readonly ILogger _logger;

        public LoggerAdapter(ILogger logger)
        {
            _logger = logger;
        }

        public bool IsConnected()
        {
            return true;
        }

        public void Log(byte logLevel, string message)
        {
            switch (logLevel)
            {
                case 0:
                    _logger.LogDebug(message);
                    break;
                case 1:
                    _logger.LogInformation(message);
                    break;
                case 2:
                    _logger.LogWarning(message);
                    break;
                case 3:
                    _logger.LogError(message);
                    break;
                default:
                    _logger.LogInformation(message);
                    break;
            }
        }
    }
}
