using API.DbAccess;
using API.Models;

namespace API.Services
{
    public class OwnerTypeService : ICrud<OwnerType>
    {
        private readonly OwnerTypeRepository _repository;

        public OwnerTypeService(OwnerTypeRepository ownerTypeRepository)
        {
            _repository = ownerTypeRepository;
        }
        
        public void Add(OwnerType model)
        {
            throw new NotImplementedException();
        }

        public void Delete(OwnerType model)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OwnerType> Get()
        {
            throw new NotImplementedException();
        }

        public OwnerType? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(OwnerType model)
        {
            throw new NotImplementedException();
        }
    }
}