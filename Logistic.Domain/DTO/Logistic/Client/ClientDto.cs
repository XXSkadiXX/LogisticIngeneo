using System.Diagnostics.CodeAnalysis;

namespace Logistic.Domain.DTO.Logistic.Client
{
    [ExcludeFromCodeCoverage]
    public class ClientDto : UpdateClientDto
    {
        public string Country { get; set; }
    }
}
