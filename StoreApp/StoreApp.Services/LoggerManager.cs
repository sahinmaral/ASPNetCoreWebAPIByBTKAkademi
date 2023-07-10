using NLog;

using StoreApp.Entities.Enums;
using StoreApp.Services.Abstract;

namespace StoreApp.Services
{
    public class LoggerManager : ILoggerService
    {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();

        public void Log(string message, LogTypes logTypes)
        {
            switch (logTypes)
            {
                case LogTypes.Info:
                    _logger.Info(message);
                    break;
                case LogTypes.Warning:
                    _logger.Warn(message);
                    break;
                case LogTypes.Error:
                    _logger.Error(message);
                    break;
                case LogTypes.Debug:
                    _logger.Debug(message);
                    break;
                default:
                    break;
            }
        }
    }
}
