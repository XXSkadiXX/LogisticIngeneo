using Infraestructure.Core.Repository.Interface;
using Infraestructure.Entity.Models.General;
using Infraestructure.Entity.Models.Logistic;
using Infraestructure.Entity.Models.Security;

namespace Infraestructure.Core.UnitOfWork.Interface
{
    public interface IUnitOfWork
    {

        //Security
        IRepository<PermissionEntity> PermissionRepository { get; }
        IRepository<RolEntity> RolRepository { get; }
        IRepository<RolesPermissionsEntity> RolesPermissionsRepository { get; }
        IRepository<UserEntity> UserRepository { get; }

        IRepository<CountryEntity> CountryRepository { get; }
        IRepository<ClientEntity> ClientRepository { get; }
        IRepository<LandLotEntity> LandLotRepository { get; }
        IRepository<MaritimeLotEntity> MaritimeLotRepository { get; }
        IRepository<SeaportEntity> SeaportRepository { get; }
        IRepository<TypeProductEntity> TypeProductRepository { get; }
        IRepository<WarehouseEntity> WarehouseRepository { get; }
        Task<int> Save();
    }
}
