using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Models.Security;
using Logistic.Common.Exceptions;
using Logistic.Common.Resources;
using Logistic.Domain.DTO.Logistic.User;
using Logistic.Domain.Services.Logistic;
using Logistic.Domain.Services.Logistic.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;
using NETCore.Encrypt;
using Xunit;

namespace Logistic.Test.Services
{
    public class UserServicesTest
    {
        #region Attributes
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly IUserServices _userServices;
        #endregion

        #region Builder
        public UserServicesTest()
        {
            //AppSetting
            IConfigurationRoot configuration = new ConfigurationBuilder()
                 .AddInMemoryCollection(ConfigurationMock.ConfigAppSetting())
                 .Build();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _userServices = new UserServices(_unitOfWorkMock.Object, configuration);
        }
        #endregion

        #region Test

        [Fact]
        public void Login_BadCredential_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.UserRepository.FirstOrDefault(u => u.Email == It.IsAny<string>()
                                                                       && u.Password == EncryptProvider.Sha256(It.IsAny<string>())))
                                                     .Returns(new UserEntity());
            LoginDto login = new LoginDto()
            {
                Email = "mock@mock.com",
                Password = "MockTest",
            };

            //act and assert
            var exception = Assert.Throws<BusinessException>(() => _userServices.Login(login));
            Assert.Equal(GeneralMessages.BadCredentials, exception.Message);
        }

        [Fact]
        public void Login__Test()
        {
            //arrange
            LoginDto loginDto = new LoginDto()
            {
                Email = "mock@gmail.com",
                Password = "123456",
            };

            _unitOfWorkMock.Setup(x => x.UserRepository.FirstOrDefault(e => e.Email == loginDto.Email
                                                                         && e.Password == EncryptProvider.Sha256(loginDto.Password)))
                                                        .Returns(new UserEntity()
                                                        {
                                                            Id = 1,
                                                            Email = "mock@gmail.com",
                                                            Password = EncryptProvider.Sha256("123456"),
                                                            Name = "Mock",
                                                            LastName = "MockLastName",
                                                            IdRol = 1,
                                                            RegisterDate = DateTime.Now,
                                                            RolEntity = new RolEntity()
                                                            {
                                                                IdRol = 1,
                                                                Rol = "RolMock"
                                                            }
                                                        });


            //act
            TokenDto token = _userServices.Login(loginDto);

            Assert.NotNull(token);
            Assert.IsType<TokenDto>(token);
        }

        [Fact]
        public async Task RegisterUser_ExistEmail_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.UserRepository.FirstOrDefault(e => e.Email.ToLower() == "mock@gmail.com".ToLower()))
                                                        .Returns(new UserEntity()
                                                        {
                                                            Id = 1,
                                                            Email = "mock@gmail.com",
                                                            Password = EncryptProvider.Sha256("123456"),
                                                            Name = "Mock",
                                                            LastName = "MockLastName",
                                                            IdRol = 1,
                                                            RegisterDate = DateTime.Now,
                                                            RolEntity = new RolEntity()
                                                            {
                                                                IdRol = 1,
                                                                Rol = "RolMock"
                                                            }
                                                        });
            AddUserDto add = new AddUserDto()
            {
                Email = "mock@gmail.com",
                Password = "123456",
                Name = "Mock",
                LastName = "MockLastName",
            };

            //act and assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _userServices.RegisterUser(add));
            Assert.Equal(GeneralMessages.RegisteredEmail, exception.Message);
        }

        [Fact]
        public async Task RegisterUser_Test()
        {
            //arrange
            _unitOfWorkMock.Setup(x => x.UserRepository.FirstOrDefault(e => e.Email.ToLower() == It.IsAny<string>().ToLower()))
                                                        .Returns(new UserEntity());
            _unitOfWorkMock.Setup(x => x.UserRepository.Insert(It.IsAny<UserEntity>()));
            _unitOfWorkMock.Setup(x => x.Save()).ReturnsAsync(1);

            AddUserDto add = new AddUserDto()
            {
                Email = "mock@gmail.com",
                Password = "123456",
                Name = "Mock",
                LastName = "MockLastName",
            };

            //act
            bool result = await _userServices.RegisterUser(add);

            //assert
            Assert.True(result);
        }

        #endregion
    }
}
