namespace GreetingConsoleApp;                                           //Everything under the same namespace can reference each other without additional code or config

public class Program                                                    //This class contains the first code that is executed when the program is started
{
    private static GreetingTemplateRepository greetingTemplateRepository = new GreetingTemplateRepository();

    static void Main(string[] args)                                     //The Main method is a special method that is executed when the program is started
    {        
        WriteGreetingToDisk();

        Console.WriteLine("\nDone!\n");
    }

    public static void WriteGreetingToDisk()
    {
        var greeting = new Greeting
        {
            From = "Keen",
            To = "Anton",
            Timestamp = DateTime.Now,
            Message = "This will be written to disk",
            GreetingWriter = new FileGreetingWriter(),
        };

        greeting.WriteMessage();
    }

    public static void PrintTemplatesWithLinq()
    {        
        //Get by length
        var length = 29;
        var templatesByLengthWithLinq = greetingTemplateRepository.GetGreetingTemplatesByLengthWithLinq(length);
        var templatesByLengthWithLambda = greetingTemplateRepository.GetGreetingTemplatesByLengthWithLambda(length);
        var templatesByLengthWithForeach = greetingTemplateRepository.GetGreetingTemplatesByLengthWithForeach(length);

        Console.WriteLine($"\nMessages >= {length} - LINQ");
        foreach (var t in templatesByLengthWithLinq)
        {
            Console.WriteLine(t.GetMessage());
        }

        Console.WriteLine($"\nMessages >= {length} - Lambda expression");
        foreach (var t in templatesByLengthWithLambda)
        {
            Console.WriteLine(t.GetMessage());
        }

        Console.WriteLine($"\nMessages >= {length} - Foreach");
        foreach (var t in templatesByLengthWithForeach)
        {
            Console.WriteLine(t.GetMessage());
        }

        //Get by search string
        var searchString = "year";
        var templatesBySearchStringWithLinq = greetingTemplateRepository.GetGreetingTemplatesBySearchStringWithLinq(searchString);
        var templatesBySearchStringWithLambda = greetingTemplateRepository.GetGreetingTemplatesBySearchStringWithLambda(searchString);
        var templatesBySearchStringWithForeach = greetingTemplateRepository.GetGreetingTemplatesBySearchStringWithForeach(searchString);

        Console.WriteLine($"\nMessages containing '{searchString}' - LINQ");
        foreach (var t in templatesBySearchStringWithLinq)
        {
            Console.WriteLine(t.GetMessage());
        }

        Console.WriteLine($"\nMessages containing '{searchString}' - Lambda");
        foreach (var t in templatesBySearchStringWithLambda)
        {
            Console.WriteLine(t.GetMessage());
        }

        Console.WriteLine($"\nMessages containing '{searchString}' - Foreach");
        foreach (var t in templatesBySearchStringWithForeach)
        {
            Console.WriteLine(t.GetMessage());
        }

        //Get by type
        var type = typeof(ChristmasGreeting);
        var templatesByTypeWithLinq = greetingTemplateRepository.GetGreetingTemplatesByTypeWithLinq(type);
        var templatesByTypeWithLambda = greetingTemplateRepository.GetGreetingTemplatesByTypeWithLambda(type);
        var templatesByTypeWithForeach = greetingTemplateRepository.GetGreetingTemplatesByTypeWithForeach(type);

        Console.WriteLine($"\nMessages of type '{type}' - LINQ");
        foreach (var t in templatesByTypeWithLinq)
        {
            Console.WriteLine(t.GetMessage());
        }

        Console.WriteLine($"\nMessages of type '{type}' - Lambda");
        foreach (var t in templatesByTypeWithLambda)
        {
            Console.WriteLine(t.GetMessage());
        }

        Console.WriteLine($"\nMessages of type '{type}' - Foreach");
        foreach (var t in templatesByTypeWithForeach)
        {
            Console.WriteLine(t.GetMessage());
        }
    }

    public static void PrintTemplate()
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