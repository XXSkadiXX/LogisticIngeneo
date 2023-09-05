using Infraestructure.Entity.Models.General;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Infraestructure.Entity.Models.Logistic
{
    [ExcludeFromCodeCoverage]
    [Table("Seaports", Schema = "Logistic")]
    public class SeaportEntity
    {
        public SeaportEntity()
        {
            MaritimeLotEntities = new HashSet<MaritimeLotEntity>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Seaport { get; set; } = null!;

        [ForeignKey("CountryEntity")]
        public int IdCountry { get; set; }
        public CountryEntity CountryEntity { get; set; } = null!;

        public IEnumerable<MaritimeLotEntity> MaritimeLotEntities { get; set; }
    }
}
