using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using API.DbAccess.Models;

namespace API.DbAccess
{
    // In a proper application, we would need to have everything in this folder in a separate project depending on the architecture used
    // For simplicity, we will keep it in the same project
    public class BookingDbContext : IdentityDbContext<UserModel>
    {
        public BookingDbContext(DbContextOptions options)
              : base(options)
        {
        }

        public DbSet<RealEstateModel> RealEstate { get; set; }
        public DbSet<OwnerTypeModel> OwnerType { get; set; }
        public DbSet<RealEstateTypeModel> RealEstateTypes { get; set; }
    }
}