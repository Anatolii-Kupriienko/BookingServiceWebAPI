using API.DbAccess;
using API.DbAccess.Models;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace API.Services
{
    public class RealEstateService : IRealEstateService
    {
        private readonly IRepository<RealEstateModel> _repository;
        private readonly IMapper mapper;
        private readonly UserManager<UserModel> userManager;

        public RealEstateService(IRepository<RealEstateModel> realEstateRepository, IMapper mapper, UserManager<UserModel> userManager)
        {
            _repository = realEstateRepository;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public void Add(RealEstate model)
        {
            if (model == null || !model.IsValid())
            {
                throw new ArgumentException(nameof(model));
            }

            _repository.Add(mapper.Map<RealEstateModel>(model));
        }

        public void BookForUser(int realEstateId, string userId)
        {
            var realEstate = _repository.GetById(realEstateId);
            var user = userManager.FindByIdAsync(userId).Result;
            if (realEstate is null || user is null || realEstate.OccupiedById != null)
            {
                throw new ArgumentException("Invalid real estate or user");
            }
            // the above check can be separated into several if statements for better readability and error messages
            realEstate.OccupiedById = user.Id;
            _repository.Update(realEstate);
        }

        public bool DeleteById(int id)
        {
            return _repository.DeleteById(id);
        }

        public void FreeRealEsate(int realEstateId)
        {
            var realEstate = _repository.GetById(realEstateId);
            if (realEstate is null || realEstate.OccupiedById == null)
            {
                throw new ArgumentException("Invalid real estate");
            }

            realEstate.OccupiedById = null;
            _repository.Update(realEstate);
        }

        public IEnumerable<RealEstate> Get()
        {
            return mapper.Map<IEnumerable<RealEstate>>(_repository.Get());
        }

        public RealEstate? GetById(int id)
        {
            var result = _repository.GetById(id);
            if (result is not null)
            {
                return mapper.Map<RealEstate>(result);
            }

            return null;
        }

        public IEnumerable<RealEstate> GetByOwnerType(int ownerTypeId)
        {
            var data = _repository.Get().Where(x => x.Owner.OwnerTypeId == ownerTypeId);
            if (!data.Any())
            {
                throw new ArgumentException("No real estates found for the given owner type");
            }

            return mapper.Map<IEnumerable<RealEstate>>(data);
        }

        public IEnumerable<RealEstate> GetByType(int realEstateTypeId)
        {
            var data = _repository.Get().Where(x => x.RealEstateTypeId == realEstateTypeId);
            if (!data.Any())
            {
                throw new ArgumentException("No real estates found for the given type");
            }

            return mapper.Map<IEnumerable<RealEstate>>(data);
        }

        public IEnumerable<RealEstate> GetVacant()
        { // all of these where clauses can be added as separate methods in the repository
            var data = _repository.Get().Where(x => x.OccupiedById == null);
            if (!data.Any())
            {
                throw new ArgumentException("No vacant real estates found");
            }

            return mapper.Map<IEnumerable<RealEstate>>(data);
        }

        public void Update(RealEstate model)
        {
            if (model == null || !model.IsValid())
            {
                throw new ArgumentException(nameof(model));
            }

            _repository.Update(mapper.Map<RealEstateModel>(model));
        }
    }
}