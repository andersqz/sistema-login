using Identity.Api.Dtos.Users;
using Identity.Api.Models;

namespace Identity.Api.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserResponseDto> Create(UserRequestDto request);
        Task<bool> Update(UserRequestUpdateDto request, int id);
        Task<bool> Delete(int id);
        Task<IEnumerable<UserResponseDto>> SelectAll();
        Task<UserResponseDto?> SelectById(int id);
        Task<UserResponseDto?> SelectByEmail(string email);
        //Task<bool> ExistsByEmail(string email);
    }
}