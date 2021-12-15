using System.Net.Http.Json;
using System.Text.Json;

namespace GreetingService.API.Client;

public class GreetingServiceClient
{
    private static HttpClient _httpClient = new();                          //always reuse http client to avoid exhausting network resources. A static field is shared by all instances of a class

    private const string _getGreetingsCommand = "get greetings";
    private const string _getGreetingCommand = "get greeting ";
    private const string _writeGreetingCommand = "write greeting ";
    private const string _updateGreetingCommand = "update greeting ";
    private static string _from = "Batman";
    private static string _to = "Superman";

    public static async Task Main(string[] args)
    {
        Console.WriteLine("Welcome to command line Greeting client");
        Console.WriteLine("Enter name of greeting sender:");
        var from = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(from))
            _from = from;

        Console.WriteLine("Enter name of greeting recipient:");
        var to = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(to))
            _to = to;

        while (true)
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine(_getGreetingsCommand);
            Console.WriteLine($"{_getGreetingCommand} [id]");
            Console.WriteLine($"{_writeGreetingCommand} [message]");
            Console.WriteLine($"{_updateGreetingCommand} [id] [message]");

            Console.WriteLine("\nWrite command and press [enter] to execute");

            var command = Console.ReadLine();

            if (string.IsNullOrEmpty(command))
            {
                Console.WriteLine("Command cannot be empty\n");
                continue;
            }

            if (command.Equals(_getGreetingsCommand, StringComparison.OrdinalIgnoreCase))
            {
                await GetGreetingsAsync();
            }
            else if (command.StartsWith(_getGreetingCommand, StringComparison.OrdinalIgnoreCase))
            {
                var idPart = command.Replace(_getGreetingCommand, "");
                if (Guid.TryParse(idPart, out var id))
                {
                    await GetGreetingAsync(id);
                }
                else
                {
                    Console.WriteLine($"{idPart} is not a valid GUID\n");
                }
            }
            else if (command.StartsWith(_writeGreetingCommand, StringComparison.OrdinalIgnoreCase))
            {
                var message = command.Replace(_writeGreetingCommand, "");
                await WriteGreetingAsync(message);
            }
            else if (command.StartsWith(_updateGreetingCommand, StringComparison.OrdinalIgnoreCase))
            {
                var idAndMessagePart = command.Replace(_updateGreetingCommand, "") ?? "";
                var idPart = idAndMessagePart.Split(" ").First();
                var messagePart = idAndMessagePart.Replace(idPart, "").Trim();

                if (Guid.TryParse(idPart, out var id))
                {
                    await UpdateGreetingAsync(id, messagePart);
                }
                else
                {
                    Console.WriteLine($"{idPart} is not a valid GUID");
                }
            }
            else
            {
                Console.WriteLine("Command not recognized\n");
            }
        }
    }

    /// <summary>
    /// async key word means this method is awaitable. Return type Task is equivalent to void in a sync (normal) method
    /// IO operations (network, disk, db, etc) are slow. Using await async allows the CPU to work on other operations while waiting for the async operation to return.
    /// In general use async methods if possible and follow the principle of "async all the way"
    /// </summary>
    /// <returns></returns>
    private static async Task GetGreetingsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("http://localhost:5020/api/greeting");
            response.EnsureSuccessStatusCode();                                                 //throws exception if HTTP response status is not a success status
            var responseBody = await response.Content.ReadAsStringAsync();

            //Do something with response
            var greetings = JsonSerializer.Deserialize<IEnumerable<Greeting>>(responseBody);

            foreach (var greeting in greetings)
            {
                Console.WriteLine($"[{greeting.id}] [{greeting.timestamp}] ({greeting.from} -> {greeting.to}) - {greeting.message}");
            }

            Console.WriteLine();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Get greetings failed: {e.Message}\n");
        }
    }

    private static async Task GetGreetingAsync(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"http://localhost:5020/api/greeting/{id}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            //Do something with response;
            var greeting = JsonSerializer.Deserialize<Greeting>(responseBody);
            Console.WriteLine($"[{greeting.id}] [{greeting.timestamp}] ({greeting.from} -> {greeting.to}) - {greeting.message}\n");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Get greeting failed: {e.Message}\n");
        }
    }

    private static async Task WriteGreetingAsync(string message)
    {
        try
        {
            var greeting = new Greeting
            {
                from = _from,
                to = _to,
                message = message,
            };
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5020/api/greeting", greeting);
            Console.WriteLine($"Wrote greeting. Service responded with: {response.StatusCode}");            //all HTTP responses always contain a status code
            Console.WriteLine();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Write greeting failed: {e.Message}\n");
        }
    }

    private static async Task UpdateGreetingAsync(Guid id, string message)
    {
        try
        {
            var greeting = new Greeting
            {
                id = id,
                from = _from,
                to = _to,
                message = message,
            };
            var response = await _httpClient.PutAsJsonAsync("http://localhost:5020/api/greeting", greeting);
            Console.WriteLine($"Updated greeting. Service responded with: {response.StatusCode}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Update greeting failed: {e.Message}\n");
        }
    }
}