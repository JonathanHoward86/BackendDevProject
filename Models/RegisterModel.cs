using Microsoft.AspNetCore.Identity;

namespace MyEcommerceBackend.Models
{
    public class RegisterModel : IdentityUser
    {
        // This class can be used for model binding during user registration.
        // It currently inherits from IdentityUser, so it includes all the standard Identity properties.
        // You may want to add additional properties specific to your registration process or use a different base class.
    }
}
