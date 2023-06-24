using Microsoft.AspNetCore.Mvc;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Helpers;
using MyLeasing.Web.Models;

namespace MyLeasing.Web.Controllers;

public class AccountController : Controller
{
    private readonly IUserHelper _userHelper;


    public AccountController(IUserHelper userHelper)
    {
        _userHelper = userHelper;
    }


    // GET
    // [HttpGet]
    // public IActionResult Index()
    // {
    //     return View();
    // }


    // GET
    [HttpGet]
    public IActionResult Login()
    {
        if (User.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");

        return View();
    }


    // POST
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _userHelper.LoginAsync(loginViewModel);

            if (result.Succeeded)
                return Request.Query.Keys.Contains("ReturnUrl")
                    ? Redirect(Request.Query["ReturnUrl"].ToString())
                    : RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError(
            string.Empty, "Failed to login.");
        return View(loginViewModel);
    }


    // GET
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        // var result = await _userHelper.LogoutAsync()
        //     .WaitAsync(TimeSpan.FromMinutes(1));


        await _userHelper.LogoutAsync();
        return RedirectToAction("Index", "Home");
        // return View();
    }


    // GET
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }


    // POST
    [HttpPost]
    public async Task<IActionResult> Register(
        RegisterNewUserViewModel registerNewUserViewModel)
    {
        if (ModelState.IsValid)
        {
            var user = await _userHelper.GetUserByEmailAsync(
                registerNewUserViewModel.Username);

            if (user == null)
            {
                user = new User
                {
                    Document = registerNewUserViewModel.Document,
                    FirstName = registerNewUserViewModel.FirstName,
                    LastName = registerNewUserViewModel.LastName,
                    Address = registerNewUserViewModel.Address,
                    PhoneNumber = registerNewUserViewModel.PhoneNumber,
                    UserName = registerNewUserViewModel.Username,
                    Email = registerNewUserViewModel.Username
                };


                var result = await _userHelper.AddUserAsync(
                    user, registerNewUserViewModel.Password);

                if (result.Succeeded)
                {
                    var loginViewModel = new LoginViewModel
                    {
                        Password = registerNewUserViewModel.Password,
                        RememberMe = false,
                        Username = registerNewUserViewModel.Username
                    };

                    var result2 = await _userHelper.LoginAsync(loginViewModel);

                    if (result2.Succeeded)
                        return RedirectToAction("Index", "Home");

                    ModelState.AddModelError(string.Empty,
                        "The User couldn't be logged.");
                    return View(registerNewUserViewModel);
                }

                ModelState.AddModelError(string.Empty,
                    "The User couldn't be created.");
                return View(registerNewUserViewModel);
            }

            ModelState.AddModelError(string.Empty,
                "User with this email already exists.");
            // return View(registerNewUserViewModel);
            return RedirectToAction("Login", "Account");
        }

        ModelState.AddModelError(string.Empty,
            "Tem de preencher os campos!");
        return View(registerNewUserViewModel);
    }


    // GET
    [HttpGet]
    public IActionResult NotAuthorized()
    {
        return View();
    }


    // GET
    [HttpGet]
    public async Task<IActionResult> ChangeUser()
    {
        var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);

        if (user == null) return View();

        var changeUserViewModel = new ChangeUserViewModel
        {
            // Address = user.Address,
            // Document = user.Document,
            FirstName = user.FirstName,
            LastName = user.LastName,
            // PhoneNumber = user.PhoneNumber
        };

        return View(changeUserViewModel);
    }


    // POST
    [HttpPost]
    public async Task<IActionResult> ChangeUser(
        ChangeUserViewModel changeUserViewModel)
    {
        if (!ModelState.IsValid)
            return View(changeUserViewModel);

        var user = await _userHelper.GetUserByEmailAsync(
            User.Identity.Name);

        if (user == null)
        {
            ModelState.AddModelError(string.Empty,
                "User not found.");
            return View(changeUserViewModel);
        }

        // user.Address = changeUserViewModel.Address;
        // user.Document = changeUserViewModel.Document;
        user.FirstName = changeUserViewModel.FirstName;
        user.LastName = changeUserViewModel.LastName;
        // user.PhoneNumber = changeUserViewModel.PhoneNumber;

        var result = await _userHelper.UpdateUserAsync(user);

        if (result.Succeeded)
        {
            ViewBag.UserMessage = "User Updated!";
            return View(changeUserViewModel);
            // return RedirectToAction("Index", "Home");
        }


        var errorMessage = result.Errors.FirstOrDefault()?.Description;
        ModelState.AddModelError(string.Empty,
            errorMessage ?? "User couldn't be updated.");

        return View(changeUserViewModel);
    }


    // GET
    [HttpGet]
    public IActionResult ChangePassword()
    {
        return View();
    }


    // POST
    [HttpPost]
    public async Task<IActionResult> ChangePassword(
        ChangePasswordViewModel changePasswordViewModel)
    {
        if (!ModelState.IsValid)
            return View(changePasswordViewModel);

        var user = await _userHelper.GetUserByEmailAsync(
            User.Identity.Name);

        if (user == null)
        {
            ModelState.AddModelError(string.Empty,
                "User not found.");
            return View(changePasswordViewModel);
        }


        var result = await _userHelper.ChangePasswordAsync(
            user, changePasswordViewModel.OldPassword,
            changePasswordViewModel.NewPassword);

        if (result.Succeeded)
        {
            ViewBag.UserMessage = "Password changed!";

            return View(changePasswordViewModel);
            // return RedirectToAction("ChangeUser");
            // return RedirectToAction("Index", "Home");
        }


        var errorMessage = result.Errors.FirstOrDefault()?.Description;
        ModelState.AddModelError(string.Empty,
            errorMessage ?? "User couldn't be updated.");

        return View(changePasswordViewModel);
    }
}