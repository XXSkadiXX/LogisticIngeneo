using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Logistic.Domain.DTO.Logistic.LandLot
{
    [ExcludeFromCodeCoverage]
    public class AddWarehouseDto
    {
        [Required(ErrorMessage = "El nombre de la Bodega es requerida.")]
        [MaxLength(200)]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "La dirección de la Bodega es requerida.")]
        [MaxLength(100)]
        public string Direction { get; set; } = null!;
        [Required(ErrorMessage = "El país es requerido.")]
        public int IdCountry { get; set; }
    }
}
