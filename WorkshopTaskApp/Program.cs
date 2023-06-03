using Microsoft.AspNetCore.Authentication.Cookies;
using WorkshopTaskApp.Bussniss.BussiunessExtintions;
using WorkshopTaskApp.Repository.Extintions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
 .AddCookie(opt =>

 {

     opt.Cookie.Name = "UserData";
     //opt.LoginPath = "/Account/Login";
     opt.AccessDeniedPath = "/Product";
     opt.ExpireTimeSpan = TimeSpan.FromMinutes(60);
     opt.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
     opt.SlidingExpiration = true;

 });
InfrastractureExtintions.ConfigureDataBase(builder.Services, builder.Configuration.GetConnectionString("DefaultConnectionString"));
InfrastractureExtintions.ConfigureServises(builder.Services);
BussiunessExtintions.ConfigureServises(builder.Services);
var app = builder.Build();


InfrastractureExtintions.CreateDbIfNotExists(app);
// Configure the HTTP request pipeline.
app.UseStaticFiles();
app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Product}/{action=Index}/{id?}");
});

app.Run();
