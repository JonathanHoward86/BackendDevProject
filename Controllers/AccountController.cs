using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyEcommerceBackend.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MyEcommerceBackend.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Email != null && model.Password != null)
                {
                    var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction("RegisterSuccess");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email and Password must not be null");
                }
            }
            return View("Register", model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(); // Returns the Register view.
        }

        public IActionResult RegisterSuccess()
        {
            return View(); // Returns the RegisterSuccess view.
        }



        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (model.Email != null && model.Password != null)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("LoginSuccess");
                    }
                }
                ModelState.AddModelError("", "Invalid login attempt");
            }
            else
            {
                ModelState.AddModelError("", "Email and Password must not be null");
            }
            return View("Login", model);
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



        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Email == null) // Check if Email is null
                {
                    ModelState.AddModelError("", "Email must not be null");
                    return View(model);
                }

                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetUrl = Url.Action("ResetPasswordConfirm", "Account", new { token, email = model.Email }, Request.Scheme);
                    var emailBody = $"Please reset your password by clicking <a href='{resetUrl}'>here</a>.";
                    await SendEmail(model.Email, "Reset Password", emailBody);
                    return RedirectToAction("ResetPasswordEmailSent");
                }

                ModelState.AddModelError("", "Email not found");
            }

            return View("ResetPassword", model);
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



        [HttpPost("ForgotUsername")]
        public async Task<IActionResult> ForgotUsername(ForgotUsernameModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Email == null)
                {
                    ModelState.AddModelError("", "Email must not be null");
                    return View(model);
                }

                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var username = user.UserName;
                    var emailBody = $"Your username is: {username}";
                    await SendEmail(model.Email, "Retrieve Username", emailBody);
                    return RedirectToAction("ForgotUsernameEmailSent");
                }

                ModelState.AddModelError("", "Email not found");
            }

            return View("ForgotUsername", model);
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

        private async Task SendEmail(string email, string subject, string body)
        {
            string apiKey = Environment.GetEnvironmentVariable("SmtpAPI") ?? throw new InvalidOperationException("SmtpAPI must be configured");
            string fromEmail = Environment.GetEnvironmentVariable("SmtpFromEmail") ?? throw new InvalidOperationException("SmtpFromEmail must be configured");
            string fromName = Environment.GetEnvironmentVariable("SmtpFromName") ?? throw new InvalidOperationException("SmtpFromName must be configured");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, fromName);
            var to = new EmailAddress(email, "Faithful User");
            var msg = MailHelper.CreateSingleEmail(from, to, subject, body, body);
            var response = await client.SendEmailAsync(msg);
        }

        // GET action to show the reset password form
        [HttpGet]
        public IActionResult ResetPasswordConfirm(string token, string email)
        {
            var model = new ResetPasswordConfirmModel { Token = token, Email = email };
            return View("ResetPasswordConfirm", model);
        }

        // POST action to handle the form submission
        [HttpPost]
        public async Task<IActionResult> ResetPasswordConfirm(ResetPasswordConfirmModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
                    if (result.Succeeded)
                    {
                        // Password reset was successful
                        return RedirectToAction("ResetPasswordSuccess");
                    }
                    else
                    {
                        // Handle errors
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            return View("ResetPasswordConfirm", model);
        }
    }
}