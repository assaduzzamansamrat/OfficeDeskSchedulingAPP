using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.DataService;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<LoginDataService, LoginDataService>();
builder.Services.AddScoped<UserDataService, UserDataService>();
builder.Services.AddScoped<TeamDataService, TeamDataService>();
builder.Services.AddScoped<DeskDataService, DeskDataService>();
builder.Services.AddScoped<DeskBookingDataService, DeskBookingDataService>();

builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddSession();
builder.Services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);


var app = builder.Build();


builder.Services.AddDistributedMemoryCache();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();


