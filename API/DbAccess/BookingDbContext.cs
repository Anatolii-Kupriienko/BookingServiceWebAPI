using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using API.DbAccess.Models;

namespace API.DbAccess
{
    // In a proper application, we would need to have this in a separate project depending on the architecture used
    // For simplicity, we will keep it in the same project
    public class BookingDbContext : IdentityDbContext<UserModel>
    {
        public BookingDbContext(DbContextOptions options)
              : base(options)
        {
        }

        
        public DbSet<RealEstateModel> RealEstate { get; set; }
        public DbSet<OwnerTypeModel> OwnerTypes { get; set; }
        public DbSet<RealEstateTypeModel> RealEstateTypes { get; set; }
    }
}