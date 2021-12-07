namespace GreetingConsoleApp;

public class ChristmasGreeting : Greeting
{
    public string ChristmasPresent { get; set; }

    public override string GetMessage()
    {
        return $"{base.GetMessage()}\nHere's your present: {ChristmasPresent}";                   //Not reusing GetMessage() logic from base class. Try updating GetMessage in base class (Greeting class) and see what happens
    }
}