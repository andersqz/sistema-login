using System.ComponentModel.DataAnnotations;

namespace Identity.App.Dtos.Roles
{
    public class RoleRequestDto
    {
        [Required(ErrorMessage = "O nome da role é obrigatório.")]
        [StringLength(
            50,
            MinimumLength = 2,
            ErrorMessage = "O nome da role deve ter entre 2 e 50 caracteres."
        )]
        public string Name { get; set; } = string.Empty;

        [StringLength(
            255,
            ErrorMessage = "A descrição deve ter no máximo 255 caracteres."
        )]
        public string Description { get; set; } = string.Empty;
    }
}