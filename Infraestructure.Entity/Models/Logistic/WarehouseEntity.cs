using Infraestructure.Entity.Models.General;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Infraestructure.Entity.Models.Logistic
{
    [ExcludeFromCodeCoverage]
    [Table("Warehouses", Schema = "Logistic")]
    public class WarehouseEntity
    {
        public WarehouseEntity()
        {
            LandLotEntities = new HashSet<LandLotEntity>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Warehouse { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Direction { get; set; } = null!;

        [ForeignKey("CountryEntity")]
        public int IdCountry { get; set; }
        public CountryEntity CountryEntity { get; set; } = null!;

        public IEnumerable<LandLotEntity> LandLotEntities { get; set; }
    }
}
