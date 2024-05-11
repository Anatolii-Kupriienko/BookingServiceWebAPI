using API.DbAccess;
using API.DbAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.Utils
{
    public static class SeedData
    {

        private const string AdminUser = "Admin";

        private const string AdminPassword = "123123";


        public static async Task EnsurePopulated(IApplicationBuilder app)
        {
            BookingDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<BookingDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            var user = await EnsureUsersPopulated(context, app);

            if (!context.RealEstate.Any())
            {

                context.RealEstateTypes.AddRange(
                    new RealEstateTypeModel
                    {
                        Name = "House",
                    },
                    new RealEstateTypeModel
                    {
                        Name = "Apartment",
                    },
                    new RealEstateTypeModel
                    {
                        Name = "Hotel room",
                    }
                );

                context.SaveChanges();

                var typeId = context.RealEstateTypes.FirstOrDefault()!.Id;

                context.RealEstate.AddRange(
                    new RealEstateModel
                    {
                        Location = "Sofia",
                        SizeSquareMeters = 100,
                        PricePerNight = 100,
                        RealEstateTypeId = typeId,
                        OwnerId = user.Id,
                        IsVacant = true,

                    },
                    new RealEstateModel
                    {
                        Location = "Plovdiv",
                        SizeSquareMeters = 80,
                        PricePerNight = 80,
                        RealEstateTypeId = typeId,
                        OwnerId = user.Id,
                        IsVacant = true,
                    },
                    new RealEstateModel
                    {
                        Location = "Varna",
                        SizeSquareMeters = 120,
                        PricePerNight = 120,
                        RealEstateTypeId = typeId,
                        OwnerId = user.Id,
                        IsVacant = true,
                    }
                );
                context.SaveChanges();
            }

        }

        private static async Task<UserModel> EnsureUsersPopulated(BookingDbContext context, IApplicationBuilder app)
        {
            if (!context.OwnerTypes.Any())
            {
                context.OwnerTypes.AddRange(
                    new OwnerTypeModel
                    {
                        Type = "Individual",
                    },
                    new OwnerTypeModel
                    {
                        Type = "Hotel",
                    }
                );
                context.SaveChanges();
            }

            var ownerTypeId = context.OwnerTypes.First().Id;

            UserManager<UserModel> userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<UserModel>>();
            UserModel user = await userManager.FindByNameAsync(AdminUser);
            if (user is null)
            {
                user = new UserModel()
                {
                    UserName = AdminUser,
                    Email = "admin@example.com",
                    OwnerTypeId = ownerTypeId
                };

                await userManager.CreateAsync(user, AdminPassword);
                user = await userManager.FindByNameAsync(AdminUser);
            }

            return user;
        }
    }
}