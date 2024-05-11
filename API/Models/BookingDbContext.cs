using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class BookingDbContext : IdentityDbContext<UserModel>
    {
        public BookingDbContext(DbContextOptions options)
              : base(options)
        {
        }

        
        public DbSet<RealEstate> RealEstate { get; set; }
        public DbSet<OwnerType> OwnerTypes { get; set; }
        public DbSet<RealEstateType> RealEstateTypes { get; set; }
    }
}