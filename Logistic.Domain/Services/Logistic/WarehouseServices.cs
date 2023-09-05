using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Models.Logistic;
using Logistic.Common.Exceptions;
using Logistic.Common.Resources;
using Logistic.Domain.DTO.Logistic.LandLot;
using Logistic.Domain.DTO.Logistic.Warehouse;
using Logistic.Domain.Services.Logistic.Interfaces;

namespace Logistic.Domain.Services.Logistic
{
    public class WarehouseServices : IWarehouseServices
    {
        #region Attribute
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Builder
        public WarehouseServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        private WarehouseEntity GetWarehouse(int idWarehouse) => _unitOfWork.WarehouseRepository.FirstOrDefault(x => x.Id == idWarehouse);

        public List<WarehouseDto> GetAllWarehouses()
        {
            IEnumerable<WarehouseEntity> listWarehouses = _unitOfWork.WarehouseRepository.GetAll(p => p.CountryEntity);

            List<WarehouseDto> resultWarehouses = listWarehouses.Select(x => new WarehouseDto()
            {
                Id = x.Id,
                Name = x.Warehouse,
                Direction = x.Direction,
                IdCountry = x.IdCountry,
                Country = x.CountryEntity.Country

            }).ToList();

            return resultWarehouses;
        }

        public List<WarehouseDto> GetAllWarehousesByCountry(int countryId)
        {
            IEnumerable<WarehouseEntity> listWarehouses = _unitOfWork.WarehouseRepository
              .FindAll(p => p.IdCountry == countryId,
                p => p.CountryEntity);

            List<WarehouseDto> resultWarehouses = listWarehouses.Select(x => new WarehouseDto()
            {
                Id = x.Id,
                Name = x.Warehouse,
                Direction = x.Direction,
                IdCountry = x.IdCountry,
                Country = x.CountryEntity.Country

            }).ToList();

            return resultWarehouses;
        }

        public async Task<bool> InsertWarehouse(AddWarehouseDto addWarehouse)
        {
            if (ExistedWarehouse(addWarehouse.Name))
                throw new BusinessException(GeneralMessages.ExistedRegister);

            WarehouseEntity newWarehouse = new WarehouseEntity()
            {
                Warehouse = addWarehouse.Name,
                Direction = addWarehouse.Direction,
                IdCountry = addWarehouse.IdCountry,
            };

            _unitOfWork.WarehouseRepository.Insert(newWarehouse);

            return await _unitOfWork.Save() > 0;
        }

        public async Task<bool> UpdateWarehouse(UpdateWareHouseDto uWarehouse)
        {
            WarehouseEntity updateWarehouse = GetWarehouse(uWarehouse.Id);
            if (updateWarehouse == null)
                throw new BusinessException(GeneralMessages.ItemNoFound);

            updateWarehouse.Warehouse = uWarehouse.Name;
            updateWarehouse.Direction = uWarehouse.Direction;
            updateWarehouse.IdCountry = uWarehouse.IdCountry;

            _unitOfWork.WarehouseRepository.Update(updateWarehouse);

            return await _unitOfWork.Save() > 0;
        }

        public async Task<bool> DeleteWarehouse(int idWarehouse)
        {
            WarehouseEntity warehouse = GetWarehouse(idWarehouse);
            if (warehouse == null)
                throw new BusinessException(GeneralMessages.ItemNoFound);

            _unitOfWork.WarehouseRepository.Delete(warehouse);

            return await _unitOfWork.Save() > 0;
        }

        private bool ExistedWarehouse(string warehouse)
        {
            bool result = false;
            if (_unitOfWork.WarehouseRepository.FirstOrDefault(x => x.Warehouse.ToLower() == warehouse.ToLower()) != null)
                result = true;

            return result;

        }

        #endregion
    }
}
