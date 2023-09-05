using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Infraestructure.Entity.Models.Security
{
    [ExcludeFromCodeCoverage]
    [Table("Rol", Schema = "Security")]
    public class RolEntity
    {
        public RolEntity()
        {
            UserEntities = new HashSet<UserEntity>();
            RolesPermissionsEntities = new HashSet<RolesPermissionsEntity>();
        }
        [Key]
        public int IdRol { get; set; }

        [MaxLength(100)]
        public string Rol { get; set; } = null!;

        public IEnumerable<UserEntity> UserEntities { get; set; }
        public IEnumerable<RolesPermissionsEntity> RolesPermissionsEntities { get; set; }
    }
}
