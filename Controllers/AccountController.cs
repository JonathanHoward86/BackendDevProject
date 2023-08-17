using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyEcommerceBackend.Models;

namespace MyEcommerceBackend.Controllers
{
    [ApiController] // Specifies that this class is a controller and uses API behavior.
    [Route("api/[controller]")] // Defines the route template for this controller.
    public class AccountController : ControllerBase // Inherits ControllerBase for common API controller functionality.
    {
        private readonly UserManager<IdentityUser> _userManager; // UserManager is used to manage users in a persistence store.
        private readonly SignInManager<IdentityUser> _signInManager; // SignInManager is used to manage user sign-in operations.

        // Constructor that takes UserManager and SignInManager as parameters, both are provided via dependency injection.
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterModel model) // Register endpoint to create a new user account.
        {
            if (ModelState.IsValid) // Validates the model.
            {
                if (model.Email != null && model.Password != null) // Checks if Email and Password are provided.
                {
                    var user = new IdentityUser { UserName = model.Email, Email = model.Email }; // Creates a new user object.
                    var result = await _userManager.CreateAsync(user, model.Password); // Attempts to create a new user.

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction("RegisterSuccess", "ViewController"); // Redirect to RegisterSuccess view
                    }
                    foreach (var error in result.Errors) // Adds errors to ModelState if creation fails.
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Email and Password must not be null"); // Error message for null Email or Password.
                }
            }
            return BadRequest(ModelState); // Returns a BadRequest response if the model is not valid.
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] LoginModel model) // Login endpoint to authenticate an existing user.
        {
            if (model.Email != null && model.Password != null) // Checks if Email and Password are provided.
            {
                var user = await _userManager.FindByEmailAsync(model.Email); // Finds the user by email.
                if (user != null) // If the user exists, attempts to sign in.
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("LoginSuccess", "ViewController"); // Redirect to LoginSuccess view
                    }
                    if (result.Succeeded) // If sign-in is successful, returns Ok response.
                    {
                        return Ok();
                    }
                }
                ModelState.AddModelError("", "Invalid login attempt"); // Error message for an invalid login attempt.
            }
            else
            {
                ModelState.AddModelError("", "Email and Password must not be null"); // Error message for null Email or Password.
            }
            return BadRequest(ModelState); // Returns a BadRequest response if the model is not valid.
        }
    }
}
