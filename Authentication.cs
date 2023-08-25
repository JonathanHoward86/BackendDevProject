using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            // Read the environment variables from Azure
            string server = Configuration["DBServer"] ?? throw new Exception("Server environment variable is not set.");
            string database = Configuration["DB"] ?? throw new Exception("Database environment variable is not set.");
            string username = Configuration["DBLogin"] ?? throw new Exception("Username environment variable is not set.");
            string password = Configuration["DBPW"] ?? throw new Exception("Password environment variable is not set.");

            // Construct the connection string
            string connectionString = $"Server=tcp:{server},1433;Initial Catalog={database};Persist Security Info=False;User ID={username};Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            // Add the DbContext using the connection string
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Registers controller services
            services.AddControllers();

            // Add MVC support with Views and Controllers
            services.AddControllersWithViews();

            // Adds Identity services for authentication and authorization
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
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
            // Enables Swagger in the development environment
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(); // Enables Swagger UI for API documentation browsing
            }

            // Adds routing middleware
            app.UseRouting();

            // Adds authentication and authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles(); // Serve static files like CSS, JavaScript, etc.

            // Enable endpoint routing and configure the default route
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=View}/{action=Login}/{id?}"); // Defines the default route
                endpoints.MapControllers();
            });
        }
    }
}