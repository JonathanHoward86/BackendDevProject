using Microsoft.AspNetCore.Identity;

namespace MyEcommerceBackend.Models
{
    public class LoginModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}