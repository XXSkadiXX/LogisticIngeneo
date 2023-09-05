using System.Diagnostics.CodeAnalysis;

namespace Logistic.Domain.DTO.Seaport
{
    [ExcludeFromCodeCoverage]
    public class SeaportDto : UpdateSeaportDto
    {
        public string Country { get; set; }
    }
}
