using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Logistic.Domain.DTO.Logistic.User
{
    [ExcludeFromCodeCoverage]
    public class LoginDto
    {
        [Required(ErrorMessage = "La dirección Email es requerida")]
        [MaxLength(200)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email no es válido")]
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password { get; set; } = null!;
    }
}
