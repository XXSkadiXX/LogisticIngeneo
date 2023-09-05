using Logistic.Domain.DTO.Seaport;

namespace Logistic.Domain.Services.Logistic.Interfaces
{
    public interface ISeaportServices
    {
        List<SeaportDto> GetAllSeaports();
        List<SeaportDto> GetAllSeaportsByCountry(int countryId);
        Task<bool> InsertSeaport(AddSeaportDto addSeaport);
        Task<bool> UpdateSeaport(UpdateSeaportDto uSeaport);
        Task<bool> DeleteSeaport(int idSeaport);

    }
}
