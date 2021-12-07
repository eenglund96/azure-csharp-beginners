namespace GreetingConsoleApp;                                           //Everything under the same namespace can reference each other without additional code or config

public class Program                                                    //This class contains the first code that is executed when the program is started
{
    static void Main(string[] args)                                     //The Main method is a special method that is executed when the program is started
    {
        Console.WriteLine("\nGreeting message");
        Greeting greeting = new Greeting
        {
            Message = "How are you?",
            From = "Keen",
            To = "Anton",
            Timestamp = DateTime.Now,
        };

        Console.WriteLine(greeting.GetMessage());

        Console.WriteLine("\nNewYearGreeting message");
        NewYearGreeting newYearGreeting = new NewYearGreeting
        {
            Message = "Happy new year!",
            From = "Keen",
            To = "Anton",
            Timestamp = DateTime.Now,
            Year = 2022,
        };

        Console.WriteLine(newYearGreeting.GetMessage());

        Console.WriteLine("\nChristmasGreeting message");
        var christmasGreeting = new ChristmasGreeting                               //using "var christmasGreeting" as type declaration instead of "ChristmasGreeting christmasGreeting"
        {
            Message = "Merry Christmas!",
            From = "Keen",
            To = "Anton",
            Timestamp = DateTime.Now,
            ChristmasPresent = "SDNA13215"
        };

        Console.WriteLine(christmasGreeting.GetMessage());

        Console.WriteLine("\nDone!\n");
    }
}