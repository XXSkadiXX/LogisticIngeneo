using Infraestructure.Entity.Models.Logistic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Infraestructure.Entity.Models.General
{
    [ExcludeFromCodeCoverage]
    [Table("Country", Schema = "General")]
    public class CountryEntity
    {
        public CountryEntity()
        {
            ClientEntities = new HashSet<ClientEntity>();
            WarehouseEntities = new HashSet<WarehouseEntity>();
            SeaportEntities = new HashSet<SeaportEntity>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Country { get; set; } = null!;


        public IEnumerable<ClientEntity> ClientEntities { get; set; }
        public IEnumerable<WarehouseEntity> WarehouseEntities { get; set; }
        public IEnumerable<SeaportEntity> SeaportEntities { get; set; }
    }
}
