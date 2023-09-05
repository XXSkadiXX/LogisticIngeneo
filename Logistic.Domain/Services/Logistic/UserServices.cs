using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Models.Security;
using Logistic.Common.Enums;
using Logistic.Common.Exceptions;
using Logistic.Common.Helpers;
using Logistic.Common.Resources;
using Logistic.Domain.DTO.Logistic.User;
using Logistic.Domain.Services.Logistic.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NETCore.Encrypt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Logistic.Common.Constant.Const;

namespace Logistic.Domain.Services.Logistic
{
    public class UserServices : IUserServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        #endregion

        #region Builder
        public UserServices(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        #endregion

        #region Authentication
        public TokenDto Login(LoginDto loginDto)
        {
            UserEntity user = _unitOfWork.UserRepository.FirstOrDefault(x => x.Email == loginDto.Email
                                                                          && x.Password == EncryptProvider.Sha256(loginDto.Password));

            if (user == null)
                throw new BusinessException(GeneralMessages.BadCredentials);

            //TOKEN
            return GenerateTokenJWT(user);
        }

        public TokenDto GenerateTokenJWT(UserEntity userEntity)
        {
            IConfigurationSection tokenAppSetting = _configuration.GetSection("Tokens");

            var _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenAppSetting.GetSection("Key").Value));
            var _signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var _header = new JwtHeader(_signingCredentials);

            var _Claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(TypeClaims.IdUser,userEntity.Id.ToString()),
                new Claim(TypeClaims.Email,userEntity.Email),
                new Claim(TypeClaims.IdRol,userEntity.IdRol.ToString()),
            };
            var _payload = new JwtPayload(
                    issuer: tokenAppSetting.GetSection("Issuer").Value,
                    audience: tokenAppSetting.GetSection("Audience").Value,
                    claims: _Claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddDays(30)
                );
            var _token = new JwtSecurityToken(
                    _header,
                    _payload
                );
            TokenDto token = new TokenDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(_token),
                Expiration = Helper.ConvertToUnixTimestamp(_token.ValidTo),
            };

            return token;
        }

        #endregion

        #region Methods

        public async Task<bool> RegisterUser(AddUserDto register)
        {
            if (ExistEmail(register.Email))
                throw new BusinessException(GeneralMessages.RegisteredEmail);

            UserEntity user = new UserEntity()
            {
                Email = register.Email,
                Password = EncryptProvider.Sha256(register.Password),
                Name = register.Name,
                LastName = register.LastName,
                RegisterDate = DateTime.Now,
                IdRol = (int)Enums.RolUser.Estandar,
            };
            _unitOfWork.UserRepository.Insert(user);

            return await _unitOfWork.Save() > 0;
        }

        #endregion

        private bool ExistEmail(string email)
        {
            bool result = false;
            if (_unitOfWork.UserRepository.FirstOrDefault(x => x.Email.ToLower() == email.ToLower()) != null)
                result = true;

            return result;
        }
    }
}
