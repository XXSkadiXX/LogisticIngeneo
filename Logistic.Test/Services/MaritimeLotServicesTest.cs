using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Models.Logistic;
using Logistic.Common.Exceptions;
using Logistic.Common.Resources;
using Logistic.Domain.DTO.Logistic.MaritimeLot;
using Logistic.Domain.Services.Logistic;
using Logistic.Domain.Services.Logistic.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Logistic.Test.Services
{
    public class MaritimeLotServicesTest
    {
        #region Attributes
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IMaritimeLotServices _maritimeLotServices;
        #endregion

        #region Builder
        public MaritimeLotServicesTest()
        {
            //AppSetting
            IConfigurationRoot configuration = new ConfigurationBuilder()
                 .AddInMemoryCollection(ConfigurationMock.ConfigAppSetting())
                 .Build();
            _unitOfWorkMock = new Mock<IUnitOfWork>();


            _maritimeLotServices = new MaritimeLotServices(_unitOfWorkMock.Object, configuration);
        }
        #endregion

        private void PrepareData()
        {
            _unitOfWorkMock.Setup(x => x.MaritimeLotRepository.FirstOrDefault(m => m.Id == 123))
                                                            .Returns(new MaritimeLotEntity()
                                                            {
                                                                Id = 123,
                                                                Amount = 5,
                                                                Price = 1000,
                                                                DeliveryDate = DateTime.Now,
                                                                RegisterDate = DateTime.Now,
                                                                FleetNumber = "PER7856J",
                                                                GuideNumber = "KKMOV2U1Z1",
                                                                IdClient = 1,
                                                                IdSeaport = 1,
                                                                IdTypeProduct = 1,
                                                                ClientEntity = new ClientEntity()
                                                                {
                                                                    Id = 1,
                                                                    Name = "Test",
                                                                    LastName = "MockLastName",
                                                                },
                                                                SeaportEntity = new SeaportEntity()
                                                                {
                                                                    Id = 1,
                                                                    Seaport = "MockSeaport"
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
            _unitOfWorkMock.Setup(x => x.MaritimeLotRepository.GetAll(c => c.ClientEntity,
                                                                      s => s.SeaportEntity,
                                                                      t => t.TypeProductEntity))
                                                              .Returns(new List<MaritimeLotEntity>()
                                                              {
                                                                  new MaritimeLotEntity()
                                                                  {
                                                                      Id = 123,
                                                                      Amount = 5,
                                                                      Price = 1000,
                                                                      DeliveryDate = DateTime.Now,
                                                                      RegisterDate = DateTime.Now,
                                                                      FleetNumber = "PER7856J",
                                                                      GuideNumber = "KKMOV2U1Z1",
                                                                      IdClient = 1,
                                                                      IdSeaport = 1,
                                                                      IdTypeProduct = 1,
                                                                      ClientEntity = new ClientEntity()
                                                                      {
                                                                          Id =1,
                                                                          Name= "Test",
                                                                          LastName="MockLastName",
                                                                      },
                                                                      SeaportEntity = new SeaportEntity()
                                                                      {
                                                                          Id =1,
                                                                          Seaport="MockSeaport"
                                                                      },
                                                                      TypeProductEntity = new TypeProductEntity()
                                                                      {
                                                                          Id=1,
                                                                          TypeProduct="MockTypeProduct"
                                                                      }
                                                                  },
                                                                  new MaritimeLotEntity()
                                                                  {
                                                                      Id = 852,
                                                                      Amount = 5,
                                                                      Price = 1000,
                                                                      DeliveryDate = DateTime.Now,
                                                                      RegisterDate = DateTime.Now,
                                                                      FleetNumber = "PER8596L",
                                                                      GuideNumber = "VQ54JUJAKI",
                                                                      IdClient = 1,
                                                                      IdSeaport = 1,
                                                                      IdTypeProduct = 1,
                                                                      ClientEntity = new ClientEntity()
                                                                      {
                                                                          Id =1,
                                                                          Name= "Test",
                                                                          LastName="MockLastName",
                                                                      },
                                                                      SeaportEntity = new SeaportEntity()
                                                                      {
                                                                          Id =1,
                                                                          Seaport="MockSeaport"
                                                                      },
                                                                      TypeProductEntity = new TypeProductEntity()
                                                                      {
                                                                          Id=1,
                                                                          TypeProduct="MockTypeProduct"
                                                                      }
                                                                  },
                                                              });

            //act
            List<MaritimeLotDto> result = _maritimeLotServices.GetAll();

            //assert
            var model = Assert.IsAssignableFrom<List<MaritimeLotDto>>(result);
            Assert.True(model.Any());
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public void GetByGuideNumber_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.MaritimeLotRepository.FirstOrDefault(m => m.GuideNumber.ToLower() == "KKMOV2U1Z1".ToLower(),
                                                                              c => c.ClientEntity,
                                                                              s => s.SeaportEntity,
                                                                              t => t.TypeProductEntity))
                                                              .Returns(new MaritimeLotEntity()
                                                              {
                                                                  Id = 123,
                                                                  Amount = 5,
                                                                  Price = 1000,
                                                                  DeliveryDate = DateTime.Now,
                                                                  RegisterDate = DateTime.Now,
                                                                  FleetNumber = "PER7856J",
                                                                  GuideNumber = "KKMOV2U1Z1",
                                                                  IdClient = 1,
                                                                  IdSeaport = 1,
                                                                  IdTypeProduct = 1,
                                                                  ClientEntity = new ClientEntity()
                                                                  {
                                                                      Id = 1,
                                                                      Name = "Test",
                                                                      LastName = "MockLastName",
                                                                  },
                                                                  SeaportEntity = new SeaportEntity()
                                                                  {
                                                                      Id = 1,
                                                                      Seaport = "MockSeaport"
                                                                  },
                                                                  TypeProductEntity = new TypeProductEntity()
                                                                  {
                                                                      Id = 1,
                                                                      TypeProduct = "MockTypeProduct"
                                                                  }
                                                              });

            //act
            MaritimeLotDto result = _maritimeLotServices.GetByGuideNumber("KKMOV2U1Z1");

            //assert
            var model = Assert.IsAssignableFrom<MaritimeLotDto>(result);
            Assert.True(model != null);
        }

        [Fact]
        public void GetByGuideNumber_Null_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.MaritimeLotRepository.FirstOrDefault(m => m.GuideNumber.ToLower() == It.IsAny<string>().ToLower(),
                                                                              c => c.ClientEntity,
                                                                              s => s.SeaportEntity,
                                                                              t => t.TypeProductEntity))
                                                              .Returns(new MaritimeLotEntity());

            //act and assert
            var exception = Assert.Throws<BusinessException>(() => _maritimeLotServices.GetByGuideNumber(It.IsAny<string>()));
            Assert.Equal(GeneralMessages.ItemNoFound, exception.Message);
        }

        [Fact]
        public async Task Insert_Error_Amount_Test()
        {
            //arrange
            AddMaritimeLotDto addMaritime = new AddMaritimeLotDto()
            {
                Amount = 0,
                Price = 1000,
                DeliveryDate = DateTime.Now,
                FleetNumber = "PER7856J",
                IdClient = 1,
                IdSeaport = 1,
                IdTypeProduct = 1,
            };

            //act and assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _maritimeLotServices.Insert(addMaritime));
            Assert.Equal(GeneralMessages.IncorrectAmount, exception.Message);
        }

        [Fact]
        public async Task Insert_Error_Price_Test()
        {
            //arrange
            AddMaritimeLotDto addMaritime = new AddMaritimeLotDto()
            {
                Amount = 5,
                Price = 0,
                DeliveryDate = DateTime.Now,
                FleetNumber = "PER7856J",
                IdClient = 1,
                IdSeaport = 1,
                IdTypeProduct = 1,
            };

            //act and assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _maritimeLotServices.Insert(addMaritime));
            Assert.Equal(GeneralMessages.IncorrectPrice, exception.Message);
        }

        [Fact]
        public async Task Insert_ValidateFleetNumber_Test()
        {
            //arrange
            AddMaritimeLotDto addMaritime = new AddMaritimeLotDto()
            {
                Amount = 5,
                Price = 1000,
                DeliveryDate = DateTime.Now,
                FleetNumber = "MOCK74MC",
                IdClient = 1,
                IdSeaport = 1,
                IdTypeProduct = 1,
            };

            //act and assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _maritimeLotServices.Insert(addMaritime));
            Assert.Equal(GeneralMessages.IncorrectFleetFormat, exception.Message);
        }

        [Fact]
        public async Task Insert_GetDiscount_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.MaritimeLotRepository.Insert(It.IsAny<MaritimeLotEntity>()));
            _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);

            AddMaritimeLotDto addMaritime = new AddMaritimeLotDto()
            {
                Amount = 11,
                Price = 1000,
                DeliveryDate = DateTime.Now,
                FleetNumber = "MOC1745k",
                IdClient = 1,
                IdSeaport = 1,
                IdTypeProduct = 1,
            };

            //act 
            bool result = await _maritimeLotServices.Insert(addMaritime);

            //assert
            Assert.True(result);
        }

        [Fact]
        public async Task Insert_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.MaritimeLotRepository.Insert(It.IsAny<MaritimeLotEntity>()));
            _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);

            AddMaritimeLotDto addMaritime = new AddMaritimeLotDto()
            {
                Amount = 5,
                Price = 1000,
                DeliveryDate = DateTime.Now,
                FleetNumber = "MOC1745k",
                IdClient = 1,
                IdSeaport = 1,
                IdTypeProduct = 1,
            };

            //act 
            bool result = await _maritimeLotServices.Insert(addMaritime);

            //assert
            Assert.True(result);
        }


        [Fact]
        public async Task Update_Error_NotFound_Test()
        {
            //arrange
            PrepareData();

            UpdateMaritimeLotDto update = new UpdateMaritimeLotDto()
            {
                Id = 1234,
                Amount = 0,
                Price = 1000,
                DeliveryDate = DateTime.Now,
                FleetNumber = "PER7856J",
                IdClient = 1,
                IdSeaport = 1,
                IdTypeProduct = 1,
            };

            //act and assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _maritimeLotServices.Update(update));
            Assert.Equal(GeneralMessages.ItemNoFound, exception.Message);
        }

        [Fact]
        public async Task Update_Error_Amount_Test()
        {
            //arrange
            PrepareData();
            UpdateMaritimeLotDto update = new UpdateMaritimeLotDto()
            {
                Id = 123,
                Amount = 0,
                Price = 1000,
                DeliveryDate = DateTime.Now,
                FleetNumber = "PER7856J",
                IdClient = 1,
                IdSeaport = 1,
                IdTypeProduct = 1,
            };

            //act and assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _maritimeLotServices.Update(update));
            Assert.Equal(GeneralMessages.IncorrectAmount, exception.Message);
        }

        [Fact]
        public async Task Update_Error_Price_Test()
        {
            //arrange
            PrepareData();

            UpdateMaritimeLotDto update = new UpdateMaritimeLotDto()
            {
                Id = 123,
                Amount = 1,
                Price = 0,
                DeliveryDate = DateTime.Now,
                FleetNumber = "PER7856J",
                IdClient = 1,
                IdSeaport = 1,
                IdTypeProduct = 1,
            };

            //act and assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _maritimeLotServices.Update(update));
            Assert.Equal(GeneralMessages.IncorrectPrice, exception.Message);
        }

        [Fact]
        public async Task Update_Error_ValidateFleetNumber_Test()
        {
            //arrange
            PrepareData();

            UpdateMaritimeLotDto update = new UpdateMaritimeLotDto()
            {
                Id = 123,
                Amount = 1,
                Price = 100,
                DeliveryDate = DateTime.Now,
                FleetNumber = "MOCK123",
                IdClient = 1,
                IdSeaport = 1,
                IdTypeProduct = 1,
            };

            //act and assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _maritimeLotServices.Update(update));
            Assert.Equal(GeneralMessages.IncorrectFleetFormat, exception.Message);
        }

        [Fact]
        public async Task Update_GetDiscount_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.MaritimeLotRepository.Update(It.IsAny<MaritimeLotEntity>()));
            _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);

            PrepareData();

            UpdateMaritimeLotDto update = new UpdateMaritimeLotDto()
            {
                Id = 123,
                Amount = 11,
                Price = 100,
                DeliveryDate = DateTime.Now,
                FleetNumber = "PER7856J",
                IdClient = 1,
                IdSeaport = 1,
                IdTypeProduct = 1,
            };


            //act 
            bool result = await _maritimeLotServices.Update(update);

            //assert
            Assert.True(result);
        }

        [Fact]
        public async Task Update_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.MaritimeLotRepository.Update(It.IsAny<MaritimeLotEntity>()));
            _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);

            PrepareData();

            UpdateMaritimeLotDto update = new UpdateMaritimeLotDto()
            {
                Id = 123,
                Amount = 1,
                Price = 100,
                DeliveryDate = DateTime.Now,
                FleetNumber = "PER7856J",
                IdClient = 1,
                IdSeaport = 1,
                IdTypeProduct = 1,
            };

            //act 
            bool result = await _maritimeLotServices.Update(update);

            //assert
            Assert.True(result);
        }


        [Fact]
        public async Task Delete_NotFound_Test()
        {
            //arrange
            PrepareData();

            //act and assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _maritimeLotServices.Delete(It.IsAny<int>()));
            Assert.Equal(GeneralMessages.ItemNoFound, exception.Message);

        }

        [Fact]
        public async Task Delete_Test()
        {
            //arrange
            PrepareData();

            _unitOfWorkMock.Setup(x => x.MaritimeLotRepository.Delete(It.IsAny<MaritimeLotEntity>()));
            _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);

            //act and assert
            bool result = await _maritimeLotServices.Delete(123);
            Assert.True(result);
        }
        #endregion
    }
}
