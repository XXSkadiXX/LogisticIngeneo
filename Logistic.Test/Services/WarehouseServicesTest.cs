using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Models.General;
using Infraestructure.Entity.Models.Logistic;
using Logistic.Common.Exceptions;
using Logistic.Common.Resources;
using Logistic.Domain.DTO.Logistic.LandLot;
using Logistic.Domain.DTO.Logistic.Warehouse;
using Logistic.Domain.Services.Logistic;
using Logistic.Domain.Services.Logistic.Interfaces;
using Moq;
using Xunit;

namespace Logistic.Test.Services
{
    public class WarehouseServicesTest
    {
        #region Attributes
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IWarehouseServices _warehouseServices;
        #endregion

        #region Builder
        public WarehouseServicesTest()
        {

            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _warehouseServices = new WarehouseServices(_unitOfWorkMock.Object);
        }
        #endregion

        private void PrepareData()
        {
            _unitOfWorkMock.Setup(x => x.WarehouseRepository.FirstOrDefault(w => w.Id == 1))
                                                          .Returns(new WarehouseEntity()
                                                          {
                                                              Id = 1,
                                                              Warehouse = "Test",
                                                              Direction = "Mock",
                                                              IdCountry = 1,
                                                              CountryEntity = new CountryEntity()
                                                              {
                                                                  Id = 1,
                                                                  Country = "CountryTest",
                                                              }
                                                          });
        }

        #region Test
        [Fact]
        public void GetAllWarehouses_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.WarehouseRepository.GetAll(p => p.CountryEntity)).Returns(new List<WarehouseEntity>()
            {
                new WarehouseEntity()
                {
                    Id = 1,
                    Warehouse = "Test",
                    Direction="Mock",
                    IdCountry=1,
                    CountryEntity=new CountryEntity()
                    {
                        Id=1,
                        Country= "CountryTest",
                    }
                },
                new WarehouseEntity()
                {
                    Id = 2,
                    Warehouse = "Test",
                    Direction="Mock",
                    IdCountry=1,
                    CountryEntity=new CountryEntity()
                    {
                        Id=1,
                        Country= "CountryTest",
                    }
                },
            });

            //act
            List<WarehouseDto> result = _warehouseServices.GetAllWarehouses();

            //assert
            var model = Assert.IsAssignableFrom<List<WarehouseDto>>(result);
            Assert.True(model.Any());
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public void GetAllWarehousesByCountry_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.WarehouseRepository.FindAll(c => c.IdCountry == 1,
                                                                     p => p.CountryEntity))
                                                            .Returns(new List<WarehouseEntity>()
                                                            {
                                                                new WarehouseEntity()
                                                                {
                                                                    Id = 1,
                                                                    Warehouse = "Test",
                                                                    Direction="Mock",
                                                                    IdCountry=1,
                                                                    CountryEntity=new CountryEntity()
                                                                    {
                                                                        Id=1,
                                                                        Country= "CountryTest",
                                                                    }
                                                                },
                                                                new WarehouseEntity()
                                                                {
                                                                    Id = 2,
                                                                    Warehouse = "Test",
                                                                    Direction="Mock",
                                                                    IdCountry=1,
                                                                    CountryEntity=new CountryEntity()
                                                                    {
                                                                        Id=1,
                                                                        Country= "CountryTest",
                                                                    }
                                                                },
                                                            });

            //act
            List<WarehouseDto> result = _warehouseServices.GetAllWarehousesByCountry(1);

            //assert
            var model = Assert.IsAssignableFrom<List<WarehouseDto>>(result);
            Assert.True(model.Any());
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public async Task InsertWarehouse_ExistedWarehouse_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.WarehouseRepository.FirstOrDefault(w => w.Warehouse.ToLower() == "Test".ToLower()))
                                                            .Returns(new WarehouseEntity());

            AddWarehouseDto add = new AddWarehouseDto()
            {
                Name = "Test",
                Direction = "Mock",
                IdCountry = 1,
            };


            //act and assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _warehouseServices.InsertWarehouse(add));
            Assert.Equal(GeneralMessages.ExistedRegister, exception.Message);
        }

        [Fact]
        public async Task InsertWarehouse_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.WarehouseRepository.FirstOrDefault(w => w.Warehouse.ToLower() == It.IsAny<string>().ToLower()))
                                                            .Returns(new WarehouseEntity());
            _unitOfWorkMock.Setup(x => x.WarehouseRepository.Insert(It.IsAny<WarehouseEntity>()));
            _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);

            AddWarehouseDto add = new AddWarehouseDto()
            {
                Name = "Test",
                Direction = "Mock",
                IdCountry = 1,
            };

            //act and assert
            bool result = await _warehouseServices.InsertWarehouse(add);
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateWarehouse_NotFound_Test()
        {
            //arrange
            PrepareData();

            UpdateWareHouseDto update = new UpdateWareHouseDto()
            {
                Id = 2,
                Name = "Test",
                Direction = "Mock",
                IdCountry = 1,
            };


            //act and assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _warehouseServices.UpdateWarehouse(update));
            Assert.Equal(GeneralMessages.ItemNoFound, exception.Message);
        }

        [Fact]
        public async Task UpdateWarehouse_Test()
        {
            //arrange
            PrepareData();
            _unitOfWorkMock.Setup(x => x.WarehouseRepository.Update(It.IsAny<WarehouseEntity>()));
            _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);

            UpdateWareHouseDto update = new UpdateWareHouseDto()
            {
                Id = 1,
                Name = "Test",
                Direction = "Mock",
                IdCountry = 1,
            };


            //act and assert
            bool result = await _warehouseServices.UpdateWarehouse(update);
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteWarehouse_NotFound_Test()
        {
            //arrange
            PrepareData();

            //act and assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _warehouseServices.DeleteWarehouse(It.IsAny<int>()));
            Assert.Equal(GeneralMessages.ItemNoFound, exception.Message);
        }

        [Fact]
        public async Task DeleteWarehouse_Test()
        {
            //arrange
            PrepareData();
            _unitOfWorkMock.Setup(x => x.WarehouseRepository.Delete(It.IsAny<WarehouseEntity>()));
            _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);

            //act and assert
            bool result = await _warehouseServices.DeleteWarehouse(1);
            Assert.True(result);
        }

        #endregion

    }
}
