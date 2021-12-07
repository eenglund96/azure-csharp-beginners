namespace GreetingConsoleApp;                                           //Everything under the same namespace can reference each other without additional code or config

public class Program                                                    //This class contains the first code that is executed when the program is started
{
    private static GreetingTemplateRepository greetingTemplateRepository = new GreetingTemplateRepository();

    static void Main(string[] args)                                     //The Main method is a special method that is executed when the program is started
    {
        PrintGreetingTemplate();        

        Console.WriteLine("\nDone!\n");
    }

    public static void PrintGreetingTemplate()
    {
        Console.WriteLine("Available templates:");

        foreach (var template in greetingTemplateRepository.GreetingTemplates)
        {
            Console.WriteLine($"ID: {template.Key} - Message: {template.Value.Message}");
        }

        Console.WriteLine("\nEnter template ID to print:");

        try
        {
            var id = int.Parse(Console.ReadLine());
            var greeting = greetingTemplateRepository.GetGreetingTemplate(id);
            Console.WriteLine(greeting.GetMessage());
        }
        catch
        {
            Console.WriteLine("Failed to print template");
        }
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