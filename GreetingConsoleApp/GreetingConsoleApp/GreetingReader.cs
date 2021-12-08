using System.Text.Json;

namespace GreetingConsoleApp;

public class GreetingReader
{
    public IEnumerable<Greeting> ReadGreetingsFromFile(string path)
    {
        try 
        {
            var fileContents = File.ReadAllText(path);
            var greetings = JsonSerializer.Deserialize<IEnumerable<Greeting>>(fileContents);
            return greetings;
        }
        catch (Exception e) 
        {
            Console.WriteLine($"Encountered exception while reading {path}. Exception: {e}");
        }

        return Enumerable.Empty<Greeting>();                    //return empty collection by default
    }
}