using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyEcommerceBackend.Models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string server = _configuration["DBServer"] ?? "localhost";
                string database = _configuration["DB"] ?? "MyDatabase";
                string username = _configuration["DBLogin"] ?? "username";
                string password = _configuration["DBPW"] ?? "password";

                optionsBuilder.UseSqlServer($"Server={server};Database={database};User Id={username};Password={password};");
            }
        }
    }
}
