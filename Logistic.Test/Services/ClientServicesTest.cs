using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Models.General;
using Infraestructure.Entity.Models.Logistic;
using Logistic.Common.Exceptions;
using Logistic.Common.Resources;
using Logistic.Domain.DTO.Logistic.Client;
using Logistic.Domain.Services.Logistic;
using Logistic.Domain.Services.Logistic.Interfaces;
using Moq;
using Xunit;

namespace Logistic.Test.Services
{
    public class ClientServicesTest
    {
        #region Attributes
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IClientServices _clientServices;
        #endregion

        #region Builder
        public ClientServicesTest()
        {

            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _clientServices = new ClientServices(_unitOfWorkMock.Object);
        }
        #endregion

        private void PrepareData()
        {
            _unitOfWorkMock.Setup(x => x.ClientRepository.FirstOrDefault(w => w.Id == 1))
                                                          .Returns(new ClientEntity()
                                                          {
                                                              Id = 1,
                                                              Name = "Test",
                                                              Direction = "Mock",
                                                              IdCountry = 1,
                                                              Email = "Test",
                                                              LastName = "Test",
                                                              Phone = "Test",
                                                              CountryEntity = new CountryEntity()
                                                              {
                                                                  Id = 1,
                                                                  Country = "CountryTest",
                                                              }
                                                          });
        }

        #region Test
        [Fact]
        public void GetAllClients_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.ClientRepository.GetAll(p => p.CountryEntity)).Returns(new List<ClientEntity>()
            {
                new ClientEntity()
                {
                    Id = 1,
                    Name = "Test",
                    Direction="Mock",
                    IdCountry=1,
                    Email="Test",
                    LastName="Test",
                    Phone="Test",
                    CountryEntity=new CountryEntity()
                    {
                        Id=1,
                        Country= "CountryTest",
                    }
                },
                 new ClientEntity()
                {
                    Id = 2,
                    Name = "Test",
                    Direction="Mock",
                    IdCountry=1,
                    Email="Test",
                    LastName="Test",
                    Phone="Test",
                    CountryEntity=new CountryEntity()
                    {
                        Id=1,
                        Country= "CountryTest",
                    }
                },
            });

            //act
            List<ClientDto> result = _clientServices.GetAllClients();

            //assert
            var model = Assert.IsAssignableFrom<List<ClientDto>>(result);
            Assert.True(model.Any());
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public async Task InsertClient_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.ClientRepository.Insert(It.IsAny<ClientEntity>()));
            _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);

            AddClientDto add = new AddClientDto()
            {
                Name = "Test",
                Direction = "Mock",
                IdCountry = 1,
                Email = "Test",
                LastName = "Test",
                Phone = "Test",
            };

            //act and assert
            bool result = await _clientServices.InsertClient(add);
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateClient_NotFound_Test()
        {
            //arrange
            PrepareData();

            UpdateClientDto update = new UpdateClientDto()
            {
                Id = 2,
                Name = "Test",
                Direction = "Mock",
                IdCountry = 1,
                Email = "Test",
                LastName = "Test",
                Phone = "Test",
            };


            //act and assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _clientServices.UpdateClient(update));
            Assert.Equal(GeneralMessages.ItemNoFound, exception.Message);
        }

        [Fact]
        public async Task UpdateClient_Test()
        {
            //arrange
            PrepareData();
            _unitOfWorkMock.Setup(x => x.ClientRepository.Update(It.IsAny<ClientEntity>()));
            _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);

            UpdateClientDto update = new UpdateClientDto()
            {
                Id = 1,
                Name = "Test",
                Direction = "Mock",
                IdCountry = 1,
                Email = "Test",
                LastName = "Test",
                Phone = "Test",
            };


            //act and assert
            bool result = await _clientServices.UpdateClient(update);
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteClient_NotFound_Test()
        {
            //arrange
            PrepareData();

            //act
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _clientServices.DeleteClient(It.IsAny<int>()));

            //assert
            Assert.Equal(GeneralMessages.ItemNoFound, exception.Message);
        }

        [Fact]
        public async Task DeleteClient_Test()
        {
            //arrange
            PrepareData();
            _unitOfWorkMock.Setup(x => x.ClientRepository.Delete(It.IsAny<ClientEntity>()));
            _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);

            //act
            bool result = await _clientServices.DeleteClient(1);

            //assert
            Assert.True(result);
        }

        #endregion
    }
}
