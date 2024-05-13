using API.Models;

namespace API.Services
{
    public interface IRealEstateService
    {
        RealEstateViewModel Create(RealEstateInsertModel realEstate);
        RealEstateViewModel Update(RealEstateUpdateModel realEstate);
        bool DeleteById(int id);
        IEnumerable<RealEstateViewModel> GetAll();
        RealEstateViewModel? GetById(int id);
        IEnumerable<RealEstateViewModel> GetByOwnerType(int ownerTypeId);
        IEnumerable<RealEstateViewModel> GetByType(int realEstateTypeId);
        IEnumerable<RealEstateViewModel> GetVacant();
        IEnumerable<RealEstateViewModel> GetByOwner(string ownerId);
        Task BookForUser(int realEstateId, string userId);
        void FreeRealEsate(int realEstateId);
    }
}