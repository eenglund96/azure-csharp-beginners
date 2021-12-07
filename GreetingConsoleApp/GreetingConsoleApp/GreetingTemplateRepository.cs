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

    public IEnumerable<Greeting> GetGreetingTemplatesByLengthWithLinq(int length)
    {
        //This is a simple LINQ query that retrieves all items in GreetingTemplates that has a message with length longer that the input parameter "length"
        //Look at the key words in the query ("from", "in", "where", "select")
        //Check out the type of "templates". A LINQ query always returns an IEnumerable<T>
        var templates = from t in GreetingTemplates                     //which collection do we want to query? Associate each value in the collection with a temp variable (in this case "t") that we can refer to in our query
                        where t.Value.Message.Length >= length          //this is our filter: where [condition == true]
                        select t.Value;                                 //what do we want to retrieve from the collection, in this case we return the entire item. Could also return only some properties in t. Test it out!

        return templates;
    }

    public IEnumerable<Greeting> GetGreetingTemplatesByLengthLambdaWithExpression(int length)
    {
        //this is a lambda expression: Where(t => t.Value.Message.Length >= length)
        //we chain multple method calls to the output of the previous step like this:
        //myCollection.Where(...).Select(...)
        //GreetingTemplates is our collection
        //.Where(t => t.Value.Message.Length >= length) is our filter
        //.Select(t => t.Value) states what we should extract from our collection, use this to project the objects in the collection to another type of object
        //same return type: IEnumerable<T>
        var templates = GreetingTemplates.Where(t => t.Value.Message.Length >= length).Select(t => t.Value);
        return templates;
    }

    public IEnumerable<Greeting> GetGreetingTemplatesByLengthWithForeach(int length)
    {
        var templates = new List<Greeting>();                                                            //needs to match our return type
        foreach (var t in GreetingTemplates)
        {
            if (t.Value.Message.Length >= length)
            {
                templates.Add(t.Value);
            }
        }

        return templates;               //templates is a List<T> which inherits from IEnumerable<T>, no need to cast to our return type: https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1?view=net-6.0
    }

    public IEnumerable<Greeting> GetGreetingTemplatesBySearchStringWithLinq(string searchString)
    {
        var templates = from t in GreetingTemplates
                        where t.Value.Message.Contains(searchString)
                        select t.Value;

        return templates;
    }

    public IEnumerable<Greeting> GetGreetingTemplatesBySearchStringWithLambda(string searchString)
    {
        var templates = GreetingTemplates.Where(t => t.Value.Message.Contains(searchString))
                                         .Select(t => t.Value);                                 //sometimes it's more readable to write each part on a new row
        return templates;
    }

    public IEnumerable<Greeting> GetGreetingTemplatesBySearchStringWithForeach(string searchString)
    {
        var templates = new List<Greeting>();
        foreach (var t in GreetingTemplates)
        {
            if (t.Value.Message.Contains(searchString))
            {
                templates.Add(t.Value);
            }
        }

        return templates;
    }
 
    public IEnumerable<Greeting> GetGreetingTemplatesByTypeWithLinq(Type type)
    {
        var templates = from t in GreetingTemplates
                        where t.Value.GetType() == type
                        select t.Value;

        return templates;
    }

    public IEnumerable<Greeting> GetGreetingTemplatesByTypeWithLambda(Type type)
    {
        var templates = GreetingTemplates.Where(t => t.Value.GetType() == type)
                                         .Select(t => t.Value);                                 
        return templates;
    }

    public IEnumerable<Greeting> GetGreetingTemplatesByTypeWithForeach(Type type)
    {
        var templates = new List<Greeting>();
        foreach (var t in GreetingTemplates)
        {
            if (t.Value.GetType() == type)
            {
                templates.Add(t.Value);
            }
        }

        return templates;
    }
}