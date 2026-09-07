using Identity.App.Dtos.Users;
using Identity.Domain.Exceptions;
using Identity.App.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;


namespace Identity.Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _service;
        public UserController(IUserService service, ILogger<UserController> logger)
        {
            _service = service;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Buscando todos os Users");
            IEnumerable<UserResponseDto> users = await _service.SelectAll();

            return Ok(users);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Buscando o User ID {id}", id);
            UserResponseDto? user = await _service.SelectById(id);
            return Ok(user);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            _logger.LogInformation("Buscando o User E-mail {email}", email);
            UserResponseDto? user = await _service.SelectByEmail(email);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserRequestDto request)
        {
            _logger.LogInformation("Criando um novo User");
            UserResponseDto response = await _service.Create(request);
            return CreatedAtAction(nameof(GetById), new {id = response.Id}, response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromBody] UserRequestUpdateDto request, int id)
        {
            _logger.LogInformation("Atualizando o User ID {id}", id);
            bool IsUpdated = await _service.Update(request, id);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deletando o User ID {id}", id);
            await _service.Delete(id);
            return NoContent();
        }
    }
}