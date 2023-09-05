using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Models.Logistic;
using Logistic.Common.Exceptions;
using Logistic.Common.Resources;
using Logistic.Domain.DTO.Seaport;
using Logistic.Domain.Services.Logistic.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Logistic.Domain.Services.Logistic
{
    public class SeaportServices : ISeaportServices
    {
        #region Attribute
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Builder
        public SeaportServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        private SeaportEntity GetSeaport(int idSeaport) => _unitOfWork.SeaportRepository.FirstOrDefault(x => x.Id == idSeaport);

        public List<SeaportDto> GetAllSeaports()
        {
            IEnumerable<SeaportEntity> listSeaports = _unitOfWork.SeaportRepository
              .GetAll(p => p.CountryEntity);

            List<SeaportDto> resultSeaports = listSeaports.Select(x => new SeaportDto()
            {
                Id = x.Id,
                Name = x.Seaport,
                IdCountry = x.IdCountry,
                Country = x.CountryEntity.Country

            }).ToList();

            return resultSeaports;
        }

        public List<SeaportDto> GetAllSeaportsByCountry(int countryId)
        {
            IEnumerable<SeaportEntity> listSeaports = _unitOfWork.SeaportRepository
              .FindAll(p => p.IdCountry == countryId,
                p => p.CountryEntity);

            List<SeaportDto> resultSeaports = listSeaports.Select(x => new SeaportDto()
            {
                Id = x.Id,
                Name = x.Seaport,
                IdCountry = x.IdCountry,
                Country = x.CountryEntity.Country

            }).ToList();

            return resultSeaports;
        }

        public async Task<bool> InsertSeaport(AddSeaportDto addSeaport)
        {
            if (ExistedSeaport(addSeaport.Name))
                throw new BusinessException(GeneralMessages.ExistedRegister);

            SeaportEntity newSeaport = new SeaportEntity()
            {
                Seaport = addSeaport.Name,
                IdCountry = addSeaport.IdCountry,
            };

            _unitOfWork.SeaportRepository.Insert(newSeaport);

            return await _unitOfWork.Save() > 0;
        }

        public async Task<bool> UpdateSeaport(UpdateSeaportDto uSeaport)
        {
            SeaportEntity updateSeaport = GetSeaport(uSeaport.Id);
            if (updateSeaport == null)
                throw new BusinessException(GeneralMessages.ItemNoFound);

            updateSeaport.Seaport = uSeaport.Name;
            updateSeaport.IdCountry = uSeaport.IdCountry;

            _unitOfWork.SeaportRepository.Update(updateSeaport);

            return await _unitOfWork.Save() > 0;
        }

        public async Task<bool> DeleteSeaport(int idSeaport)
        {
            SeaportEntity seaport = GetSeaport(idSeaport);
            if (seaport == null)
                throw new BusinessException(GeneralMessages.ItemNoFound);

            _unitOfWork.SeaportRepository.Delete(seaport);

            return await _unitOfWork.Save() > 0;
        }

        private bool ExistedSeaport(string seaport)
        {
            bool result = false;
            if (_unitOfWork.SeaportRepository.FirstOrDefault(x => x.Seaport.ToLower() == seaport.ToLower()) != null)
                result = true;

            return result;

        }

        #endregion
    }
}
