using System.Diagnostics.CodeAnalysis;

namespace Logistic.Domain.DTO.Logistic.MaritimeLot
{
    [ExcludeFromCodeCoverage]
    public class MaritimeLotDto : UpdateMaritimeLotDto
    {
        public DateTime RegisterDate { get; set; }

        public decimal? DiscountPrice { get; set; }

        public string GuideNumber { get; set; } = null!;

        public string Seaport { get; set; } = null!;
        public string TypeProduct { get; set; } = null!;
        public string Client { get; set; } = null!;
    }
}
