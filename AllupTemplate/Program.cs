using AllupTemplate.DataAccessLayer;
using AllupTemplate.Interfaces;
using AllupTemplate.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options=>options.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddDbContext<AppDbContext>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ILayoutService,LayoutService>();
var app = builder.Build();

app.UseSession(); 
app.UseStaticFiles();
app.MapControllerRoute("default","{controller=Home}/{action=Index}/{id?}");

app.Run();
