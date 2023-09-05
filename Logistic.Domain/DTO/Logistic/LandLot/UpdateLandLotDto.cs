using System.Diagnostics.CodeAnalysis;

namespace Logistic.Domain.DTO.Logistic.LandLot
{
    [ExcludeFromCodeCoverage]
    public class UpdateLandLotDto : AddLandLotDto
    {
        public int IdLandLot { get; set; }
    }
}
