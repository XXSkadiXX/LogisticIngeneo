using Logistic.Domain.DTO.Logistic.TypeProduct;

namespace Logistic.Domain.Services.Logistic.Interfaces
{
    public interface ITypeProductServices
    {
        List<TypeProductDto> GetAllTypeProduct();
        Task<bool> InsertTypeProduct(AddTypeProductDto nTypeProduct);
        Task<bool> UpdateTypeProduct(TypeProductDto uTypeProduct);
        Task<bool> DeleteTypeProduct(int idTypeProduct);
    }
}
