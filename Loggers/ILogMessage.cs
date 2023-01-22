using System;

namespace WebParser.Loggers;

public interface ILogMessage
{
    string Message { get; }

    Exception? Exception { get; }
}