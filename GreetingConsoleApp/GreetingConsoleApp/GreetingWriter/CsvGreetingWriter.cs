using System.Text;

namespace GreetingConsoleApp;

public class CsvGreetingWriter : IGreetingWriter
{
    private static readonly string _path;

    static CsvGreetingWriter()
    {
        var settings = Program.InitializeSettings();
        _path = settings.GreetingWriterOutputFilePath;
    }

    public void Write(string message)
    {
        throw new NotImplementedException();
    }

    public void Write(Greeting greeting)
    {
        Write(new List<Greeting> { greeting });
    }

    public void Write(IEnumerable<Greeting> greetings)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("timestamp;from;to;message");              //write csv headers
        
        foreach (var greeting in greetings)
        {
            stringBuilder.AppendLine($"{greeting.Timestamp};{greeting.From};{greeting.To};{greeting.Message}");
        }

        var filename = GetFilename();
        File.WriteAllText(filename, stringBuilder.ToString());
        
        Console.WriteLine($"Wrote {greetings.Count()} greeting(s) to {filename}");
    }

    //simple logic to always add a timestamp to a file to avoid overwriting existing files - this logic is not fool proof but works for our purpose
    private string GetFilename()
    {
        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        if (_path.EndsWith(".csv", StringComparison.InvariantCultureIgnoreCase))
            return _path.Replace(".csv", $".{timestamp}.csv");

        return $"{_path}.{timestamp}.csv";
    }
}