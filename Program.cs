using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

//using NCCF_MVC_App.Models.NCCF_DatabaseContext;
var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<NCCF_MVC_AppContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("NCCF_MVC_AppContext") ?? throw new InvalidOperationException("Connection string 'NCCF_MVC_AppContext' not found.")));

builder.Services.AddDbContext<NCCF_MVC_App.Models.NCCF_DatabaseContext>(options => options.UseSqlServer("Server=DESKTOP-NC277NA\\SQLEXPRESS;Database=NCCF_Database;Trusted_Connection=True;MultipleActiveResultSets=true;"));

// Add services to the container.
builder.Services.AddControllersWithViews();

//Configure Session State
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "PersonBirthday",
    pattern: "{controller=Person}/{action=PersonBirthday}/{year}/{month}",
    constraints: new { year = @"2015|2016", month = @"\d{2}" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
