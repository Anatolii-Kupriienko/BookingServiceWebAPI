using API.DbAccess;
using API.DbAccess.Models;
using API.Models;
using AutoMapper;

namespace API.Services
{
    public class OwnerTypeService : ICrud<OwnerType>
    {
        private readonly IRepository<OwnerTypeModel> _repository;
        private readonly IMapper mapper;

        public OwnerTypeService(IRepository<OwnerTypeModel> ownerTypeRepository, IMapper mapper)
        {
            _repository = ownerTypeRepository;
            this.mapper = mapper;
        }

        public void Add(OwnerType model)
        {
            if (model == null || !model.IsValid())
            {
                throw new ArgumentException(nameof(model));
            }

            _repository.Add(mapper.Map<OwnerTypeModel>(model));
        }

        public bool DeleteById(int id)
        {
            return _repository.DeleteById(id);
        }

        public IEnumerable<OwnerType> Get()
        {
            return mapper.Map<IEnumerable<OwnerType>>(_repository.Get());
        }

        public OwnerType? GetById(int id)
        {
            var result = _repository.GetById(id);
            if (result is not null)
            {
                return mapper.Map<OwnerType>(result);
            }

            return null;
        }

        public void Update(OwnerType model)
        {
            if (model == null || !model.IsValid())
            {
                throw new ArgumentException(nameof(model));
            }

            _repository.Update(mapper.Map<OwnerTypeModel>(model));
        }
    }
}