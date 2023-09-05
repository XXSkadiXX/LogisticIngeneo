using Logistic.Domain.DTO.Logistic.MaritimeLot;

namespace Logistic.Domain.Services.Logistic.Interfaces
{
    public interface IMaritimeLotServices
    {
        List<MaritimeLotDto> GetAll();
        MaritimeLotDto GetByGuideNumber(string guideNumber);
        Task<bool> Insert(AddMaritimeLotDto lot);
        Task<bool> Update(UpdateMaritimeLotDto lot);
        Task<bool> Delete(int idMaritimeLot);
    }
}
