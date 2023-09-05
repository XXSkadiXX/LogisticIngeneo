using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Logistic.Domain.DTO.Logistic.Client
{
    [ExcludeFromCodeCoverage]
    public class AddClientDto
    {
        [Required(ErrorMessage = "El nombre del cliente es requerido.")]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Los apellidos del cliente son requeridos.")]
        [MaxLength(100)]
        public string LastName { get; set; } = null!;
        [Required(ErrorMessage = "La direción es requerida.")]
        [MaxLength(100)]
        public string Direction { get; set; } = null!;
        [Required(ErrorMessage = "El teléfono es requerido.")]
        [MaxLength(50)]
        public string? Phone { get; set; }
        [Required(ErrorMessage = "El email es requerido.")]
        [MaxLength(300)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "El país es requerido.")]
        public int IdCountry { get; set; }
    }
}
