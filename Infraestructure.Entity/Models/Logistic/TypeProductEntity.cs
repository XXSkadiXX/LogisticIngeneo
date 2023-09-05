using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Infraestructure.Entity.Models.Logistic
{
    [ExcludeFromCodeCoverage]
    [Table("TypeProduct", Schema = "Logistic")]
    public class TypeProductEntity
    {
        public TypeProductEntity()
        {
            MaritimeLotEntities = new HashSet<MaritimeLotEntity>();
            LandLotEntities = new HashSet<LandLotEntity>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string TypeProduct { get; set; } = null!;

        [Required]
        [MaxLength(300)]
        public string Description { get; set; } = null!;

        public IEnumerable<MaritimeLotEntity> MaritimeLotEntities { get; set; }
        public IEnumerable<LandLotEntity> LandLotEntities { get; set; }
    }
}
