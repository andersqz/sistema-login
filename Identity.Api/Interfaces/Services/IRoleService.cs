using Identity.Api.Dtos.Roles;
using Identity.Api.Models;

namespace Identity.Api.Interfaces.Services
{
    public interface IRoleService
    {
        Task<RoleResponseDto> Create(RoleRequestDto request); 
        Task<bool> ExistsByName(string name);
        Task<IEnumerable<RoleResponseDto>> GetAll();
        Task<RoleResponseDto> GetById(int id);
    }
}