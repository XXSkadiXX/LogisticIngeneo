using Logistic.Domain.DTO.Logistic.Client;

namespace Logistic.Domain.Services.Logistic.Interfaces
{
    public interface IClientServices
    {
        List<ClientDto> GetAllClients();
        Task<bool> InsertClient(AddClientDto addClient);
        Task<bool> UpdateClient(UpdateClientDto uClient);
        Task<bool> DeleteClient(int idClient);
    }
}
