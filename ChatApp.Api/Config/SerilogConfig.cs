

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using ChatApp.Core.Models;

namespace ChatApp.Api.Config
{
    public static class SerilogConfig
    {
        public static ILogger GetLogger(IConfiguration configuration = null)
        {
            var _settings = configuration.GetSection("SerilogSettings").Get<SerilogSettings>();
            var connectionStrings = configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

            string conStr = _settings?.ConnectionStrings ?? connectionStrings.DefaultConnection;

            LoggerConfiguration _loggerConfig = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Logger(x => x.Filter.ByIncludingOnly(e => e.Level == Serilog.Events.LogEventLevel.Information).WriteTo.RollingFile(_settings.FullFilePath + "Info-{Date}.log"))
                .WriteTo.Logger(x => x.Filter.ByIncludingOnly(e => e.Level == Serilog.Events.LogEventLevel.Warning).WriteTo.RollingFile(_settings.FullFilePath + "Warn-{Date}.log"))
                .WriteTo.Logger(x => x.Filter.ByIncludingOnly(e => e.Level == Serilog.Events.LogEventLevel.Error).WriteTo.RollingFile(_settings.FullFilePath + "Error-{Date}.log"))
                .WriteTo.Logger(x => x.Filter.ByIncludingOnly(e => e.Level == Serilog.Events.LogEventLevel.Fatal).WriteTo.RollingFile(_settings.FullFilePath + "Fatal-{Date}.log"))
                .WriteTo.Logger(x => x.Filter.ByIncludingOnly(e => e.Level == Serilog.Events.LogEventLevel.Debug).WriteTo.RollingFile(_settings.FullFilePath + "Debug-{Date}.log"))
                .WriteTo.RollingFile(_settings.FullFilePath + "Verbose-{Date}.log")
                .WriteTo.MSSqlServer(
                        connectionString: conStr,
                        tableName: _settings.Table,
                        autoCreateSqlTable: true);

            _loggerConfig.WriteTo.ColoredConsole();

            var log = _loggerConfig.CreateLogger();

            Log.Logger = log;

            return log;
        }
        public static IServiceCollection AddSerilog(this IServiceCollection services, IConfiguration configuration)
        {
            var logger = GetLogger(configuration);
            return services.AddLogging(confiruge => confiruge.AddSerilog(logger, dispose: true));
        }
    }
}
