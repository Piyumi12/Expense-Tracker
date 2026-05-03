using Expense_Tracker.Models; 
using Microsoft.EntityFrameworkCore; 


var builder = WebApplication.CreateBuilder(args); 
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080"; 
//builder.WebHost.UseUrls($"http://0.0.0.0:{port}");


// Add services to the container.
 builder.Services.AddControllersWithViews();
  //dependancy Injection 
 
  builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));
   var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DevConnection"); 
   builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString)); 
   //Register Syncfusion license 
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBMAY9C3t2UFhhQlJBfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hTX5QdEViXX9XcnFUT2dV"); 
   var app = builder.Build();

   // Configure the HTTP request pipeline. 
//if (!app.Environment.IsDevelopment()) { 
  //  app.UseExceptionHandler("/Home/Error"); 

//} 
app.UseDeveloperExceptionPage();
app.UseStaticFiles(); 

app.UseRouting(); 

app.UseAuthorization(); 
app.MapControllerRoute( name: "default", pattern: "{controller=Dashboard}/{action=Index}/{id?}");
 app.Run();
