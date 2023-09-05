using System.Diagnostics.CodeAnalysis;

namespace Logistic.Domain.DTO.Logistic.Client
{
    [ExcludeFromCodeCoverage]
    public class UpdateClientDto: AddClientDto
    {
        public int Id { get; set; }
    }
}
