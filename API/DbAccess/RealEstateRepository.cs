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

        public void Add(RealEstateModel model)
        {
            _context.Add(model);
            _context.SaveChanges();
        }

        public void Delete(RealEstateModel model)
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

        public void Update(RealEstateModel model)
        {
            _context.Update(model);
            _context.SaveChanges();
        }
    }
}