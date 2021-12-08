namespace GreetingConsoleApp;

public class BlackWhiteGreetingWriter : IGreetingWriter
{
    public void Write(string message)
    {
        Console.WriteLine(message);
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