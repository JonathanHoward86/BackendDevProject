using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyEcommerceBackend.Models;

public class ViewController : Controller
{
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterModel model) // Removed 'async' and 'Task'
    {
        // Logic to call the AccountController's Register endpoint
        // and process the response can go here. You might use
        // client-side scripting (JavaScript) to handle form submission.

        return View(model);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginModel model) // Removed 'async' and 'Task'
    {
        // Logic to call the AccountController's Login endpoint
        // and process the response can go here.

        return View(model);
    }
}
