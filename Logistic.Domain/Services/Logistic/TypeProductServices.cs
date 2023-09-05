using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Models.Logistic;
using Logistic.Common.Exceptions;
using Logistic.Common.Resources;
using Logistic.Domain.DTO.Logistic.TypeProduct;
using Logistic.Domain.Services.Logistic.Interfaces;

namespace Logistic.Domain.Services.Logistic
{
    public class TypeProductServices : ITypeProductServices
    {
        #region Attribute
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Builder
        public TypeProductServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods

        private TypeProductEntity GetTypeProduct(int idTypeProduct) => _unitOfWork.TypeProductRepository.FirstOrDefault(x => x.Id == idTypeProduct);

        public List<TypeProductDto> GetAllTypeProduct()
        {
            IEnumerable<TypeProductEntity> listTypeProduct = _unitOfWork.TypeProductRepository.GetAll();
            List<TypeProductDto> result = listTypeProduct.Select(x => new TypeProductDto()
            {
                Id = x.Id,
                TypeProduct = x.TypeProduct,
                Description = x.Description,


            }).ToList();

            return result;
        }

        public async Task<bool> InsertTypeProduct(AddTypeProductDto nTypeProduct)
        {
            if (ExistedTypeProduct(nTypeProduct.TypeProduct))
                throw new BusinessException(GeneralMessages.ExistedRegister);

            TypeProductEntity newTypeProduct = new TypeProductEntity()
            {
                TypeProduct = nTypeProduct.TypeProduct,
                Description = nTypeProduct.Description,
            };

            _unitOfWork.TypeProductRepository.Insert(newTypeProduct);

            return await _unitOfWork.Save() > 0;
        }

        public async Task<bool> UpdateTypeProduct(TypeProductDto uTypeProduct)
        {
            TypeProductEntity updateTypeProduct = GetTypeProduct(uTypeProduct.Id);
            if (updateTypeProduct == null)
                throw new BusinessException(GeneralMessages.ItemNoFound);

            updateTypeProduct.TypeProduct = uTypeProduct.TypeProduct;
            updateTypeProduct.Description = uTypeProduct.Description;

            _unitOfWork.TypeProductRepository.Update(updateTypeProduct);

            return await _unitOfWork.Save() > 0;
        }

        public async Task<bool> DeleteTypeProduct(int idTypeProduct)
        {
            TypeProductEntity typeProduct = GetTypeProduct(idTypeProduct);
            if (typeProduct == null)
                throw new BusinessException(GeneralMessages.ItemNoFound);

            _unitOfWork.TypeProductRepository.Delete(typeProduct);

            return await _unitOfWork.Save() > 0;
        }

        private bool ExistedTypeProduct(string typeProduct)
        {
            bool result = false;
            if (_unitOfWork.TypeProductRepository.FirstOrDefault(x => x.TypeProduct.ToLower() == typeProduct.ToLower()) != null)
                result = true;

            return result;

        }

        #endregion
    }
}
