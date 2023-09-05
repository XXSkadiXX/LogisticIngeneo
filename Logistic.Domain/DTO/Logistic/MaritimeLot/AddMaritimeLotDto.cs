using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Logistic.Domain.DTO.Logistic.MaritimeLot
{
    [ExcludeFromCodeCoverage]
    public class AddMaritimeLotDto
    {
        public int IdTypeProduct { get; set; }
        public int IdClient { get; set; }
        public int Amount { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }

        [Required]
        public int IdSeaport { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [MaxLength(8)]
        public string FleetNumber { get; set; } = null!;
    }
}
