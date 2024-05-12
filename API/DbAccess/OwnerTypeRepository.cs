using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public void Add(OwnerTypeModel model)
        {
            _context.Add(model);
            _context.SaveChanges();
        }

        public void Delete(OwnerTypeModel model)
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

        public IEnumerable<OwnerTypeModel> Get()
        {
            return _context.OwnerTypes;
        }

        public OwnerTypeModel? GetById(int id)
        {
            return _context.OwnerTypes.Find(id);
        }

        public void Update(OwnerTypeModel model)
        {
            _context.Update(model);
            _context.SaveChanges();
        }
    }
}