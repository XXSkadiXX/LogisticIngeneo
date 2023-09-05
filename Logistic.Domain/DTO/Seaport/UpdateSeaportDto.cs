using System.Diagnostics.CodeAnalysis;

namespace Logistic.Domain.DTO.Seaport
{
    [ExcludeFromCodeCoverage]
    public class UpdateSeaportDto : AddSeaportDto
    {
        public int Id { get; set; }
    }
}
