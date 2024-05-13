using API.Models;

namespace API.Services
{
    public interface IRealEstateService
    {
        RealEstate Create(RealEstateInsertModel realEstate);
        RealEstate Update(RealEstate realEstate);
        bool DeleteById(int id);
        IEnumerable<RealEstate> GetAll();
        RealEstate? GetById(int id);
        IEnumerable<RealEstate> GetByOwnerType(int ownerTypeId);
        IEnumerable<RealEstate> GetByType(int realEstateTypeId);
        IEnumerable<RealEstate> GetVacant();
        void BookForUser(int realEstateId, string userId);
        void FreeRealEsate(int realEstateId);
    }
}