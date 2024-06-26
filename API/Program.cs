using API.BusinessLogic;
using API.BusinessLogic.Services;
using API.DbAccess;
using API.DbAccess.Models;
using API.Services;
using API.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddDbContext<BookingDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookingService")!);
});
builder.Services.AddIdentity<UserModel, IdentityRole>().AddEntityFrameworkStores<BookingDbContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;

    options.User.RequireUniqueEmail = true;
});
builder.Services.AddScoped<IRepository<RealEstateModel>, RealEstateRepository>();
builder.Services.AddScoped<IRepository<OwnerTypeModel>, OwnerTypeRepository>();
builder.Services.AddScoped<IRepository<RealEstateTypeModel>, RealEstateTypeRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRealEstateService, RealEstateService>();
builder.Services.AddScoped<IRealEstateTypeService, RealEstateTypeService>();
builder.Services.AddScoped<IOwnerTypeService, OwnerTypeService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IMapper>(sp => new Mapper(new MapperConfiguration(conf => { conf.AddProfile<AutomapperProfile>(); })));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await SeedData.EnsurePopulated(app); // this is used only for testing/demonstration since this is a demo project
app.Run();
