using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;

namespace GreetingConsoleApp;

public class FileGreetingWriter : IGreetingWriter
{
    private static readonly Logger _logger;                 //store our logger in this class wide field. I prefer using _ as prefix for private fields, what do you prefer?
    static FileGreetingWriter()                             //use a static constructor to only init our logger once for the lifetime of the application
    {
        //duplicate IConfiguration code here, not optimal but we'll run with it for now
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var settings = config.GetRequiredSection("Settings").Get<Settings>();

        _logger = new LoggerConfiguration()                 //init our logger in the constructor to ensure every instance of this class has a logger
                        .WriteTo.File(settings.GreetingWriterOutputFilePath, rollingInterval: RollingInterval.Day)          //set output file to GreetingWriterOutputFilePath
                        .CreateLogger();
    }

    public void Write(string message)
    {
        Console.WriteLine(message);
        _logger.Write(Serilog.Events.LogEventLevel.Information, $"{message}\n");
    }

    public void Write(Greeting greeting)
    {
        Write(greeting.GetMessage());
    }
}