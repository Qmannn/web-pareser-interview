using System;
using System.Collections.Generic;

using WebParser.Loggers.Sinks;

namespace WebParser.Loggers;

public class Logger : ILogger
{
    private readonly IEnumerable<ILoggerSink> _sinks;

    public Logger(IEnumerable<ILoggerSink> sinks) => _sinks = sinks;

    public void Log(string message)
    {
        foreach (var sink in _sinks)
        {
            sink.WriteMassage(new LogMessage(message));
        }
    }

    public void LogError(string message, Exception exception)
    {
        foreach (var sink in _sinks)
        {
            sink.WriteMassage(new LogMessage(message, exception));
        }
    }
}