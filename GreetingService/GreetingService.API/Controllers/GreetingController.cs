using GreetingService.Core.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GreetingService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreetingController : ControllerBase
    {
        // GET: api/<GreetingController>
        [HttpGet]
        public IEnumerable<Greeting> Get()
        {
            return new Greeting[] 
            {
                new Greeting
                {
                    From = "Keen",
                    To = "Anton",
                    Message = "Hello",
                },
                new Greeting
                {
                    From = "Keen",
                    To = "Anton",
                    Message = "Hello again",
                },
            };
        }

        // GET api/<GreetingController>/5
        [HttpGet("{id}")]
        public Greeting Get(Guid id)
        {
            return new Greeting
            {
                From = "Keen",
                To = "Anton",
                Message = "Hello",
            };
        }

        // POST api/<GreetingController>
        [HttpPost]
        public void Post([FromBody] Greeting greeting)
        {
            Console.WriteLine($"Create: {greeting.Message}");
        }

        // PUT api/<GreetingController>/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Greeting greeting)
        {
            Console.WriteLine($"Update: {greeting.Message}");
        }
    }
}
