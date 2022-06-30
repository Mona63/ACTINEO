using ACTINEO.Core;
using ACTINEO.Infrastructure;
using ACTINEO.Infrastructure.Repositories;
using ACTINEO.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ACTINEO.Web {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private const string _corsPolicyName = "CorsPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<CarManagementContext>(options =>
                                      options.UseSqlite(Configuration.GetConnectionString("MainConnectionString")));

            services.AddScoped<ICarAdvertRepository, CarAdvertRepository>();
            services.AddScoped<ICarAdvertService, CarAdvertService>();

            services.AddControllers();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ACTINEO.Web", Version = "v1" });
            });

            services.AddCors(options => {
                options.AddPolicy(name: _corsPolicyName,
                    builder => {

                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ACTINEO.Web v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(_corsPolicyName);

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
