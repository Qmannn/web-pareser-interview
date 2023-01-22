using System;

namespace WebParser.Loggers;

public interface ILogger
{
    public void Log(string message);

    public void LogError(string message, Exception exception);
}