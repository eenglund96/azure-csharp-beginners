namespace GreetingConsoleApp;

public class ColorGreetingWriter : IGreetingWriter
{
    public void Write(string message)
    {
        var previousColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine(message);
        Console.ForegroundColor = previousColor;
    }

    public void Write(Greeting greeting)
    {
        Write(greeting.GetMessage());
    }

    public void Write(IEnumerable<Greeting> greetings)
    {
        foreach (var greeting in greetings)
        {
            Write(greeting);
        }
    }
}