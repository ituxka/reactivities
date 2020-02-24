using System;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API {
    public class Program {
        public static void Main (string[] args) {
            var host = CreateHostBuilder (args).Build ();

            using var scope = host.Services.CreateScope ();
            SetupDatabaseMigrations (scope);

            host.Run ();
        }

        private static IHostBuilder CreateHostBuilder (string[] args) =>
            Host.CreateDefaultBuilder (args)
            .ConfigureWebHostDefaults (webBuilder => { webBuilder.UseStartup<Startup> (); });

        private static void SetupDatabaseMigrations (IServiceScope scope) {
            var services = scope.ServiceProvider;
            try {
                var context = services.GetRequiredService<ApplicationDbContext> ();
                context.Database.Migrate ();
                ApplicationDbContextSeed.SeedData (context);
            } catch (Exception e) {
                var logger = services.GetRequiredService<ILogger<Program>> ();
                logger.LogError (e, "An error occurred during migration");
            }
        }
    }
}
