using API.BusinessLogic.Services;
using API.DbAccess;
using API.DbAccess.Models;
using API.Models;
using AutoMapper;

namespace API.Services
{
    public class RealEstateTypeService : IRealEstateTypeService
    {
        private readonly IRepository<RealEstateTypeModel> _repository;
        private readonly IMapper mapper;

        public RealEstateTypeService(IRepository<RealEstateTypeModel> realEstateTypeRepository, IMapper mapper)
        {
            _repository = realEstateTypeRepository;
            this.mapper = mapper;
        }

        public RealEstateType Create(RealEstateTypeInsertModel realEstateType)
        {
            if (realEstateType == null || !realEstateType.IsValid())
            {
                throw new ArgumentException(nameof(realEstateType));
            }

            var entity = _repository.Add(mapper.Map<RealEstateTypeModel>(realEstateType));
            return mapper.Map<RealEstateType>(entity);
        }

        public bool DeleteById(int id)
        {
            return _repository.DeleteById(id);
        }

        public IEnumerable<RealEstateType> GetAll()
        {
            return mapper.Map<IEnumerable<RealEstateType>>(_repository.Get());
        }

        public RealEstateType? GetById(int id)
        {
            var result = _repository.GetById(id);
            if (result is not null)
            {
                return mapper.Map<RealEstateType>(result);
            }

            return null;
        }

        RealEstateType IRealEstateTypeService.Update(RealEstateType realEstateType)
        {
            if (realEstateType == null || !realEstateType.IsValid())
            {
                throw new ArgumentException(nameof(realEstateType));
            }

            var entity = _repository.Update(mapper.Map<RealEstateTypeModel>(realEstateType));
            return mapper.Map<RealEstateType>(entity);
        }
    }
}