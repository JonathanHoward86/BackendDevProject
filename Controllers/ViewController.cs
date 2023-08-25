using Microsoft.AspNetCore.Mvc;

public class ViewController : Controller // Controller for handling user-facing views like Register, Login, etc.
{
    [HttpGet]
    public IActionResult Register()
    {
        return View(); // Returns the Register view.
    }

    public IActionResult RegisterSuccess()
    {
        return View(); // Returns the RegisterSuccess view.
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View(); // Returns the Login view.
    }

    public IActionResult LoginSuccess()
    {
        return View(); // Returns the LoginSuccess view.
    }

    [HttpGet]
    public IActionResult ResetPassword()
    {
        return View(); // Returns the ResetPassword view.
    }

    public IActionResult ResetPasswordEmailSent()
    {
        return View(); // Returns the ResetPasswordEmailSent view.
    }

    [HttpGet]
    public IActionResult ResetPasswordConfirm()
    {
        return View(); // Returns the ResetPasswordConfirm view.
    }

    public IActionResult ResetPasswordSuccess()
    {
        return View(); // Returns the ResetPasswordSuccess view.
    }

    [HttpGet]
    public IActionResult ForgotUsername()
    {
        return View(); // Returns the ForgotUsername view.
    }

    public IActionResult ForgotUsernameEmailSent()
    {
        return View(); // Returns the ForgotUsernameEmailSent view.
    }
}