using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Logistic.Domain.DTO.Logistic.User
{
    [ExcludeFromCodeCoverage]
    public class AddUserDto : LoginDto
    {
        [Required(ErrorMessage = "El Nombre de usuario es requerida")]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "El Apellido de usuario es requerida")]
        [MaxLength(100)]
        public string LastName { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
