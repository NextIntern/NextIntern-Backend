using SWD.NextIntern.API.Filters;
using SWD.NextIntern.Repository;
using SWD.NextIntern.Service;
using SWD.NextIntern.Service.Common.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SWD.NextIntern.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(otp =>
            {
                otp.Filters.Add<ExceptionFilter>();
            });
            //    .AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            //});

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            //Register layer
            services.AddRepository(Configuration);
            services.AddService(Configuration);

            //Register configuration
            services.ConfigureApplicationSecurity(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
