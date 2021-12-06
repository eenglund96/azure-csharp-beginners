namespace GreetingConsoleApp;                                           //Everything under the same namespace can reference each other without additional code or config

public class Program                                                    //This class contains the first code that is executed when the program is started
{
    static void Main(string[] args)                                     //The Main method is a special method that is executed when the program is started
    {
        Console.WriteLine("Available templates:");

        var repo = InitGreetingTemplateRepository();

        foreach (var template in repo.GreetingTemplates)
        {
            Console.WriteLine($"ID: {template.Key} - Message: {template.Value.Message}");
        }

        Console.WriteLine("Enter template ID to print:");

        try
        {
            var id = int.Parse(Console.ReadLine());
            var greeting = repo.GetGreetingTemplate(id);
            Console.WriteLine(greeting.GetMessage());
        }
        catch
        {
            Console.WriteLine("Failed to print template");
        }

        Console.WriteLine("\nDone!\n");
    }

    public static void ProcessGreeting(Greeting greeting)
    {
        greeting.WriteMessage();
    }

    public static GreetingTemplateRepository InitGreetingTemplateRepository()
    {
        var repo = new GreetingTemplateRepository();

        var christmasTemplate = new ChristmasGreeting
        {
            Message = "A generic christmas greeting!",
            Timestamp = DateTime.Now,
            ChristmasPresent = "DSAN13284",
        };
        repo.SaveGreetingTemplate(1, christmasTemplate);           //Save a greeting in the repo

        var newYearGreetingTemplate = new NewYearGreeting
        {
            Message = "A generic new year greeting!",
            Timestamp = DateTime.Now,
            Year = 2022,
        };
        repo.SaveGreetingTemplate(2, newYearGreetingTemplate);       //Save another greeting in the repo

        return repo;
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