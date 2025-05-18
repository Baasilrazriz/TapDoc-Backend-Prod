
using Serilog;
using Serilog.Formatting.Json;

namespace TapDoc_Mobile_App_Backend
{
    public class EOLogger
    {
        private readonly Serilog.Core.Logger serilogger;
        public EOLogger()
        {
            serilogger = new LoggerConfiguration()
           .WriteTo.Console(new JsonFormatter())
           .CreateLogger();
        }
        public void LogError(string s, object? args = null)
        {
            var data = new
            {
                message = s,
                additionalData = args
            };
            serilogger.Error("{@data}", data);

        }

        public void LogInformation(string s, object? args = null)
        {
            var data = new
            {
                message = s,
                additionalData = args
            };
            serilogger.Information("{@data}", data);
        }
        public void LogWarning(string s, object? args = null)
        {
            var data = new
            {
                message = s,
                additionalData = args
            };
            serilogger.Warning("{@data}", data);
        }

        public void LogDebug(string s, object? args = null)
        {
            var data = new
            {
                message = s,
                additionalData = args
            };
            serilogger.Debug("{@data}", data);
        }

        public void LogTrace(string s)
        {
            serilogger.Debug(s);
        }
        public string GetAmazonString()
        {
            return "trace:";
        }
    }
}
