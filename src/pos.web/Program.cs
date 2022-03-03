using pos.core;
using pos.orders;
using pos.products;
using pos.users;
using pos.web.Services;

var builder = WebApplication
    .CreateBuilder(args);

var env = builder.Environment.EnvironmentName;
var isDevelopment = builder.Environment.IsDevelopment();

builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{env}.json", optional: true);

var connectionString = builder.Configuration.GetConnectionString("TenantConnection");

// Add services to the container.
builder.Services
    .AddInternalServices(builder.Configuration)
    .AddCoreServices(connectionString, isDevelopment, isDevelopment)
    .AddProductServices()
    .AddOrderServices()
    .AddUserServices()
    .AddControllersWithViews();

builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
