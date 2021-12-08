using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace GreetingConsoleApp;

public class JsonGreetingWriter : IGreetingWriter
{
    private static readonly string _path;
    static JsonGreetingWriter()
    {
        var settings = Program.InitializeSettings();
        _path = settings.GreetingWriterOutputFilePath;
    }

    public void Write(string message)
    {
        throw new NotImplementedException();                                //do not support 
    }

    public void Write(Greeting greeting)
    {
        Write(new List<Greeting> { greeting });                             //example of code reuse, call batch method by putting greeting in a collection inline when calling the method
    }

    public void Write(IEnumerable<Greeting> greetings)
    {
        var options = new JsonSerializerOptions 
        {
            WriteIndented = true,
        };
        var serializedGreetings = JsonSerializer.Serialize(greetings, options);
        var filename = GetFilename();
        File.WriteAllText(filename, serializedGreetings);

        Console.WriteLine($"Wrote {greetings.Count()} greeting(s) to {filename}");
    }

    //simple logic to always add a timestamp to a file to avoid overwriting existing files - this logic is not fool proof but works for our purpose
    private string GetFilename()
    {
        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        if (_path.EndsWith(".json", StringComparison.InvariantCultureIgnoreCase))
            return _path.Replace(".json", $".{timestamp}.json");

        return $"{_path}.{timestamp}.json";
    }
}