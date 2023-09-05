using Logistic.Domain.DTO.Logistic.LandLot;
using Logistic.Domain.DTO.Logistic.Warehouse;

namespace Logistic.Domain.Services.Logistic.Interfaces
{
    public interface IWarehouseServices
    {
        List<WarehouseDto> GetAllWarehouses();
        List<WarehouseDto> GetAllWarehousesByCountry(int countryId);
        Task<bool> InsertWarehouse(AddWarehouseDto addWarehouse);
        Task<bool> UpdateWarehouse(UpdateWareHouseDto uWarehouse);
        Task<bool> DeleteWarehouse(int idWarehouse);

    }
}
