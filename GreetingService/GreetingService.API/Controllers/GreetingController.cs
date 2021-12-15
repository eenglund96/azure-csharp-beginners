using GreetingService.API.Authentication;
using GreetingService.Core.Entities;
using GreetingService.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GreetingService.API.Controllers
{
    [Route("api/[controller]")]
    [BasicAuth]
    [ApiController]
    public class GreetingController : ControllerBase
    {
        private readonly IGreetingRepository _greetingRepository;

        public GreetingController(IGreetingRepository greetingRepository)
        {
            _greetingRepository = greetingRepository;
        }

        // GET: api/<GreetingController>
        [HttpGet]
        public IEnumerable<Greeting> Get()
        {
            return _greetingRepository.Get();
        }

        // GET api/<GreetingController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Greeting))]        //when we return IActionResult instead of Greeting, there is no way for swagger to know what the return type is, we need to explicitly state what it will return
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(Guid id)
        {
            var greeting = _greetingRepository.Get(id);
            if (greeting == null)
                return NotFound();

            return Ok(greeting);
        }

        // POST api/<GreetingController>
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost]
        public IActionResult Post([FromBody] Greeting greeting)
        {
            try
            {
                _greetingRepository.Create(greeting);
                return Accepted();
            }
            catch                       //any exception will result in 409 Conflict which might not be true but we'll use this for now
            {
                return Conflict();
            }
        }

        // PUT api/<GreetingController>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put([FromBody] Greeting greeting)
        {
            try
            {
                _greetingRepository.Update(greeting);
                return Accepted();
            }
            catch
            {
                return NotFound($"Greeting with {greeting.Id} not found");
            }
        }
    }
}
