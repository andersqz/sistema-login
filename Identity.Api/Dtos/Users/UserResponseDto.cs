
using Identity.Api.Dtos.Roles;
using Identity.Api.Models;

namespace Identity.Api.Dtos.Users
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<RoleResponseDto> Roles { get; set; }
    }
}