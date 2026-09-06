using Identity.App.Dtos.Roles;
using Identity.Domain.Exceptions;
using Identity.App.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{

    [ApiController]
    [Route("api/role")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;
        public RoleController(IRoleService service)
            => _service = service;


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<RoleResponseDto> roles = await _service.GetAll();
                return Ok(roles);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                RoleResponseDto role = await _service.GetById(id);
                return Ok(role);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }

        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RoleRequestDto request)
        {
            try
            {
                RoleResponseDto response = await _service.Create(request);
                return CreatedAtAction(nameof(GetById), new {id = response.Id}, response);
            }
            catch (RuleBusinessException e)
            {
                return Conflict(e.Message);
            }            
        }
    }
}