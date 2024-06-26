using API.DbAccess.Models;

namespace API.DbAccess
{
    public class OwnerTypeRepository : IRepository<OwnerTypeModel>
    {
        private readonly BookingDbContext _context;

        public OwnerTypeRepository(BookingDbContext context)
        {
            _context = context;
        }

        public OwnerTypeModel Add(OwnerTypeModel model)
        {
            var result = _context.Add(model);
            _context.SaveChanges();
            return result.Entity;
        }

        public bool Delete(OwnerTypeModel model)
        {
            var result = _context.Remove(model);
            _context.SaveChanges();
            return result.State == Microsoft.EntityFrameworkCore.EntityState.Detached;
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

        public IEnumerable<OwnerTypeModel> Get()
        {
            return _context.OwnerType;
        }

        public OwnerTypeModel? GetById(int id)
        {
            return _context.OwnerType.Find(id);
        }

        public OwnerTypeModel Update(OwnerTypeModel model)
        {
            var result = _context.Update(model);
            _context.SaveChanges();
            return result.Entity;
        }
    }
}