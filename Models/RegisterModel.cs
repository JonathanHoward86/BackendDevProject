using Microsoft.AspNetCore.Identity;

namespace MyEcommerceBackend.Models
{
    public class RegisterModel // Model for user registration.
    {
        public string? Email { get; set; } // Optional email property.
        public string? Password { get; set; } // Optional password property.
        public string? ConfirmPassword { get; set; } // Optional confirm password property.
    }
}