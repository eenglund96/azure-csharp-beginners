using Serilog;
using Serilog.Core;

namespace GreetingConsoleApp;

public class FileGreetingWriter : IGreetingWriter
{
    private static readonly Logger _logger;                //store our logger in this class wide field. I prefer using _ as prefix for private fields, what do you prefer?
    static FileGreetingWriter()                             //use a static constructor to only init our logger once for the lifetime of the application
    {
        _logger = new LoggerConfiguration()         //init our logger in the constructor to ensure every instance of this class has a logger
                        .WriteTo.File("greetings.txt", rollingInterval: RollingInterval.Day)
                        .CreateLogger();
    }

    public void Write(string message)
    {
        Console.WriteLine(message);
        _logger.Write(Serilog.Events.LogEventLevel.Information, $"{message}\n");
    }
}