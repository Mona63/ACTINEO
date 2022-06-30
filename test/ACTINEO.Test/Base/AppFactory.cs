using ACTINEO.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace ACTINEO.Test.Base {

    public class AppFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class {

        protected override void ConfigureWebHost(IWebHostBuilder builder) {
            base.ConfigureWebHost(builder);

            builder.ConfigureServices(ConfigureServices);
        }

        protected virtual void ConfigureServices(IServiceCollection services) {
            // Create a new service provider.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Add a database context (CarManagementContext) using an in-memory 
            // database for testing.
            services.AddDbContext<CarManagementContext>(options => {
                options.UseInMemoryDatabase("InMemory");
                options.UseInternalServiceProvider(serviceProvider);
            });

            // Build the service provider.
            var sp = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database
            // context (CarManagementContext).
            using (var scope = sp.CreateScope()) {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<CarManagementContext>();

                // Ensure the database is created.
                db.Database.EnsureCreated();
            }
        }

    }

}