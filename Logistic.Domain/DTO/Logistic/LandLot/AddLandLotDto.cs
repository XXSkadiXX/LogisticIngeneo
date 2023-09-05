using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Logistic.Domain.DTO.Logistic.LandLot
{
    [ExcludeFromCodeCoverage]
    public class AddLandLotDto
    {
        public int IdTypeProduct { get; set; }
        public int IdClient { get; set; }
        public int Amount { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int IdWarehouse { get; set; }
        public decimal Price { get; set; }

        [Required]
        [MaxLength(6)]
        public string VehiclePlate { get; set; } = null!;

    }
}
