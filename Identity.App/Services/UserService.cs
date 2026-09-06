
using Identity.App.Dtos.Roles;
using Identity.App.Dtos.Users;
using Identity.Domain.Exceptions;
using Identity.Domain.Interfaces.Repositories;
using Identity.App.Interfaces.Services;
using Identity.Domain.Entities;

namespace Identity.App.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
            => _repository = repository;


        public async Task ExistsByEmail(string email)
        {
            bool exists = await _repository.ExistsByEmail(email);
            
            if (exists)
            {
                throw new RuleBusinessException("E-mail já cadastrado.");
            }  
        }


        public async Task<UserResponseDto> Create(UserRequestDto request)
        {
            await ExistsByEmail(request.Email);

            User u = new User()
            {
                Name = request.Name,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            u.Id = await _repository.Insert(u);

            UserResponseDto response = new()
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                CreatedAt = u.CreatedAt
            };

            return response;
        }

        public async Task<bool> Delete(int id)
        {
            User? u = await _repository.SelectById(id);

            if (u is null)
                throw new NotFoundException("Usuário não encontrado");

            u.Roles.Clear();

            return await _repository.Delete(u);
        }

        public async Task<IEnumerable<UserResponseDto>> SelectAll()
        {
            IEnumerable<User> users = await _repository.SelectAll();
            List<UserResponseDto> responses = new();

            foreach (User u in users)
            {
                responses.Add(new UserResponseDto()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    CreatedAt = u.CreatedAt,

                    Roles = u.Roles.Select(r => new RoleResponseDto()
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Description = r.Description
                    }).ToList()
                });
            }

            return responses;
        }

        public async Task<UserResponseDto?> SelectByEmail(string email)
        {
            User? u = await _repository.SelectByEmail(email);

            if (u is null)
            {
                throw new NotFoundException($"Usuário ID {email} não encontrado.");
            }

            return new UserResponseDto()
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                CreatedAt = u.CreatedAt
            };
        }

        public async Task<UserResponseDto?> SelectById(int id)
        {
            User? u = await _repository.SelectById(id);

            if (u is null)
            {
                throw new NotFoundException($"Usuário ID {id} não encontrado.");
            }

            return new UserResponseDto()
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                CreatedAt = u.CreatedAt
            };
        }

        public async Task<bool> Update(UserRequestUpdateDto request, int id)
        {
            User? u = await _repository.SelectById(id);

            if (u is null)
                throw new NotFoundException("Usuário não encontrado");

            u.Name = request.Name;
            u.Email = request.Email;

            return await _repository.Update(u);
        }
    }
}