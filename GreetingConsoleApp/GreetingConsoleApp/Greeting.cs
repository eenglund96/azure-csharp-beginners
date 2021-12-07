namespace GreetingConsoleApp;
public class Greeting
{
    public string Message { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public DateTime Timestamp { get; set; }

    public virtual string GetMessage()
    {
        return $"{Timestamp}:\n{Message}";
    }
}