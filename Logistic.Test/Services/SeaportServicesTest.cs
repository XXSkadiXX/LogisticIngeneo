using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Models.General;
using Infraestructure.Entity.Models.Logistic;
using Logistic.Common.Exceptions;
using Logistic.Common.Resources;
using Logistic.Domain.DTO.Seaport;
using Logistic.Domain.Services.Logistic;
using Logistic.Domain.Services.Logistic.Interfaces;
using Moq;
using Xunit;

namespace Logistic.Test.Services
{
    public class SeaportServicesTest
    {
        #region Attributes
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly ISeaportServices _seaportServices;
        #endregion

        #region Builder
        public SeaportServicesTest()
        {

            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _seaportServices = new SeaportServices(_unitOfWorkMock.Object);
        }
        #endregion
        private void PrepareData()
        {
            _unitOfWorkMock.Setup(x => x.SeaportRepository.FirstOrDefault(w => w.Id == 1))
                                                          .Returns(new SeaportEntity()
                                                          {
                                                              Id = 1,
                                                              Seaport = "Test",
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
        public void GetAllSeaports_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.SeaportRepository.GetAll(p => p.CountryEntity)).Returns(new List<SeaportEntity>()
            {
                new SeaportEntity()
                {
                    Id = 1,
                    Seaport= "Test",
                    IdCountry=1,
                    CountryEntity=new CountryEntity()
                    {
                        Id=1,
                        Country= "CountryTest",
                    }
                },
                new SeaportEntity()
                {
                    Id = 2,
                    Seaport= "Test",
                    IdCountry=1,
                    CountryEntity=new CountryEntity()
                    {
                        Id=1,
                        Country= "CountryTest",
                    }
                },
            });

            //act
            List<SeaportDto> result = _seaportServices.GetAllSeaports();

            //assert
            var model = Assert.IsAssignableFrom<List<SeaportDto>>(result);
            Assert.True(model.Any());
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public void GetAllSeaportsByCountry_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.SeaportRepository.FindAll(c => c.IdCountry == 1,
                                                                     p => p.CountryEntity))
                                                            .Returns(new List<SeaportEntity>()
            {
                new SeaportEntity()
                {
                    Id = 1,
                    Seaport= "Test",
                    IdCountry=1,
                    CountryEntity=new CountryEntity()
                    {
                        Id=1,
                        Country= "CountryTest",
                    }
                },
                new SeaportEntity()
                {
                    Id = 2,
                    Seaport= "Test",
                    IdCountry=1,
                    CountryEntity=new CountryEntity()
                    {
                        Id=1,
                        Country= "CountryTest",
                    }
                },
            });

            //act
            List<SeaportDto> result = _seaportServices.GetAllSeaportsByCountry(1);

            //assert
            var model = Assert.IsAssignableFrom<List<SeaportDto>>(result);
            Assert.True(model.Any());
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public async Task InsertSeaport_ExistedWarehouse_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.SeaportRepository.FirstOrDefault(w => w.Seaport.ToLower() == "Test".ToLower()))
                                                          .Returns(new SeaportEntity());

            AddSeaportDto add = new AddSeaportDto()
            {
                Name = "Test",
                IdCountry = 1,
            };


            //act and assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _seaportServices.InsertSeaport(add));
            Assert.Equal(GeneralMessages.ExistedRegister, exception.Message);
        }

        [Fact]
        public async Task InsertSeaport_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.SeaportRepository.FirstOrDefault(w => w.Seaport.ToLower() == It.IsAny<string>().ToLower()))
                                                            .Returns(new SeaportEntity());
            _unitOfWorkMock.Setup(x => x.SeaportRepository.Insert(It.IsAny<SeaportEntity>()));
            _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);

            AddSeaportDto add = new AddSeaportDto()
            {
                Name = "Test",
                IdCountry = 1,
            };

            //act and assert
            bool result = await _seaportServices.InsertSeaport(add);
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateSeaport_NotFound_Test()
        {
            //arrange
            PrepareData();

            UpdateSeaportDto update = new UpdateSeaportDto()
            {
                Id = 2,
                Name = "Test",
                IdCountry = 1,
            };


            //act and assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _seaportServices.UpdateSeaport(update));
            Assert.Equal(GeneralMessages.ItemNoFound, exception.Message);
        }

        [Fact]
        public async Task UpdateSeaport_Test()
        {
            //arrange
            PrepareData();
            _unitOfWorkMock.Setup(x => x.SeaportRepository.Update(It.IsAny<SeaportEntity>()));
            _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);

            UpdateSeaportDto update = new UpdateSeaportDto()
            {
                Id = 1,
                Name = "Test",
                IdCountry = 1,
            };


            //act and assert
            bool result = await _seaportServices.UpdateSeaport(update);
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteSeaport_NotFound_Test()
        {
            //arrange
            PrepareData();

            //act and assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _seaportServices.DeleteSeaport(It.IsAny<int>()));
            Assert.Equal(GeneralMessages.ItemNoFound, exception.Message);
        }

        [Fact]
        public async Task DeleteSeaport_Test()
        {
            //arrange
            PrepareData();
            _unitOfWorkMock.Setup(x => x.SeaportRepository.Delete(It.IsAny<SeaportEntity>()));
            _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);

            //act and assert
            bool result = await _seaportServices.DeleteSeaport(1);
            Assert.True(result);
        }

        #endregion

    }
}
