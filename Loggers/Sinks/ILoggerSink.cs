namespace WebParser.Loggers.Sinks;

public interface ILoggerSink
{
    public void WriteMassage(ILogMessage message);
}