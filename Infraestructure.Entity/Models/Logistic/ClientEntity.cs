using Infraestructure.Entity.Models.General;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Infraestructure.Entity.Models.Logistic
{
    [ExcludeFromCodeCoverage]
    [Table("Client", Schema = "Logistic")]
    public class ClientEntity
    {
        public ClientEntity()
        {
            MaritimeLotEntities = new HashSet<MaritimeLotEntity>();
            LandLotEntities = new HashSet<LandLotEntity>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Direction { get; set; } = null!;

        [MaxLength(50)]
        public string? Phone { get; set; }

        [MaxLength(300)]
        public string? Email { get; set; }

        [ForeignKey("CountryEntity")]
        public int IdCountry { get; set; }
        public CountryEntity CountryEntity { get; set; } = null!;

        public IEnumerable<MaritimeLotEntity> MaritimeLotEntities { get; set; }
        public IEnumerable<LandLotEntity> LandLotEntities { get; set; }



        [NotMapped]
        public string FullName { get { return $"{this.Name} {this.LastName}"; } }
    }
}
