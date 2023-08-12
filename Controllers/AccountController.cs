using Microsoft.AspNetCore.Mvc;
using MyEcommerceBackend.Models;

namespace MyEcommerceBackend.Controllers
{
    [ApiController] // Specifies that this class is a controller and uses API behavior.
    [Route("api/[controller]")] // Defines the route template for this controller.
    public class AccountController : ControllerBase // Inherits ControllerBase for common API controller functionality.
    {
        // Dependency injection, constructor, etc. - Here is where you'll inject any dependencies.

        [HttpPost("Register")] // Specifies that this action responds to HTTP POST requests at the "/Register" path.
        public IActionResult Register(RegisterModel model) // Model binding automatically maps data from HTTP requests to method parameters.
        {
            // TODO: Implement registration logic here
            // You may need to create the RegisterModel class with the required properties.

            return Ok(); // Return a success response (you'll likely want to customize this).
        }
    }
}
