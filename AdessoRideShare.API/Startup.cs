using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdessoRideShare.Core.Repositories;
using AdessoRideShare.Core.Services;
using AdessoRideShare.Core.UnitOfWork;
using AdessoRideShare.Data.Contexts;
using AdessoRideShare.Data.Repositories;
using AdessoRideShare.Data.UnitOfWork;
using AdessoRideShare.Service.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace AdessoRideShare.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<,>), typeof(AdessoRideShare.Service.Service.Service<,>));
            services.AddScoped(typeof(ITravelService), typeof(AdessoRideShare.Service.Service.TravelService));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<AdessoDbContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName: "rideShareDatabase");
            });

            //services.UseCustomValidationResponse();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("AdessoRideShareAPI", new OpenApiInfo { Title = "Adesso RideShare API", Version = "v1" });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/AdessoRideShareAPI/swagger.json", "Adesso RideShare API");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
