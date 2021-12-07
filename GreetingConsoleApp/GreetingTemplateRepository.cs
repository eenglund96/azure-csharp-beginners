namespace GreetingConsoleApp;

public class GreetingTemplateRepository
{
    public Dictionary<int, Greeting> GreetingTemplates { get; set; } = new Dictionary<int, Greeting>();
    
    public GreetingTemplateRepository()
    {
        var christmasTemplate = new ChristmasGreeting
        {
            Message = "A generic christmas greeting!",
            Timestamp = DateTime.Now,
            ChristmasPresent = "DSAN13284",
        };
        SaveGreetingTemplate(1, christmasTemplate);           //Save a greeting in the repo

        var newYearGreetingTemplate = new NewYearGreeting
        {
            Message = "A generic new year greeting!",
            Timestamp = DateTime.Now,
            Year = 2022,
        };
        SaveGreetingTemplate(2, newYearGreetingTemplate);       //Save another greeting in the repo
    }

    public Greeting GetGreetingTemplate(int id)
    {
        if (GreetingTemplates.TryGetValue(id, out var greeting))
            return greeting;

        throw new KeyNotFoundException($"Template with id {id} not found");
    }

    public int SaveGreetingTemplate(int id, Greeting greeting)
    {
        if (GreetingTemplates.TryAdd(id, greeting))
            return id;

        throw new Exception($"Failed to save GreetingTemplate, key {id} already exists");
    }
}