namespace GreetingConsoleApp;

public class GreetingTemplateRepository
{
    public Dictionary<int, Greeting> GreetingTemplates { get; set; }
    
    public GreetingTemplateRepository()
    {
        GreetingTemplates = new Dictionary<int, Greeting>();
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