using Expense_Tracker.Models;



using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 🌐 Render port binding
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Add services to the container
builder.Services.AddControllersWithViews();

// 🔐 Get PostgreSQL connection string from Render
var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DevConnection");

// ⚠️ Safety check (prevents silent crash)
if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Database connection string is missing!");
}

// 🐘 PostgreSQL DbContext (IMPORTANT CHANGE)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Register Syncfusion license
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
    "Mgo+DSMBMAY9C3t2UFhhQlJBfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hTX5QdEViXX9XcnFUT2dV"
);

var app = builder.Build();

// Configure HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}"
);

app.Run();