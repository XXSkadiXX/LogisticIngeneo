using Infraestructure.Core.Data;
using Infraestructure.Core.Repository;
using Infraestructure.Core.Repository.Interface;
using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Models.General;
using Infraestructure.Entity.Models.Logistic;
using Infraestructure.Entity.Models.Security;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics.CodeAnalysis;

namespace Infraestructure.Core.UnitOfWork
{
    [ExcludeFromCodeCoverage]
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Attributes
        private readonly DataContext _context;
        private bool disposed = false;
        #endregion Attributes

        #region builder
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        #endregion

        #region Properties

        //security
        private IRepository<PermissionEntity> permissionRepository;
        private IRepository<RolEntity> rolRepository;
        private IRepository<RolesPermissionsEntity> rolesPermissionsRepository;
        private IRepository<UserEntity> userRepository;


        //logistic
        private IRepository<CountryEntity> countryRepository;
        private IRepository<ClientEntity> clientRepository;
        private IRepository<LandLotEntity> landLotRepository;
        private IRepository<MaritimeLotEntity> maritimeLotRepository;
        private IRepository<SeaportEntity> seaportRepository;
        private IRepository<TypeProductEntity> typeProductRepository;
        private IRepository<WarehouseEntity> warehouseRepository;
        #endregion


        #region Members

        //Security

        public IRepository<PermissionEntity> PermissionRepository
        {
            get
            {
                if (this.permissionRepository == null)
                    this.permissionRepository = new Repository<PermissionEntity>(_context);

                return permissionRepository;
            }
        }
        public IRepository<RolEntity> RolRepository
        {
            get
            {
                if (this.rolRepository == null)
                    this.rolRepository = new Repository<RolEntity>(_context);

                return rolRepository;
            }
        }
        public IRepository<RolesPermissionsEntity> RolesPermissionsRepository
        {
            get
            {
                if (this.rolesPermissionsRepository == null)
                    this.rolesPermissionsRepository = new Repository<RolesPermissionsEntity>(_context);

                return rolesPermissionsRepository;
            }
        }

        public IRepository<UserEntity> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                    this.userRepository = new Repository<UserEntity>(_context);

                return userRepository;
            }
        }

        //Logistic
        public IRepository<CountryEntity> CountryRepository
        {
            get
            {
                if (this.countryRepository == null)
                    this.countryRepository = new Repository<CountryEntity>(_context);

                return countryRepository;
            }
        }
        public IRepository<ClientEntity> ClientRepository
        {
            get
            {
                if (this.clientRepository == null)
                    this.clientRepository = new Repository<ClientEntity>(_context);

                return clientRepository;
            }
        }
        public IRepository<LandLotEntity> LandLotRepository
        {
            get
            {
                if (this.landLotRepository == null)
                    this.landLotRepository = new Repository<LandLotEntity>(_context);

                return landLotRepository;
            }
        }
        public IRepository<MaritimeLotEntity> MaritimeLotRepository
        {
            get
            {
                if (this.maritimeLotRepository == null)
                    this.maritimeLotRepository = new Repository<MaritimeLotEntity>(_context);

                return maritimeLotRepository;
            }
        }
        public IRepository<SeaportEntity> SeaportRepository
        {
            get
            {
                if (this.seaportRepository == null)
                    this.seaportRepository = new Repository<SeaportEntity>(_context);

                return seaportRepository;
            }
        }
        public IRepository<TypeProductEntity> TypeProductRepository
        {
            get
            {
                if (this.typeProductRepository == null)
                    this.typeProductRepository = new Repository<TypeProductEntity>(_context);

                return typeProductRepository;
            }
        }
        public IRepository<WarehouseEntity> WarehouseRepository
        {
            get
            {
                if (this.warehouseRepository == null)
                    this.warehouseRepository = new Repository<WarehouseEntity>(_context);

                return warehouseRepository;
            }
        }
        #endregion

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        protected virtual void Dispose(bool disposing)
        {

            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> Save() => await _context.SaveChangesAsync();
    }
}
