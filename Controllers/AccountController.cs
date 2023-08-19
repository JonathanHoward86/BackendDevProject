using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyEcommerceBackend.Models;
using System.Net;
using System.Net.Mail;

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
                        return RedirectToAction("RegisterSuccess", "View");
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
            return View(model);
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
                        return RedirectToAction("LoginSuccess", "View");
                    }
                }
                ModelState.AddModelError("", "Invalid login attempt");
            }
            else
            {
                ModelState.AddModelError("", "Email and Password must not be null");
            }
            return View(model);
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
                    SendEmail(model.Email, "Reset Password", emailBody);
                    return RedirectToAction("ResetPasswordEmailSent", "View");
                }

                ModelState.AddModelError("", "Email not found");
            }

            return View(model);
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
                    SendEmail(model.Email, "Retrieve Username", emailBody);
                    return RedirectToAction("UsernameEmailSent", "View");
                }

                ModelState.AddModelError("", "Email not found");
            }

            return View(model);
        }


        private void SendEmail(string email, string subject, string body)
        {
            string smtpEmail = _configuration["SmtpEmail"] ?? throw new InvalidOperationException("SmtpEmail must be configured");
            string smtpPassword = _configuration["SmtpPassword"] ?? throw new InvalidOperationException("SmtpPassword must be configured");

            if (string.IsNullOrEmpty(smtpEmail) || string.IsNullOrEmpty(smtpPassword))
            {
                // Handle error or log an issue
                return;
            }

            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(smtpEmail, smtpPassword);

                using (MailMessage message = new MailMessage())
                {
                    message.From = new MailAddress(smtpEmail);
                    message.To.Add(email);
                    message.Subject = subject;
                    message.Body = body;

                    client.Send(message);
                }
            }
        }
    }
}
