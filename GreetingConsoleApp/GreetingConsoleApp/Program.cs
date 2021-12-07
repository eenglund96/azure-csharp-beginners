namespace GreetingConsoleApp;                                           //Everything under the same namespace can reference each other without additional code or config

public class Program                                                    //This class contains the first code that is executed when the program is started
{
    static void Main(string[] args)                                     //The Main method is a special method that is executed when the program is started
    {
        var greeting = new Greeting
        {
            Message = "How are you?",
            From = "Keen",
            To = "Anton",
            Timestamp = DateTime.Now,
            GreetingWriter = new BlackWhiteGreetingWriter(),
        };

        var newYearGreeting = new NewYearGreeting
        {
            Message = "Happy new year!",
            From = "Keen",
            To = "Anton",
            Timestamp = DateTime.Now,
            Year = 2022,
            GreetingWriter = new ColorGreetingWriter(),              //Commented out to make this throw NullReferenceException
        };

        var christmasGreeting = new ChristmasGreeting
        {
            Message = "Merry Christmas!",
            From = "Keen",
            To = "Anton",
            Timestamp = DateTime.Now,
            ChristmasPresent = "SDNA13215",
            GreetingWriter = new BlackWhiteGreetingWriter(),
        };

        ProcessGreeting(greeting);                                      //Process a single greeting
        
        var greetings = new List<Greeting>();
        greetings.Add(greeting);
        greetings.Add(newYearGreeting);
        greetings.Add(christmasGreeting);

        ProcessGreetings(greetings);                                    //Process multiple greetings

        var largeGreetingsBatch = GenerateGreetings(1000);
        ProcessGreetings(largeGreetingsBatch);

        Console.WriteLine("\nDone!\n");
    }

    public static void ProcessGreeting(Greeting greeting)
    {
        greeting.WriteMessage();
    }

    public static void ProcessGreetings(List<Greeting> greetings)
    {
        Console.WriteLine($"### PROCESSING BATCH OF {greetings.Count} GREETING(S) ###");
        
        foreach (var greeting in greetings)
        {
            ProcessGreeting(greeting);                                  //Reuse ProcessGreeting()
        }

        Console.WriteLine($"### FINISHED PROCESSING BATCH OF {greetings.Count} GREETING(S) ###");
    } 

    public static List<Greeting> GenerateGreetings(int count)
    {
        var greetings = new List<Greeting>();
        for(var i=1;i<=count;i++)
        {
            var greeting = new Greeting
            {
                Message = $"This is greeting no {i}",
                Timestamp = DateTime.Now,
                GreetingWriter = new BlackWhiteGreetingWriter(),
            };
            greetings.Add(greeting);
        }

        return greetings;
    }
}