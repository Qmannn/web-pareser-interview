using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using WebParser.Loaders;
using WebParser.Loggers;
using WebParser.Loggers.Sinks;
using WebParser.Options;
using WebParser.Parser;
using WebParser.Services;

namespace WebParser;

class Program
{
    public static async Task Main(string[] args)
    {
        await Host.CreateDefaultBuilder(args)
                  .ConfigureServices((hostContext, services) =>
                   {
                       services.Configure<ServiceOptions>(hostContext.Configuration);
                       services.AddSingleton<ILoggerSink, FileSink>();
                       services.AddSingleton<ILoggerSink, ConsoleSink>();
                       services.AddSingleton<ILogger, Logger>();
                       services.AddSingleton<IResurceLoader, WebLoader>();
                       services.AddScoped<IContentParser, ContentParser>();
                       services.AddHostedService<ParsingService>();
                   })
                  .UseConsoleLifetime()
                  .Build()
                  .RunAsync();
    }
}