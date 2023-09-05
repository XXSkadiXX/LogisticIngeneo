using Logistic.Domain.DTO.Logistic.LandLot;

namespace Logistic.Domain.Services.Logistic.Interfaces
{
    public interface ILandLotServices
    {
        List<LandLotDto> GetAll();
        LandLotDto GetByGuideNumber(string guideNumber);
        Task<bool> Insert(AddLandLotDto lot);
        Task<bool> Update(UpdateLandLotDto lot);
        Task<bool> Delete(int idLandLot);

    }
}
