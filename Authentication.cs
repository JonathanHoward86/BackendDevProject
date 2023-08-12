using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using MyEcommerceBackend.Models;

namespace MyEcommerceBackend
{
    public class Authentication
    {
        // Main method - entry point of the application
        public static void Main(string[] args)
        {
            // Creates and runs the web host
            CreateHostBuilder(args).Build().Run();
        }

        // Defines the host builder
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Configures the web host to use this class (Authentication) as the startup class
                    webBuilder.UseStartup<Authentication>();
                });

        // Constructor that accepts configuration settings
        public Authentication(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Configuration property
        public IConfiguration Configuration { get; }

        // ConfigureServices method - used to register application services
        public void ConfigureServices(IServiceCollection services)
        {
            // Registers controller services
            services.AddControllers();

            // Configures Identity and database context
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            // Adds Identity services for authentication and authorization
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Sets password and lockout options
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Configures API explorer and Swagger for API documentation
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        // Configure method - used to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enables Swagger in development environment
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Adds HTTPS redirection middleware
            app.UseHttpsRedirection();

            // Adds routing middleware
            app.UseRouting();

            // Adds authentication and authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            // Configures endpoint routing
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
