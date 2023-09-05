using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Infraestructure.Entity.Models.Security
{
    [ExcludeFromCodeCoverage]
    [Table("User", Schema = "Security")]
    public class UserEntity
    {

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
        public string Email { get; set; } = null!;

        public DateTime RegisterDate { get; set; }

        [Required]
        public string Password { get; set; } = null!;

        [ForeignKey("RolEntity")]
        public int IdRol { get; set; }

        public RolEntity RolEntity { get; set; } = null!;

        [NotMapped]
        public string FullName { get { return $"{this.Name} {this.LastName}"; } }
    }
}
