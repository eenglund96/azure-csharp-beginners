using System.Xml;
using System.Xml.Serialization;
using Microsoft.Extensions.Configuration;

namespace GreetingConsoleApp;

public class XmlGreetingWriter : IGreetingWriter
{
    private readonly string _path;
    public XmlGreetingWriter()
    {
        //duplicate IConfiguration code here, not optimal but we'll run with it for now
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var settings = config.GetRequiredSection("Settings").Get<Settings>();
        _path = settings.GreetingWriterOutputFilePath;
    }

    public void Write(string message)
    {
        throw new NotImplementedException();
    }

    public void Write(Greeting greeting)
    {
        throw new NotImplementedException();
    }

    public void Write(IEnumerable<Greeting> greetings)
    {
        var filename = GetFilename();        
        var xmlWriterSettings = new XmlWriterSettings 
        {
            Indent = true,
        };
        using var xmlWriter = XmlWriter.Create(filename, xmlWriterSettings);
        var serializer = new XmlSerializer(typeof(List<Greeting>));                             //this xml serializer does not support serializing interfaces, need to convert to a concrete class
        serializer.Serialize(xmlWriter, greetings.ToList());                                   //convert our greetings of type IEnumerable (interface) to List (concrete class)
        
        Console.WriteLine($"Wrote {greetings.Count()} greeting(s) to {filename}");
    }

    private string GetFilename()
    {
        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        if (_path.EndsWith(".xml", StringComparison.InvariantCultureIgnoreCase))
            return _path.Replace(".xml", $".{timestamp}.xml");

        return $"{_path}.{timestamp}.xml";
    }
}