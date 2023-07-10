using StoreApp.Entities.Enums;

namespace StoreApp.Services.Abstract
{
    public interface ILoggerService
    {
        void Log(string message, LogTypes logTypes);
    }
}
