// created during Database seeding

using qenergy.Data;

namespace qenergy.Data;

public static class Extensions
{
    public static void CreateDbIfNotExists(this IHost host) // defined an an extension of IHost
    {
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<qEnergyContext>(); // a reference to the qEnergyContext service is created
                context.Database.EnsureCreated(); // EnsureCreated ensures the database exists. Will create a new db if one doesnt exist, passing the qEnergyContext as a parameter
                DbInitializer.Initialize(context); // DbInitializer.Initialize method is called, passing the qEnergyContext as a parameter
            }
        }
    }
}