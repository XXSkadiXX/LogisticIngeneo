using Infraestructure.Entity.Models.General;
using Infraestructure.Entity.Models.Logistic;
using Infraestructure.Entity.Models.Security;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Infraestructure.Core.Data
{
    [ExcludeFromCodeCoverage]
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<CountryEntity> CountryEntity { get; set; }
        public DbSet<ClientEntity> ClientEntity { get; set; }
        public DbSet<LandLotEntity> LandLotEntity { get; set; }
        public DbSet<MaritimeLotEntity> MaritimeLotEntity { get; set; }
        public DbSet<SeaportEntity> SeaportEntity { get; set; }
        public DbSet<TypeProductEntity> TypeProductEntity { get; set; }
        public DbSet<WarehouseEntity> WarehouseEntity { get; set; }


        //Security
        public DbSet<PermissionEntity> PermissionEntity { get; set; }
        public DbSet<RolEntity> RolEntity { get; set; }
        public DbSet<RolesPermissionsEntity> RolesPermissionsEntity { get; set; }
        public DbSet<UserEntity> UserEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                      .HasIndex(b => b.Email)
                      .IsUnique();

            modelBuilder.Entity<TypeProductEntity>()
                      .HasIndex(b => b.TypeProduct)
                      .IsUnique();

            modelBuilder.Entity<MaritimeLotEntity>(entity =>
            {
                entity.HasOne(d => d.SeaportEntity)
                      .WithMany(a => a.MaritimeLotEntities)
                      .HasForeignKey(e => e.IdSeaport)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("MaritimeLotEntity_xx_constraint");

            });

            modelBuilder.Entity<LandLotEntity>(entity =>
            {
                entity.HasOne(d => d.WarehouseEntity)
                      .WithMany(a => a.LandLotEntities)
                      .HasForeignKey(e => e.IdWarehouse)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("LandLotEntity_xx_constraint");

            });

            modelBuilder.Entity<RolEntity>().Property(t => t.IdRol).ValueGeneratedNever();
            modelBuilder.Entity<PermissionEntity>().Property(t => t.IdPermission).ValueGeneratedNever();
        }
    }
}
