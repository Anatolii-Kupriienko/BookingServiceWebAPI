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

        public bool Delete(RealEstateTypeModel model)
        {
            var result = _context.Remove(model);
            _context.SaveChanges();
            return result.State == Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public bool DeleteById(int id)
        {
            var model = GetById(id);
            if (model is not null)
            {
                return Delete(model);
            }
            return false;
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