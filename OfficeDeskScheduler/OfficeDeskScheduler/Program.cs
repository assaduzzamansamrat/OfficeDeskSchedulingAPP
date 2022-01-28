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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();


