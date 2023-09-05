using Logistic.Domain.DTO.Logistic.LandLot;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistic.Domain.DTO.Logistic.Warehouse
{
    [ExcludeFromCodeCoverage]
    public class UpdateWareHouseDto: AddWarehouseDto
    {
        public int Id { get; set; }
    }
}
