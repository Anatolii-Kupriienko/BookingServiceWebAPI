using API.Models;

namespace API.Services
{
    public interface IRealEstateService : ICrud<RealEstate>
    {
        IEnumerable<RealEstate> GetByOwnerType(int ownerTypeId);
        IEnumerable<RealEstate> GetByType(int realEstateTypeId);
        IEnumerable<RealEstate> GetVacant();
        void BookForUser(int realEstateId, string userId);
        void FreeRealEsate(int realEstateId);
    }
}