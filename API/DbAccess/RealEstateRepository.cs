using API.DbAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DbAccess
{
    public class RealEstateRepository : IRepository<RealEstateModel>
    {
        private readonly BookingDbContext _context;

        public RealEstateRepository(BookingDbContext context)
        {
            _context = context;
        }

        public RealEstateModel Add(RealEstateModel model)
        {
            var result = _context.Add(model);
            _context.SaveChanges();
            return result.Entity;
        }

        public bool Delete(RealEstateModel model)
        {
            var result = _context.Remove(model);
            _context.SaveChanges();
            return result.State == EntityState.Deleted;
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

        public IEnumerable<RealEstateModel> Get()
        {
            return _context.RealEstate
            .Include(x => x.Type).Include(x => x.OccupiedBy)
            .Include(x => x.Owner).ThenInclude(x => x.OwnerType);
        }

        public RealEstateModel? GetById(int id)
        {
            return _context.RealEstate.Include(x => x.Type).Include(x => x.OccupiedBy)
            .Include(x => x.Owner).ThenInclude(x => x.OwnerType)
            .FirstOrDefault(x => x.Id == id);
        }

        public RealEstateModel Update(RealEstateModel model)
        {
            var result = _context.Update(model);
            _context.SaveChanges();
            return result.Entity;
        }
    }
}