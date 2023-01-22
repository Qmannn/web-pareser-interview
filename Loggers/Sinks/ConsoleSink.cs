using System;

namespace WebParser.Loggers.Sinks;

public class ConsoleSink : ILoggerSink
{
    public void WriteMassage(ILogMessage message)
    {
        Console.WriteLine(message.Message);
        
        if (message.Exception != null)
        {
            Console.WriteLine($"Exception: {message.Exception}");
        }
    }
}