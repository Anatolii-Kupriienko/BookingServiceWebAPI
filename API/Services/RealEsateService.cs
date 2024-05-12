using API.DbAccess;
using API.Models;

namespace API.Services
{
    public class RealEstateService : IRealEstateService
    {
        private readonly IRepository<RealEstate> _repository;

        public RealEstateService(IRepository<RealEstate> realEstateRepository)
        {
            _repository = realEstateRepository;
        }

        public void Add(RealEstate model)
        {
            throw new NotImplementedException();
        }

        public void BookForUser(int realEstateId, string userId)
        {
            throw new NotImplementedException();
        }

        public void Delete(RealEstate model)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public void FreeRealEsate(int realEstateId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RealEstate> Get()
        {
            throw new NotImplementedException();
        }

        public RealEstate? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RealEstate> GetByOwnerType(int ownerTypeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RealEstate> GetByType(int realEstateTypeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RealEstate> GetVacant()
        {
            throw new NotImplementedException();
        }

        public void Update(RealEstate model)
        {
            throw new NotImplementedException();
        }
    }
}