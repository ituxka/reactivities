using Application;
using Application.Common.Interfaces;
using API.Common;
using FluentValidation.AspNetCore;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private const string CorsPolicy = "CorsPolicy";

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services.AddControllers()
                .AddFluentValidation(fv => { fv.RegisterValidatorsFromAssemblyContaining<IApplicationDbContext>(); });

            services.AddCors(opt =>
            {
                opt.AddPolicy(CorsPolicy, policy =>
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomExceptionHandler();

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(CorsPolicy);

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
