namespace GreetingConsoleApp;

public class BlackWhiteGreetingWriter : IGreetingWriter
{
    public void Write(string message)
    {
        Console.WriteLine(message);
    }
}