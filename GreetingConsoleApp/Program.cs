namespace GreetingConsoleApp;                                           //Everything under the same namespace can reference each other without additional code or config

public class Program                                                    //This class contains the first code that is executed when the program is started
{
    private static GreetingTemplateRepository greetingTemplateRepository = new GreetingTemplateRepository();

    static void Main(string[] args)                                     //The Main method is a special method that is executed when the program is started
    {
        Console.WriteLine("Available templates:");
        
        PrintTemplatesWithLinq();

        Console.WriteLine("\nDone!\n");
    }

    public static void PrintTemplatesWithLinq()
    {        
        var length = 29;
        var templatesWithLinq = greetingTemplateRepository.GetGreetingTemplatesWithLongMessageWithLinq(length);
        var templatesWithLambda = greetingTemplateRepository.GetGreetingTemplatesWithLongMessageWithLambdaExpression(length);
        var templatesWithForeach = greetingTemplateRepository.GetGreetingTemplatesWithLongMessageWithForeach(length);

        Console.WriteLine("\nResult with LINQ");
        foreach (var t in templatesWithLinq)
        {
            Console.WriteLine(t.Value.GetMessage());
        }

        Console.WriteLine("\nResult with Lambda expression");
        foreach (var t in templatesWithLambda)
        {
            Console.WriteLine(t.Value.GetMessage());
        }

        Console.WriteLine("\nResult with foreach");
        foreach (var t in templatesWithForeach)
        {
            Console.WriteLine(t.Value.GetMessage());
        }
    }

    public static void PrintTemplate()
    {
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