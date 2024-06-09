
using Microsoft.EntityFrameworkCore;
using SWD.NextIntern.API.Filters;
using SWD.NextIntern.Repository;
using SWD.NextIntern.Repository.Persistence;
using SWD.NextIntern.Service;
using SWD.NextIntern.Service.Auth.ForgotPassword;
using SWD.NextIntern.Service.Auth.SignIn;
using SWD.NextIntern.Service.Auth.SignUp;
using SWD.NextIntern.Service.Common.Configuration;


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
            services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers(otp =>
            {
                otp.Filters.Add<ExceptionFilter>();
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("https:api-gateway.nextintern.tech", "https://localhost:7205")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDistributedMemoryCache();
            services.AddEndpointsApiExplorer();

            services.AddService(Configuration);
            services.AddRepository(Configuration);
            services.ConfigureApplicationSecurity(Configuration);
            services.AddScoped<SignUpCommandHandler>();
            services.AddScoped<SignInQueryHandler>();
            services.AddTransient<ForgotPasswordCommandHandler>();
            services.AddScoped<ForgotPasswordCommandHandler>();
            services.AddControllersWithViews();

            services.AddRepository(Configuration);
            services.AddService(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            app.UseCors("AllowSpecificOrigin");

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

