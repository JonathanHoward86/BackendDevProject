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

        public IActionResult RegisterSuccess()
    {
        return View(); // Returns the RegisterSuccess view.
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

        public IActionResult LoginSuccess()
    {
        return View(); // Returns the LoginSuccess view.
    }

    public IActionResult ResetPassword()
    {
        return View(); // Returns the Register view.
    }

    public IActionResult ForgotUsername()
    {
        return View(); // Returns the Register view.
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

    public IActionResult ResetPasswordEmailSent()
    {
        return View(); // Returns the ResetPasswordEmailSent view.
    }

    public IActionResult ForgotUsernameEmailSent()
    {
        return View(); // Returns the ResetUsernameEmailSent view.
    }
}