using System.Diagnostics.CodeAnalysis;

namespace Logistic.Domain.DTO.Logistic.LandLot
{
    [ExcludeFromCodeCoverage]
    public class LandLotDto : UpdateLandLotDto
    {
        public DateTime RegisterDate { get; set; }

        public decimal? DiscountPrice { get; set; }

        public string GuideNumber { get; set; } = null!;

        public string Warehouse { get; set; } = null!;
        public string TypeProduct { get; set; } = null!;
        public string Client { get; set; } = null!;
    }
}
