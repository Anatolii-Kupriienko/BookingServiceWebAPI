using API.DbAccess;
using API.DbAccess.Models;
using API.Models;
using AutoMapper;

namespace API.Services
{
    public class RealEstateTypeService : ICrud<RealEstateType>
    {
        private readonly IRepository<RealEstateTypeModel> _repository;
        private readonly IMapper mapper;

        public RealEstateTypeService(IRepository<RealEstateTypeModel> realEstateTypeRepository, IMapper mapper)
        {
            _repository = realEstateTypeRepository;
            this.mapper = mapper;
        }

        public void Add(RealEstateType model)
        {
            if (model == null || !model.IsValid())
            {
                throw new ArgumentException(nameof(model));
            }

            _repository.Add(mapper.Map<RealEstateTypeModel>(model));
        }

        public bool DeleteById(int id)
        {
            return _repository.DeleteById(id);
        }

        public IEnumerable<RealEstateType> Get()
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

        public void Update(RealEstateType model)
        {
            if (model == null || !model.IsValid())
            {
                throw new ArgumentException(nameof(model));
            }

            _repository.Update(mapper.Map<RealEstateTypeModel>(model));
        }
    }
}