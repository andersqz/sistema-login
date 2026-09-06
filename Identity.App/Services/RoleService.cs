using Identity.App.Dtos.Roles;
using Identity.Domain.Exceptions;
using Identity.Domain.Interfaces.Repositories;
using Identity.App.Interfaces.Services;
using Identity.Domain.Entities;

namespace Identity.App.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        public RoleService(IRoleRepository repository)
            => _repository = repository;

        /// <summary>
        /// salva uma Role no banco de dados
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<RoleResponseDto> Create(RoleRequestDto request)
        {
            await ExistsByName(request.Name);

            Role role = new Role()
            {
              Name = request.Name,
              Description = request.Description  
            };

            role.Id = await _repository.Insert(role);

            RoleResponseDto response = new RoleResponseDto()
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };

            return response;
        }

        /// <summary>
        /// verifica se já existe uma Role no banco pelo nome
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> ExistsByName(string name)
        {
            bool exists = await _repository.ExistsByName(name);

            if (exists)
                throw new RuleBusinessException("Role já existe.");

            return exists;
        }

        /// <summary>
        /// método que retorna uma lista de todas Roles do banco de dados
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<RoleResponseDto>> GetAll()
        {
            IEnumerable<Role> roles = await _repository.SelectAll();
            List<RoleResponseDto> responses = new List<RoleResponseDto>();

            foreach (Role role in roles)
            {
                RoleResponseDto response = new RoleResponseDto()
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description
                };

                responses.Add(response);
            }

            return responses;
        }


        /// <summary>
        /// método que retorna um Role do banco de dados pelo ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<RoleResponseDto> GetById(int id)
        {
            Role? role = await _repository.SelectById(id);

            if (role is null)
                throw new NotFoundException("Role não encontrada.");
            
            RoleResponseDto response = new RoleResponseDto()
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };

            return response;
        }

    }
}