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
}