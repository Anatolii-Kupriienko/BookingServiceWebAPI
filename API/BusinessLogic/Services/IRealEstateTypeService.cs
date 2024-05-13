using API.Models;

namespace API.BusinessLogic.Services
{
    public interface IRealEstateTypeService
    {
        RealEstateType Create(RealEstateTypeInsertModel realEstateType);   
        RealEstateType Update(RealEstateType realEstateType);
        bool DeleteById(int id);
        IEnumerable<RealEstateType> GetAll();
        RealEstateType GetById(int id);
    }
}