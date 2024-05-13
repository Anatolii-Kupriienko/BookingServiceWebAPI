using API.BusinessLogic.Services;
using API.DbAccess;
using API.DbAccess.Models;
using API.Models;
using AutoMapper;
using AutoMapper.Configuration.Annotations;

namespace API.Services
{
    public class OwnerTypeService : IOwnerTypeService
    {
        private readonly IRepository<OwnerTypeModel> _repository;
        private readonly IMapper mapper;

        public OwnerTypeService(IRepository<OwnerTypeModel> ownerTypeRepository, IMapper mapper)
        {
            _repository = ownerTypeRepository;
            this.mapper = mapper;
        }

        public OwnerType Create(OwnerTypeInsertModel ownerType)
        {
            if (ownerType == null || !ownerType.IsValid())
            {
                throw new ArgumentException(nameof(ownerType));
            }

            var result = _repository.Add(mapper.Map<OwnerTypeModel>(ownerType));
            return mapper.Map<OwnerType>(result);
        }

        public bool DeleteById(int id)
        {
            return _repository.DeleteById(id);
        }

        public IEnumerable<OwnerType> GetAll()
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

        OwnerType IOwnerTypeService.Update(OwnerType ownerType)
        {
            if (ownerType == null || !ownerType.IsValid())
            {
                throw new ArgumentException(nameof(ownerType));
            }

            var result = _repository.Update(mapper.Map<OwnerTypeModel>(ownerType));
            return mapper.Map<OwnerType>(result);
        }
    }
}