namespace GreetingConsoleApp;
public class Greeting
{
    public string Message { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public DateTime Timestamp { get; set; }
    public IGreetingWriter GreetingWriter { get; set; }

    public virtual string GetMessage()
    {
        return $"{Timestamp}:\n{Message}";
    }

    public void WriteMessage()
    {
        var message = GetMessage();
        GreetingWriter.Write(message);
        Console.WriteLine();                    //Write empty line to make output easier to read
    }
}