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

    public Dictionary<int, Greeting> GetGreetingTemplatesWithLongMessageWithLinq(int length)
    {
        //This is a simple LINQ query that retrieves all items in GreetingTemplates that has a message with length longer that the input parameter "length"
        //Look at the key words in the query ("from", "in", "where", "select")
        //
        //Check out the type of "templates". A LINQ query always returns an IEnumerable<T>
        IEnumerable<KeyValuePair<int, Greeting>> templates = from t in GreetingTemplates                             //which collection do we want to query? Associate each value in the collection with a temp variable (in this case "t") that we can refer to in our query
                                                             where t.Value.Message.Length >= length                  //this is our filter: where [condition == true]
                                                             select t;                                               //what do we want to retrieve from the collection, in this case we return the entire item. Could also return only some properties in t. Test it out!

        Dictionary<int, Greeting> dictionary = new Dictionary<int, Greeting>(templates);                             //need to convert template to a Dictionary<int, Greeting> to match our return type
        return dictionary;
    }

    public Dictionary<int, Greeting> GetGreetingTemplatesWithLongMessageWithLambdaExpression(int length)
    {
        //this is a lambda expression: Where(t => t.Value.Message.Length >= length)
        //same return type: IEnumerable<T>
        IEnumerable<KeyValuePair<int, Greeting>> templates = GreetingTemplates.Where(t => t.Value.Message.Length >= length);

        Dictionary<int, Greeting> dictionary = new Dictionary<int, Greeting>(templates);                             //need to convert template to a Dictionary<int, Greeting> to match our return type
        return dictionary;
    }

    public Dictionary<int, Greeting> GetGreetingTemplatesWithLongMessageWithForeach(int length)
    {
        var dictionary = new Dictionary<int, Greeting>();                                                            //needs to match our return type
        foreach (var t in GreetingTemplates)
        {
            if (t.Value.Message.Length >= length)
            {
                dictionary.Add(t.Key, t.Value);
            }
        }

        return dictionary;
    }
}