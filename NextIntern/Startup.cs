using NextIntern.API.Configuration;
using NextIntern.API.Filters;
using NextIntern.Application;
using NextIntern.Application.Auth.SignIn;
using NextIntern.Application.Auth.SignUp;
using NextIntern.Infrastructure;

namespace NextIntern.API
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
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            //Register layer
            services.AddApplication(Configuration);
            services.AddInfrastructure(Configuration);
            services.ConfigureApplicationSecurity(Configuration);
            services.AddScoped<SignUpCommandHandler>();
            services.AddScoped<SignInQueryHandler>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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