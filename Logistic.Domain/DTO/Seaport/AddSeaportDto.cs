using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Logistic.Domain.DTO.Seaport
{
    [ExcludeFromCodeCoverage]
    public class AddSeaportDto
    {
        [Required(ErrorMessage = "El nombre del puerto es requerido")]
        [MaxLength(200)]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "El país es requerido.")]
        public int IdCountry { get; set; }
    }
}
