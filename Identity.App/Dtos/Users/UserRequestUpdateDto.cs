

using System.ComponentModel.DataAnnotations;

namespace Identity.App.Dtos.Users
{
    public class UserRequestUpdateDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(
            100,
            MinimumLength = 3,
            ErrorMessage = "O nome deve ter entre 3 e 100 caracteres."
        )]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email informado é inválido.")]
        [StringLength(
            255,
            ErrorMessage = "O email deve ter no máximo 255 caracteres."
        )]
        public string Email { get; set; } = string.Empty;
    }
}
