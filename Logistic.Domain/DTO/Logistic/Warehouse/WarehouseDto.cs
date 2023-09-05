using Logistic.Domain.DTO.Logistic.Warehouse;
using System.Diagnostics.CodeAnalysis;

namespace Logistic.Domain.DTO.Logistic.LandLot
{
    [ExcludeFromCodeCoverage]
    public class WarehouseDto : UpdateWareHouseDto
    {
        public string Country { get; set; }
    }
}
