using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Models.Logistic;
using Logistic.Common.Exceptions;
using Logistic.Common.Resources;
using Logistic.Domain.DTO.Logistic.Client;
using Logistic.Domain.Services.Logistic.Interfaces;

namespace Logistic.Domain.Services.Logistic
{
    public class ClientServices : IClientServices
    {
        #region Attribute
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Builder
        public ClientServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        private ClientEntity GetClient(int idClient) => _unitOfWork.ClientRepository.FirstOrDefault(x => x.Id == idClient);

        public List<ClientDto> GetAllClients()
        {
            IEnumerable<ClientEntity> listClients = _unitOfWork.ClientRepository
              .GetAll(p => p.CountryEntity);

            List<ClientDto> resultClients = listClients.Select(x => new ClientDto()
            {
                Id = x.Id,
                Name = x.Name,
                LastName = x.LastName,
                Direction = x.Direction,
                Email = x.Email,
                Phone = x.Phone,
                IdCountry = x.IdCountry,
                Country = x.CountryEntity.Country

            }).ToList();

            return resultClients;
        }

        public async Task<bool> InsertClient(AddClientDto addClient)
        {
            ClientEntity newClient = new ClientEntity()
            {
                Name = addClient.Name,
                LastName = addClient.LastName,
                Direction = addClient.Direction,
                Email = addClient.Email,
                Phone = addClient.Phone,
                IdCountry = addClient.IdCountry,
            };

            _unitOfWork.ClientRepository.Insert(newClient);

            return await _unitOfWork.Save() > 0;
        }

        public async Task<bool> UpdateClient(UpdateClientDto uClient)
        {
            ClientEntity updateClient = GetClient(uClient.Id);
            if (updateClient == null)
                throw new BusinessException(GeneralMessages.ItemNoFound);

            updateClient.Name = uClient.Name;
            updateClient.LastName = uClient.LastName;
            updateClient.Direction = uClient.Direction;
            updateClient.Email = uClient.Email;
            updateClient.Phone = uClient.Phone;
            updateClient.IdCountry = uClient.IdCountry;

            _unitOfWork.ClientRepository.Update(updateClient);

            return await _unitOfWork.Save() > 0;
        }

        public async Task<bool> DeleteClient(int idClient)
        {
            ClientEntity client = GetClient(idClient);
            if (client == null)
                throw new BusinessException(GeneralMessages.ItemNoFound);

            _unitOfWork.ClientRepository.Delete(client);

            return await _unitOfWork.Save() > 0;
        }


        #endregion
    }
}
