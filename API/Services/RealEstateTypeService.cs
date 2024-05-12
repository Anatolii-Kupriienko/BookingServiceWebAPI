using API.DbAccess;
using API.Models;

namespace API.Services
{
    public class RealEstateTypeService : ICrud<RealEstateType>
    {
        private readonly RealEstateTypeRepository _repository;

        public RealEstateTypeService(RealEstateTypeRepository realEstateTypeRepository)
        {
            _repository = realEstateTypeRepository;
        }
        
        public void Add(RealEstateType model)
        {
            throw new NotImplementedException();
        }

        public void Delete(RealEstateType model)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RealEstateType> Get()
        {
            throw new NotImplementedException();
        }

        public RealEstateType? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(RealEstateType model)
        {
            throw new NotImplementedException();
        }
    }
}