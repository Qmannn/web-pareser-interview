using System;
using System.Globalization;
using System.IO;

using Microsoft.Extensions.Options;

using WebParser.Options;

namespace WebParser.Loggers.Sinks;

public class FileSink : ILoggerSink
{
    private readonly ServiceOptions _configuration;

    public FileSink(IOptions<ServiceOptions> configuration) => _configuration = configuration.Value;

    public void WriteMassage(ILogMessage message)
    {
        var logFileName = _configuration.LogFileName ?? "log.txt";

        using var fileStream = new StreamWriter(Path.Combine(Environment.CurrentDirectory, logFileName), true);

        fileStream.WriteLine($"{DateTime.Now.ToString(CultureInfo.InvariantCulture)} {message.Message}");

        if (message.Exception != null)
        {
            fileStream.WriteLine($"Exception: {message.Exception}");
        }
    }
}