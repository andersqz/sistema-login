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
        private readonly ILogger<RoleController> _logger;
        private readonly IRoleService _service;
        public RoleController(IRoleService service, ILogger<RoleController> logger)
        {
            _service = service;
            _logger = logger;
        } 


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Buscando todas as Roles");
            IEnumerable<RoleResponseDto> roles = await _service.GetAll();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Buscando a role {id}", id);
            RoleResponseDto role = await _service.GetById(id);
            return Ok(role);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RoleRequestDto request)
        {
            _logger.LogInformation("Criando uma Role");
            RoleResponseDto response = await _service.Create(request);
            return CreatedAtAction(nameof(GetById), new {id = response.Id}, response);          
        }
    }
}