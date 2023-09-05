using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Logistic.Domain.DTO.Logistic.TypeProduct
{
    [ExcludeFromCodeCoverage]
    public class AddTypeProductDto
    {
        [Required(ErrorMessage = "El tipo de producto el requerido.")]
        [MaxLength(100)]
        public string TypeProduct { get; set; } = null!;
        [Required(ErrorMessage = "La descripción del tipo de producto es requerida")]
        [MaxLength(300)]
        public string Description { get; set; } = null!;
    }
}
