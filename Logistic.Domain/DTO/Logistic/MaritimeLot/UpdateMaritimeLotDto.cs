using System.Diagnostics.CodeAnalysis;

namespace Logistic.Domain.DTO.Logistic.MaritimeLot
{
    [ExcludeFromCodeCoverage]
    public class UpdateMaritimeLotDto : AddMaritimeLotDto
    {
        public int Id { get; set; }
    }
}
