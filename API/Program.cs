using API.DbAccess;
using API.DbAccess.Models;
using API.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!");

await SeedData.EnsurePopulated(app); // this is used only for testing/demonstration since this is a demo project
app.Run();
