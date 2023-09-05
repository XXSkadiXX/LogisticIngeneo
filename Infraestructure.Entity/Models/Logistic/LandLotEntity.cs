using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Infraestructure.Entity.Models.Logistic
{
    [ExcludeFromCodeCoverage]
    [Table("LandLot", Schema = "Logistic")]
    public class LandLotEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("TypeProductEntity")]
        public int IdTypeProduct { get; set; }
        public TypeProductEntity TypeProductEntity { get; set; } = null!;

        [Required]
        [ForeignKey("ClientEntity")]
        public int IdClient{ get; set; }
        public ClientEntity ClientEntity { get; set; } = null!;

        [Required]
        public int Amount { get; set; }
        [Required]
        public DateTime RegisterDate { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }

        [Required]
        [ForeignKey("WarehouseEntity")]
        public int IdWarehouse { get; set; }
        public WarehouseEntity WarehouseEntity { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }

        [Required]
        [MaxLength(6)]
        public string VehiclePlate { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string GuideNumber { get; set; } = null!;
    }
}
