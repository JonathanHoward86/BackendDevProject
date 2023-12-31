using System.ComponentModel.DataAnnotations;

namespace MyEcommerceBackend.Models
{
    public class ResetPasswordModel // Model to handle reset password request.
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; } // Required email property.
    }
}