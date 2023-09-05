using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Models.Logistic;
using Logistic.Common.Exceptions;
using Logistic.Common.Helpers;
using Logistic.Common.Resources;
using Logistic.Domain.DTO.Logistic.LandLot;
using Logistic.Domain.Services.Logistic.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace Logistic.Domain.Services.Logistic
{
    public class LandLotServices : ILandLotServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        #endregion

        #region Builder
        public LandLotServices(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        #endregion

        #region Methods
        public List<LandLotDto> GetAll()
        {
            IEnumerable<LandLotEntity> list = _unitOfWork.LandLotRepository.GetAll(x => x.ClientEntity,
                                                                                   s => s.WarehouseEntity,
                                                                                   t => t.TypeProductEntity);
            List<LandLotDto> result = list.Select(x => new LandLotDto()
            {
                IdLandLot = x.Id,
                Amount = x.Amount,
                Price = x.Price,
                DiscountPrice = x.DiscountPrice,
                DeliveryDate = x.DeliveryDate,
                RegisterDate = x.RegisterDate,
                VehiclePlate = x.VehiclePlate,
                GuideNumber = x.GuideNumber,
                IdClient = x.IdClient,
                Client = x.ClientEntity.FullName,
                IdWarehouse = x.IdWarehouse,
                Warehouse = x.WarehouseEntity.Warehouse,
                IdTypeProduct = x.IdTypeProduct,
                TypeProduct = x.TypeProductEntity.TypeProduct
            }).ToList();

            return result;
        }

        public LandLotDto GetByGuideNumber(string guideNumber)
        {
            LandLotEntity entity = _unitOfWork.LandLotRepository.FirstOrDefault(x => x.GuideNumber.ToLower() == guideNumber.ToLower(),
                                                                                c => c.ClientEntity,
                                                                                w => w.WarehouseEntity,
                                                                                t => t.TypeProductEntity);
            if (entity == null)
                throw new BusinessException(GeneralMessages.ItemNoFound);

            LandLotDto landLot = new LandLotDto()
            {
                IdLandLot = entity.Id,
                Amount = entity.Amount,
                Price = entity.Price,
                DiscountPrice = entity.DiscountPrice,
                DeliveryDate = entity.DeliveryDate,
                RegisterDate = entity.RegisterDate,
                VehiclePlate = entity.VehiclePlate,
                GuideNumber = entity.GuideNumber,
                IdClient = entity.IdClient,
                Client = entity.ClientEntity.FullName,
                IdWarehouse = entity.IdWarehouse,
                Warehouse = entity.WarehouseEntity.Warehouse,
                IdTypeProduct = entity.IdTypeProduct,
                TypeProduct = entity.TypeProductEntity.TypeProduct
            };

            return landLot;
        }

        public async Task<bool> Insert(AddLandLotDto lot)
        {
            if (lot.Amount <= 0)
                throw new BusinessException(GeneralMessages.IncorrectAmount);

            if (lot.Price <= 0)
                throw new BusinessException(GeneralMessages.IncorrectPrice);

            if (!ValidateVehiclePlate(lot.VehiclePlate))
                throw new BusinessException(GeneralMessages.IncorrectPlate);

            int percentaje = Convert.ToInt32(_configuration["Settings:TerrestrialLogisticsDiscount"]);
            int discountAmount = Convert.ToInt32(_configuration["Settings:DiscountAmount"]);

            decimal discount = 0;
            if (lot.Amount >= discountAmount)
                discount = Helper.GetDiscount(lot.Price, percentaje);

            LandLotEntity landLot = new LandLotEntity()
            {
                Amount = lot.Amount,
                VehiclePlate = lot.VehiclePlate,
                GuideNumber = Helper.GenerateAlphanumericCode(10),
                IdClient = lot.IdClient,
                IdWarehouse = lot.IdWarehouse,
                IdTypeProduct = lot.IdTypeProduct,
                RegisterDate = DateTime.Now,
                DeliveryDate = lot.DeliveryDate,
                Price = lot.Price,
                DiscountPrice = DiscountPrice(lot.Price, discount)
            };
            _unitOfWork.LandLotRepository.Insert(landLot);

            return await _unitOfWork.Save() > 0;
        }

        public async Task<bool> Update(UpdateLandLotDto lot)
        {
            LandLotEntity landLot = Get(lot.IdLandLot);
            if (landLot == null)
                throw new BusinessException(GeneralMessages.ItemNoFound);

            if (lot.Amount <= 0)
                throw new BusinessException(GeneralMessages.IncorrectAmount);

            if (lot.Price <= 0)
                throw new BusinessException(GeneralMessages.IncorrectPrice);

            if (!ValidateVehiclePlate(lot.VehiclePlate))
                throw new BusinessException(GeneralMessages.IncorrectPlate);

            int percentaje = Convert.ToInt32(_configuration["Settings:TerrestrialLogisticsDiscount"]);
            int discountAmount = Convert.ToInt32(_configuration["Settings:DiscountAmount"]);

            decimal discount = 0;
            if (lot.Amount >= discountAmount)
                discount = Helper.GetDiscount(lot.Price, percentaje);

            landLot.Amount = lot.Amount;
            landLot.VehiclePlate = lot.VehiclePlate;
            landLot.IdClient = lot.IdClient;
            landLot.IdWarehouse = lot.IdWarehouse;
            landLot.IdTypeProduct = lot.IdTypeProduct;
            landLot.DeliveryDate = lot.DeliveryDate;
            landLot.Price = lot.Price;
            landLot.DiscountPrice = DiscountPrice(lot.Price, discount);
            _unitOfWork.LandLotRepository.Update(landLot);

            return await _unitOfWork.Save() > 0;
        }

        public async Task<bool> Delete(int idLandLot)
        {
            LandLotEntity landLot = Get(idLandLot);
            if (landLot == null)
                throw new BusinessException(GeneralMessages.ItemNoFound);

            _unitOfWork.LandLotRepository.Delete(landLot);

            return await _unitOfWork.Save() > 0;
        }

        #region Privates
        private LandLotEntity Get(int id) => _unitOfWork.LandLotRepository.FirstOrDefault(x => x.Id == id);
        private decimal? DiscountPrice(decimal price, decimal discount)
        {
            return discount > 0 ? (price - discount) : null;
        }

        private bool ValidateVehiclePlate(string vehiclePlate)
        {
            // Define the regular expression to validate the format.
            string patron = @"^[A-Za-z]{3}\d{3}$";

            // Create a Regex object and perform validation.
            Regex regex = new Regex(patron);

            return regex.IsMatch(vehiclePlate);
        }


        #endregion
        #endregion
    }
}
