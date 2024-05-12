using API.DbAccess.Models;

namespace API.DbAccess
{
    public class RealEstateTypeRepository : IRepository<RealEstateTypeModel>
    {
        private readonly BookingDbContext _context;

        public RealEstateTypeRepository(BookingDbContext context)
        {
            _context = context;
        }

        public void Add(RealEstateTypeModel model)
        {
            _context.Add(model);
            _context.SaveChanges();
        }

        public void Delete(RealEstateTypeModel model)
        {
            _context.Remove(model);
            _context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var model = GetById(id);
            if (model is not null)
            {
                Delete(model);
            }
        }

        public IEnumerable<RealEstateTypeModel> Get()
        {
            return _context.RealEstateTypes;
        }

        public RealEstateTypeModel? GetById(int id)
        {
            return _context.RealEstateTypes.Find(id);
        }

        public void Update(RealEstateTypeModel model)
        {
            _context.Update(model);
            _context.SaveChanges();
        }
    }
}