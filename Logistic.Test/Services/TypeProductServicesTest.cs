using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Models.Logistic;
using Logistic.Common.Exceptions;
using Logistic.Common.Resources;
using Logistic.Domain.DTO.Logistic.TypeProduct;
using Logistic.Domain.Services.Logistic;
using Logistic.Domain.Services.Logistic.Interfaces;
using Moq;
using Xunit;

namespace Logistic.Test.Services
{
    public class TypeProductServicesTest
    {
        #region Attributes
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly ITypeProductServices _typeProductServices;
        #endregion

        #region Builder
        public TypeProductServicesTest()
        {

            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _typeProductServices = new TypeProductServices(_unitOfWorkMock.Object);
        }
        #endregion

        private void PrepareData()
        {
            _unitOfWorkMock.Setup(x => x.TypeProductRepository.FirstOrDefault(w => w.Id == 1))
                                                          .Returns(new TypeProductEntity()
                                                          {
                                                              Id = 1,
                                                              Description = "Test",
                                                              TypeProduct = "MockTypeProduct"
                                                          });
        }

        #region Test
        [Fact]
        public void GetAllTypeProduct_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.TypeProductRepository.GetAll()).Returns(new List<TypeProductEntity>()
            {
                new TypeProductEntity()
                {
                    Id = 1,
                    Description = "Test",
                    TypeProduct="MockTypeProduct"
                },
                new TypeProductEntity()
                {
                    Id = 2,
                    Description = "Test",
                    TypeProduct="MockTypeProduct"
                },
            });

            //act
            List<TypeProductDto> result = _typeProductServices.GetAllTypeProduct();

            //assert
            var model = Assert.IsAssignableFrom<List<TypeProductDto>>(result);
            Assert.True(model.Any());
            Assert.Equal(2, model.Count);
        }
        [Fact]
        public async Task InsertTypeProduct_ExistedTypeProduct_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.TypeProductRepository.FirstOrDefault(w => w.TypeProduct.ToLower() == "MockTypeProduct".ToLower()))
                                                          .Returns(new TypeProductEntity());
            AddTypeProductDto add = new AddTypeProductDto()
            {
                Description = "Test",
                TypeProduct = "MockTypeProduct",
            };

            //act and assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _typeProductServices.InsertTypeProduct(add));
            Assert.Equal(GeneralMessages.ExistedRegister, exception.Message);
        }

        [Fact]
        public async Task InsertTypeProduct_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.TypeProductRepository.FirstOrDefault(w => w.TypeProduct.ToLower() == It.IsAny<string>().ToLower()))
                                                         .Returns(new TypeProductEntity());
            _unitOfWorkMock.Setup(x => x.TypeProductRepository.Insert(It.IsAny<TypeProductEntity>()));
            _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);

            AddTypeProductDto add = new AddTypeProductDto()
            {
                Description = "Test",
                TypeProduct = "MockTypeProduct",
            };

            //act and assert
            bool result = await _typeProductServices.InsertTypeProduct(add);
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateTypeProduct_NotFound_Test()
        {
            //arrange
            PrepareData();

            TypeProductDto update = new TypeProductDto()
            {
                Id = 2,
                Description = "Test",
                TypeProduct = "MockTypeProduct",
            };


            //act and assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _typeProductServices.UpdateTypeProduct(update));
            Assert.Equal(GeneralMessages.ItemNoFound, exception.Message);
        }

        [Fact]
        public async Task UpdateTypeProduct_Test()
        {
            //arrange
            PrepareData();
            _unitOfWorkMock.Setup(x => x.TypeProductRepository.Update(It.IsAny<TypeProductEntity>()));
            _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);

            TypeProductDto update = new TypeProductDto()
            {
                Id = 1,
                Description = "Test",
                TypeProduct = "MockTypeProduct",
            };


            //act and assert
            bool result = await _typeProductServices.UpdateTypeProduct(update);
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteTypeProduct_NotFound_Test()
        {
            //arrange
            PrepareData();

            //act
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _typeProductServices.DeleteTypeProduct(It.IsAny<int>()));

            //assert
            Assert.Equal(GeneralMessages.ItemNoFound, exception.Message);
        }

        [Fact]
        public async Task DeleteTypeProduct_Test()
        {
            //arrange
            PrepareData();
            _unitOfWorkMock.Setup(x => x.TypeProductRepository.Delete(It.IsAny<TypeProductEntity>()));
            _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);

            //act
            bool result = await _typeProductServices.DeleteTypeProduct(1);

            //assert
            Assert.True(result);
        }

        #endregion
    }
}
