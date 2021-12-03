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
        try
        {
            var message = GetMessage();
            GreetingWriter.Write(message);
            Console.WriteLine();                                    //Write empty line to make output easier to read
        }
        catch (NullReferenceException)                              //If thrown exception is of type NullReferenceException code will enter here
        {
            Console.WriteLine("ERROR: Failed to write greeting. GreetingWriter or something was null\n");           
        }
        catch (Exception e)                                         //All other exceptions will enter here. Also save the thrown exception in variable 'e' so we can use it in our exception handling
        {
            Console.WriteLine($"Something went wrong, here is the full exception: {e}");
        }
    }
}