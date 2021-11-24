using PlatformService.Models;
using Serilog;
using Serilog.Core;

namespace PlatformService.Data
{
    public static class SeedDb
    {
        public static void PrepPopulation(WebApplication app)
        {
            using (var serviceScope = app.Services.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }
        private static void SeedData(AppDbContext context)
        {
            if (!context.Platforms.Any())
            {
                Log.Information("No Data Found, Started Seed Data");

                context.Platforms.AddRange(
                    new Platform()
                    {
                        Name = "Dotnet",
                        Publisher = "Microsoft",
                        Cost = "Free"
                    },
                    new Platform()
                    {
                        Name = "SQL SE",
                        Publisher = "Microsoft",
                        Cost = "Free"
                    },
                    new Platform()
                    {
                        Name = "Kubernetes",
                        Publisher = "CNCF",
                        Cost = "Free"
                    }
                );
                context.SaveChanges();
            }
            else
            {
                Log.Information("Data already exist -- Ignore Seed Data");
            }
        }
    }
}