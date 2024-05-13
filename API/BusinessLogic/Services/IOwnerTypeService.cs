using API.Models;

namespace API.BusinessLogic.Services
{
    public interface IOwnerTypeService
    {
        OwnerType Create(OwnerTypeInsertModel ownerType);
        OwnerType Update(OwnerType ownerType);
        bool DeleteById(int id);
        OwnerType? GetById(int id);
        IEnumerable<OwnerType> GetAll();
    }
}