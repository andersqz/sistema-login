using Identity.App.Dtos.Roles;

namespace Identity.App.Interfaces.Services
{
    public interface IRoleService
    {
        Task<RoleResponseDto> Create(RoleRequestDto request); 
        Task<bool> ExistsByName(string name);
        Task<IEnumerable<RoleResponseDto>> GetAll();
        Task<RoleResponseDto> GetById(int id);
    }
}