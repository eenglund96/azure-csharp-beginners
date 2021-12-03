namespace GreetingConsoleApp;

public class NewYearGreeting : Greeting 
{
    public int Year { get; set; }

    public override string GetMessage()
    {
        return $"{base.GetMessage()}\nLooking forward to {Year}!";                  //resuse GetMessage() logic from base class (in this case the Greeting class) to avoid duplicate code
    }
}