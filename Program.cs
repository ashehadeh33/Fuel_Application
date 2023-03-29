using Microsoft.EntityFrameworkCore;
using qenergy.Data;
using qenergy.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<AccountService>();


// this registers PizzaContext with ASP.NET Core's dependency injection system
// Specifies that Pizza Context will use the SQLite database provider
// Defines a SQLite connection string that points to a local file, ContosoPizza.db
//builder.Services.AddSqlite<PizzaContext>("Data Source=ContosoPizza.db");
var connection = String.Empty;
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
}
else
{
    connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
}

builder.Services.AddDbContext<qEnergyContext>(options =>
    options.UseSqlServer(connection));

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// CreateDbIfNotExists method call
// added during Database seeding
app.CreateDbIfNotExists();

app.Run();
