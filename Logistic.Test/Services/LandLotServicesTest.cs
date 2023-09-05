using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Models.Logistic;
using Logistic.Common.Exceptions;
using Logistic.Common.Resources;
using Logistic.Domain.DTO.Logistic.LandLot;
using Logistic.Domain.Services.Logistic;
using Logistic.Domain.Services.Logistic.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Logistic.Test.Services
{
    public class LandLotServicesTest
    {
        #region Attributes
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly ILandLotServices _landLotServices;
        #endregion

        #region Builder
        public LandLotServicesTest()
        {
            //AppSetting
            IConfigurationRoot configuration = new ConfigurationBuilder()
                 .AddInMemoryCollection(ConfigurationMock.ConfigAppSetting())
                 .Build();
            _unitOfWorkMock = new Mock<IUnitOfWork>();




            _landLotServices = new LandLotServices(_unitOfWorkMock.Object, configuration);
        }
        #endregion

        private void PrepareData()
        {
            _unitOfWorkMock.Setup(x => x.LandLotRepository.FirstOrDefault(m => m.Id == 123))
                                                             .Returns(new LandLotEntity()
                                                             {
                                                                 Id = 123,
                                                                 Amount = 5,
                                                                 Price = 1000,
                                                                 DeliveryDate = DateTime.Now,
                                                                 RegisterDate = DateTime.Now,
                                                                 VehiclePlate = "TRK502",
                                                                 GuideNumber = "KKMOV2U1Z1",
                                                                 IdClient = 1,
                                                                 IdWarehouse = 1,
                                                                 IdTypeProduct = 1,
                                                                 ClientEntity = new ClientEntity()
                                                                 {
                                                                     Id = 1,
                                                                     Name = "Test",
                                                                     LastName = "MockLastName",
                                                                 },
                                                                 WarehouseEntity = new WarehouseEntity()
                                                                 {
                                                                     Id = 1,
                                                                     Warehouse = "MockWarehouse"
                                                                 },
                                                                 TypeProductEntity = new TypeProductEntity()
                                                                 {
                                                                     Id = 1,
                                                                     TypeProduct = "MockTypeProduct"
                                                                 }
                                                             });
        }

        #region Test

        [Fact]
        public void GetAll_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.LandLotRepository.GetAll(c => c.ClientEntity,
                                                                      s => s.WarehouseEntity,
                                                                      t => t.TypeProductEntity))
                                                              .Returns(new List<LandLotEntity>()
                                                              {
                                                                  new LandLotEntity()
                                                                  {
                                                                      Id = 123,
                                                                      Amount = 5,
                                                                      Price = 1000,
                                                                      DeliveryDate = DateTime.Now,
                                                                      RegisterDate = DateTime.Now,
                                                                      VehiclePlate = "TRK502",
                                                                      GuideNumber = "KKMOV2U1Z1",
                                                                      IdClient = 1,
                                                                      IdWarehouse = 1,
                                                                      IdTypeProduct = 1,
                                                                      ClientEntity = new ClientEntity()
                                                                      {
                                                                          Id =1,
                                                                          Name= "Test",
                                                                          LastName="MockLastName",
                                                                      },
                                                                      WarehouseEntity = new WarehouseEntity()
                                                                      {
                                                                          Id =1,
                                                                          Warehouse="MockWarehouse"
                                                                      },
                                                                      TypeProductEntity = new TypeProductEntity()
                                                                      {
                                                                          Id=1,
                                                                          TypeProduct="MockTypeProduct"
                                                                      }
                                                                  },
                                                                  new LandLotEntity()
                                                                  {
                                                                      Id = 852,
                                                                      Amount = 5,
                                                                      Price = 1000,
                                                                      DeliveryDate = DateTime.Now,
                                                                      RegisterDate = DateTime.Now,
                                                                      VehiclePlate = "TRK502",
                                                                      GuideNumber = "KKMOV2U1Z1",
                                                                      IdClient = 1,
                                                                      IdWarehouse = 1,
                                                                      IdTypeProduct = 1,
                                                                      ClientEntity = new ClientEntity()
                                                                      {
                                                                          Id =1,
                                                                          Name= "Test",
                                                                          LastName="MockLastName",
                                                                      },
                                                                      WarehouseEntity = new WarehouseEntity()
                                                                      {
                                                                          Id =1,
                                                                          Warehouse="MockWarehouse"
                                                                      },
                                                                      TypeProductEntity = new TypeProductEntity()
                                                                      {
                                                                          Id=1,
                                                                          TypeProduct="MockTypeProduct"
                                                                      }
                                                                  },
                                                              });

            //act
            List<LandLotDto> result = _landLotServices.GetAll();

            //assert
            var model = Assert.IsAssignableFrom<List<LandLotDto>>(result);
            Assert.True(model.Any());
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public void GetByGuideNumber_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.LandLotRepository.FirstOrDefault(m => m.GuideNumber.ToLower() == "KKMOV2U1Z1".ToLower(),
                                                                              c => c.ClientEntity,
                                                                              s => s.WarehouseEntity,
                                                                              t => t.TypeProductEntity))
                                                              .Returns(new LandLotEntity()
                                                              {
                                                                  Id = 123,
                                                                  Amount = 5,
                                                                  Price = 1000,
                                                                  DeliveryDate = DateTime.Now,
                                                                  RegisterDate = DateTime.Now,
                                                                  VehiclePlate = "TRK502",
                                                                  GuideNumber = "KKMOV2U1Z1",
                                                                  IdClient = 1,
                                                                  IdWarehouse = 1,
                                                                  IdTypeProduct = 1,
                                                                  ClientEntity = new ClientEntity()
                                                                  {
                                                                      Id = 1,
                                                                      Name = "Test",
                                                                      LastName = "MockLastName",
                                                                  },
                                                                  WarehouseEntity = new WarehouseEntity()
                                                                  {
                                                                      Id = 1,
                                                                      Warehouse = "MockWarehouse"
                                                                  },
                                                                  TypeProductEntity = new TypeProductEntity()
                                                                  {
                                                                      Id = 1,
                                                                      TypeProduct = "MockTypeProduct"
                                                                  }
                                                              });

            //act
            LandLotDto result = _landLotServices.GetByGuideNumber("KKMOV2U1Z1");

            //assert
            var model = Assert.IsAssignableFrom<LandLotDto>(result);
            Assert.True(model != null);
        }

        [Fact]
        public void GetByGuideNumber_Null_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.LandLotRepository.FirstOrDefault(m => m.GuideNumber.ToLower() == "KKMOV2U1Z1".ToLower(),
                                                                              c => c.ClientEntity,
                                                                              s => s.WarehouseEntity,
                                                                              t => t.TypeProductEntity))
                                                              .Returns(new LandLotEntity()
                                                              {
                                                                  Id = 123,
                                                                  Amount = 5,
                                                                  Price = 1000,
                                                                  DeliveryDate = DateTime.Now,
                                                                  RegisterDate = DateTime.Now,
                                                                  VehiclePlate = "TRK502",
                                                                  GuideNumber = "KKMOV2U1Z1",
                                                                  IdClient = 1,
                                                                  IdWarehouse = 1,
                                                                  IdTypeProduct = 1,
                                                                  ClientEntity = new ClientEntity()
                                                                  {
                                                                      Id = 1,
                                                                      Name = "Test",
                                                                      LastName = "MockLastName",
                                                                  },
                                                                  WarehouseEntity = new WarehouseEntity()
                                                                  {
                                                                      Id = 1,
                                                                      Warehouse = "MockWarehouse"
                                                                  },
                                                                  TypeProductEntity = new TypeProductEntity()
                                                                  {
                                                                      Id = 1,
                                                                      TypeProduct = "MockTypeProduct"
                                                                  }
                                                              });

            //act and assert
            var exception = Assert.Throws<BusinessException>(() => _landLotServices.GetByGuideNumber(It.IsAny<string>()));
            Assert.Equal(GeneralMessages.ItemNoFound, exception.Message);
        }

        [Fact]
        public async Task Insert_Error_Amount_Test()
        {
            //arrange
            AddLandLotDto add = new AddLandLotDto()
            {
                Amount = 0,
                Price = 1000,
                DeliveryDate = DateTime.Now,
                VehiclePlate = "TRK502",
                IdClient = 1,
                IdWarehouse = 1,
                IdTypeProduct = 1,
            };

            //act and assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _landLotServices.Insert(add));
            Assert.Equal(GeneralMessages.IncorrectAmount, exception.Message);
        }

        [Fact]
        public async Task Insert_Error_Price_Test()
        {
            //arrange
            AddLandLotDto add = new AddLandLotDto()
            {
                Amount = 10,
                Price = 0,
                DeliveryDate = DateTime.Now,
                VehiclePlate = "TRK502",
                IdClient = 1,
                IdWarehouse = 1,
                IdTypeProduct = 1,
            };

            //act and assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _landLotServices.Insert(add));
            Assert.Equal(GeneralMessages.IncorrectPrice, exception.Message);
        }

        [Fact]
        public async Task Insert_ValidateFleetNumber_Test()
        {
            //arrange
            AddLandLotDto add = new AddLandLotDto()
            {
                Amount = 10,
                Price = 1000,
                DeliveryDate = DateTime.Now,
                VehiclePlate = "T502RK",
                IdClient = 1,
                IdWarehouse = 1,
                IdTypeProduct = 1,
            };

            //act and assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _landLotServices.Insert(add));
            Assert.Equal(GeneralMessages.IncorrectPlate, exception.Message);
        }

        [Fact]
        public async Task Insert_GetDiscount_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.LandLotRepository.Insert(It.IsAny<LandLotEntity>()));
            _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);

            AddLandLotDto add = new AddLandLotDto()
            {
                Amount = 11,
                Price = 10,
                DeliveryDate = DateTime.Now,
                VehiclePlate = "TRK502",
                IdClient = 1,
                IdWarehouse = 1,
                IdTypeProduct = 1,
            };

            //act 
            bool result = await _landLotServices.Insert(add);

            //assert
            Assert.True(result);
        }

        [Fact]
        public async Task Insert_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.LandLotRepository.Insert(It.IsAny<LandLotEntity>()));
            _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);

            AddLandLotDto add = new AddLandLotDto()
            {
                Amount = 10,
                Price = 10,
                DeliveryDate = DateTime.Now,
                VehiclePlate = "TRK502",
                IdClient = 1,
                IdWarehouse = 1,
                IdTypeProduct = 1,
            };

            //act 
            bool result = await _landLotServices.Insert(add);

            //assert
            Assert.True(result);
        }


        [Fact]
        public async Task Update_Error_NotFound_Test()
        {
            //arrange
            PrepareData();

            UpdateLandLotDto update = new UpdateLandLotDto()
            {
                IdLandLot = 1234,
                Amount = 0,
                Price = 1000,
                DeliveryDate = DateTime.Now,
                VehiclePlate = "TRK502",
                IdClient = 1,
                IdWarehouse = 1,
                IdTypeProduct = 1,
            };

            //act and assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _landLotServices.Update(update));
            Assert.Equal(GeneralMessages.ItemNoFound, exception.Message);
        }

        [Fact]
        public async Task Update_Error_Amount_Test()
        {
            //arrange
            PrepareData();

            UpdateLandLotDto update = new UpdateLandLotDto()
            {
                IdLandLot = 123,
                Amount = 0,
                Price = 1000,
                DeliveryDate = DateTime.Now,
                VehiclePlate = "TRK502",
                IdClient = 1,
                IdWarehouse = 1,
                IdTypeProduct = 1,
            };

            //act and assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _landLotServices.Update(update));
            Assert.Equal(GeneralMessages.IncorrectAmount, exception.Message);
        }

        [Fact]
        public async Task Update_Error_Price_Test()
        {
            //arrange
            PrepareData();

            UpdateLandLotDto update = new UpdateLandLotDto()
            {
                IdLandLot = 123,
                Amount = 10,
                Price = 0,
                DeliveryDate = DateTime.Now,
                VehiclePlate = "TRK502",
                IdClient = 1,
                IdWarehouse = 1,
                IdTypeProduct = 1,
            };

            //act and assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _landLotServices.Update(update));
            Assert.Equal(GeneralMessages.IncorrectPrice, exception.Message);
        }

        [Fact]
        public async Task Update_Error_ValidateVehiclePlate_Test()
        {
            //arrange
            PrepareData();

            UpdateLandLotDto update = new UpdateLandLotDto()
            {
                IdLandLot = 123,
                Amount = 10,
                Price = 10,
                DeliveryDate = DateTime.Now,
                VehiclePlate = "TK502R",
                IdClient = 1,
                IdWarehouse = 1,
                IdTypeProduct = 1,
            };

            //act and assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _landLotServices.Update(update));
            Assert.Equal(GeneralMessages.IncorrectPlate, exception.Message);
        }

        [Fact]
        public async Task Update_GetDiscount_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.LandLotRepository.Update(It.IsAny<LandLotEntity>()));
            _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);

            PrepareData();
            UpdateLandLotDto update = new UpdateLandLotDto()
            {
                IdLandLot = 123,
                Amount = 11,
                Price = 10,
                DeliveryDate = DateTime.Now,
                VehiclePlate = "TKR502",
                IdClient = 1,
                IdWarehouse = 1,
                IdTypeProduct = 1,
            };

            //act 
            bool result = await _landLotServices.Update(update);

            //assert
            Assert.True(result);
        }

        [Fact]
        public async Task Update_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.LandLotRepository.Update(It.IsAny<LandLotEntity>()));
            _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);

            PrepareData();
            UpdateLandLotDto update = new UpdateLandLotDto()
            {
                IdLandLot = 123,
                Amount = 5,
                Price = 10,
                DeliveryDate = DateTime.Now,
                VehiclePlate = "TKR502",
                IdClient = 1,
                IdWarehouse = 1,
                IdTypeProduct = 1,
            };

            //act 
            bool result = await _landLotServices.Update(update);

            //assert
            Assert.True(result);
        }


        [Fact]
        public async Task Delete_NotFound_Test()
        {
            //arrange
            PrepareData();

            //act and assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _landLotServices.Delete(It.IsAny<int>()));
            Assert.Equal(GeneralMessages.ItemNoFound, exception.Message);
        }

        [Fact]
        public async Task Delete_Test()
        {
            //arrange
            PrepareData();

            _unitOfWorkMock.Setup(x => x.LandLotRepository.Delete(It.IsAny<LandLotEntity>()));
            _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);

            //act and assert
            bool result = await _landLotServices.Delete(123);
            Assert.True(result);
        }
        #endregion
    }
}
