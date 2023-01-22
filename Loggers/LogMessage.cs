using System;

namespace WebParser.Loggers;

public class LogMessage : ILogMessage
{
    public string Message { get; }

    public Exception? Exception { get; }

    public LogMessage(string message) => Message = message;

    public LogMessage(string message, Exception exception) : this(message) => Exception = exception;
}