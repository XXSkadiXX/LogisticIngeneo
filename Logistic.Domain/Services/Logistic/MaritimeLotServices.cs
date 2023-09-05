using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Models.Logistic;
using Logistic.Common.Exceptions;
using Logistic.Common.Helpers;
using Logistic.Common.Resources;
using Logistic.Domain.DTO.Logistic.MaritimeLot;
using Logistic.Domain.Services.Logistic.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace Logistic.Domain.Services.Logistic
{
    public class MaritimeLotServices : IMaritimeLotServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        #endregion

        #region Builder
        public MaritimeLotServices(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        #endregion

        #region Methods

        public List<MaritimeLotDto> GetAll()
        {
            IEnumerable<MaritimeLotEntity> list = _unitOfWork.MaritimeLotRepository.GetAll(x => x.ClientEntity,
                                                                                            s => s.SeaportEntity,
                                                                                            t => t.TypeProductEntity);
            List<MaritimeLotDto> result = list.Select(x => new MaritimeLotDto()
            {
                Id = x.Id,
                Amount = x.Amount,
                Price = x.Price,
                DiscountPrice = x.DiscountPrice,
                DeliveryDate = x.DeliveryDate,
                RegisterDate = x.RegisterDate,
                FleetNumber = x.FleetNumber,
                GuideNumber = x.GuideNumber,
                IdClient = x.IdClient,
                Client = x.ClientEntity.FullName,
                IdSeaport = x.IdSeaport,
                Seaport = x.SeaportEntity.Seaport,
                IdTypeProduct = x.IdTypeProduct,
                TypeProduct = x.TypeProductEntity.TypeProduct

            }).ToList();

            return result;
        }

        public MaritimeLotDto GetByGuideNumber(string guideNumber)
        {
            MaritimeLotEntity entity = _unitOfWork.MaritimeLotRepository.FirstOrDefault(x => x.GuideNumber.ToLower() == guideNumber.ToLower(),
                                                                                        c => c.ClientEntity,
                                                                                        s => s.SeaportEntity,
                                                                                        t => t.TypeProductEntity);
            if (entity == null)
                throw new BusinessException(GeneralMessages.ItemNoFound);

            MaritimeLotDto maritimeLot = new MaritimeLotDto()
            {
                Id = entity.Id,
                Amount = entity.Amount,
                Price = entity.Price,
                DiscountPrice = entity.DiscountPrice,
                DeliveryDate = entity.DeliveryDate,
                RegisterDate = entity.RegisterDate,
                FleetNumber = entity.FleetNumber,
                GuideNumber = entity.GuideNumber,
                IdClient = entity.IdClient,
                Client = entity.ClientEntity.FullName,
                IdSeaport = entity.IdSeaport,
                Seaport = entity.SeaportEntity.Seaport,
                IdTypeProduct = entity.IdTypeProduct,
                TypeProduct = entity.TypeProductEntity.TypeProduct
            };

            return maritimeLot;
        }

        public async Task<bool> Insert(AddMaritimeLotDto lot)
        {
            if (lot.Amount <= 0)
                throw new BusinessException(GeneralMessages.IncorrectAmount);

            if (lot.Price <= 0)
                throw new BusinessException(GeneralMessages.IncorrectPrice);

            if (!ValidateFleetNumber(lot.FleetNumber))
                throw new BusinessException(GeneralMessages.IncorrectFleetFormat);

            int percentaje = Convert.ToInt32(_configuration["Settings:MaritimeLogisticDiscount"]);
            int discountAmount = Convert.ToInt32(_configuration["Settings:DiscountAmount"]);
            decimal discount = 0;
            if (lot.Amount > discountAmount)
                discount = Helper.GetDiscount(lot.Price, percentaje);

            MaritimeLotEntity maritimeLot = new MaritimeLotEntity()
            {
                Amount = lot.Amount,
                FleetNumber = lot.FleetNumber,
                GuideNumber = Helper.GenerateAlphanumericCode(10),
                IdClient = lot.IdClient,
                IdSeaport = lot.IdSeaport,
                IdTypeProduct = lot.IdTypeProduct,
                RegisterDate = DateTime.Now,
                DeliveryDate = lot.DeliveryDate,
                Price = lot.Price,
                DiscountPrice = DiscountPrice(lot.Price, discount)
            };
            _unitOfWork.MaritimeLotRepository.Insert(maritimeLot);

            return await _unitOfWork.Save() > 0;
        }

        public async Task<bool> Update(UpdateMaritimeLotDto lot)
        {
            MaritimeLotEntity maritimeLot = Get(lot.Id);
            if (maritimeLot == null)
                throw new BusinessException(GeneralMessages.ItemNoFound);

            if (lot.Amount <= 0)
                throw new BusinessException(GeneralMessages.IncorrectAmount);

            if (lot.Price <= 0)
                throw new BusinessException(GeneralMessages.IncorrectPrice);

            if (!ValidateFleetNumber(lot.FleetNumber))
                throw new BusinessException(GeneralMessages.IncorrectFleetFormat);

            int percentaje = Convert.ToInt32(_configuration["Settings:MaritimeLogisticDiscount"]);
            int discountAmount = Convert.ToInt32(_configuration["Settings:DiscountAmount"]);
            decimal discount = 0;
            if (lot.Amount >= discountAmount)
                discount = Helper.GetDiscount(lot.Price, percentaje);

            maritimeLot.Amount = lot.Amount;
            maritimeLot.FleetNumber = lot.FleetNumber;
            maritimeLot.IdClient = lot.IdClient;
            maritimeLot.IdSeaport = lot.IdSeaport;
            maritimeLot.IdTypeProduct = lot.IdTypeProduct;
            maritimeLot.DeliveryDate = lot.DeliveryDate;
            maritimeLot.Price = lot.Price;
            maritimeLot.DiscountPrice = DiscountPrice(lot.Price, discount);
            _unitOfWork.MaritimeLotRepository.Update(maritimeLot);

            return await _unitOfWork.Save() > 0;
        }

        public async Task<bool> Delete(int idMaritimeLot)
        {
            MaritimeLotEntity maritimeLot = Get(idMaritimeLot);
            if (maritimeLot == null)
                throw new BusinessException(GeneralMessages.ItemNoFound);

            _unitOfWork.MaritimeLotRepository.Delete(maritimeLot);

            return await _unitOfWork.Save() > 0;
        }

        #region Privates
        private MaritimeLotEntity Get(int id) => _unitOfWork.MaritimeLotRepository.FirstOrDefault(x => x.Id == id);
        private decimal? DiscountPrice(decimal price, decimal discount)
        {
            return discount > 0 ? (price - discount) : null;
        }

        private bool ValidateFleetNumber(string fleetNumber)
        {
            // Define the regular expression to validate the format.
            string patron = @"^[A-Za-z]{3}\d{4}[A-Za-z]{1}$";

            // Create a Regex object and perform validation.
            Regex regex = new Regex(patron);

            return regex.IsMatch(fleetNumber);
        }

        #endregion

        #endregion

    }
}
