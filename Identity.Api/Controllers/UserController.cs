using Identity.Api.Dtos.Users;
using Identity.Api.Exceptions;
using Identity.Api.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;


namespace Identity.Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<UserResponseDto> users = await _service.SelectAll();

            return Ok(users);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                UserResponseDto? user = await _service.SelectById(id);
                return Ok(user);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                UserResponseDto? user = await _service.SelectByEmail(email);
                return Ok(user);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserRequestDto request)
        {
            try
            {
                UserResponseDto response = await _service.Create(request);
                return CreatedAtAction(nameof(GetById), new {id = response.Id}, response);
            }
            catch (RuleBusinessException e)
            {
                return Conflict(e.Message);
            }

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromBody] UserRequestUpdateDto request, int id)
        {
            try
            {
                bool IsUpdated = await _service.Update(request, id);
                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (RuleBusinessException e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.Delete(id);
                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}