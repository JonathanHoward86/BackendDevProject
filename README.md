# BackendDevProject

This is my ASP.NET program that uses my Azure Devops Pipeline and Self-Hosted Agent to CI/CD my code
I use an Azure Web App to host my code and store data in the Azure SQL Database connected to it.

The goal of this program is to ultimatley incorporate an OAuth2 authentication flow and to allow users to register as a user of the Web App for this purpose.

Eventually I'll expand out from there.

Prerequisites:

.NET 7.0 SDK
Entity Framework Core
Microsoft.AspNetCore.Authentication.JwtBearer
Microsoft.AspNetCore.Cors
Microsoft.AspNetCore.Identity.EntityFrameworkCore
Microsoft.AspNetCore.OpenApi
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Swashbuckle.AspNetCore

Getting Started:

Clone this repository to your local machine.
Make sure you have the required prerequisites installed.
Configure the database connection string in the Authentication class constructor within the MyEcommerceBackend namespace.
Build the project using the .NET CLI: dotnet build.
Run the application using the .NET CLI: dotnet run.
Usage:

API Endpoints:

POST /api/Account/Register: Register a new user account.
POST /api/Account/Login: Authenticate an existing user.

User-Facing Views:

GET /View/Register: User registration view.
POST /View/Register: User registration view with form data.
GET /View/Login: User login view.
POST /View/Login: User login view with form data.
POST /View/ResetPassword: Reset password view with form data.
POST /View/ForgotUsername: Forgot username view with form data.

Code Structure:

Authentication.cs: Main class for configuring the web host and application services.
AccountController.cs: Controller handling API endpoints related to user registration and login.
ViewController.cs: Controller handling user-facing views for registration, login, and other actions.
LoginModel.cs: Model for user login, including email and password properties.
RegisterModel.cs: Model for user registration, including email, password, and confirm password properties.
Login.cshtml and Register.cshtml: Razor views for user login and registration.

Configuration:

Configure the database connection string in the Authentication class constructor.
Adjust minimum password length and other validation rules in the RegisterModel class.

Acknowledgments

The automated test scripts in this repository were created by Jonathan Howard, an aspiring software developer with expertise in Agile Software Development and proficiency in various programming languages.
