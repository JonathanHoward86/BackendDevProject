using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyEcommerceBackend.Models;

public class ViewController : Controller // Controller for handling user-facing views like Register, Login, etc.
{
    public IActionResult Register()
    {
        return View(); // Returns the Register view.
    }

    [HttpPost]
    public IActionResult Register(RegisterModel model)
    {
        return View(model); // Returns the Register view along with the model data.
    }

    public IActionResult Login()
    {
        return View(); // Returns the Login view.
    }

    [HttpPost]
    public IActionResult Login(LoginModel model)
    {
        return View(model); // Returns the Login view along with the model data.
    }

    [HttpPost]
    public IActionResult ResetPassword(ResetPasswordModel model)
    {
        return View(model); // Returns the ResetPassword view along with the model data.
    }

    [HttpPost]
    public IActionResult ForgotUsername(ForgotUsernameModel model)
    {
        return View(model); // Returns the ForgotUsername view along with the model data.
    }
}