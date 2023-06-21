using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Models;


namespace MyLeasing.Web.Helpers;

public class UserHelper : IUserHelper
{
    #region Constructor

    public UserHelper(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        RoleManager<IdentityRole> roleManager
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    #endregion


    public async Task<User> GetUserByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }


    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email ?? string.Empty);
    }


    public Task<IdentityResult> AddUserAsync(User user, string password)
    {
        return _userManager.CreateAsync(user, password);
    }


    #region Attributes

    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    #endregion


    public async Task CheckRoleAsync(string roleName)
    {
        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            await _roleManager.CreateAsync(new IdentityRole
            {
                Name = roleName
            });
        }
    }


    public async Task<Microsoft.AspNetCore.Identity.SignInResult>
        LoginAsync(LoginViewModel model)
    {
        return await _signInManager.PasswordSignInAsync(
            model.Username,
            model.Password,
            model.RememberMe,
            false
        );
    }


    public async Task LogOutAsync()
    {
        await _signInManager.SignOutAsync();
    }


    // public Task<SignOutResult> LogoutAsync()
    // {
    //     // Sign out the user
    //     var result =
    //         _signInManager.SignOutAsync().ConfigureAwait(false).GetAwaiter();
    //
    //     if (result.IsCompleted)
    //     {
    //         // Handle successful sign-out
    //         // For example, redirect to a sign-out confirmation page
    //         return new Task<SignOutResult>(
    //             new RedirectToActionResult("LogoutConfirmation",
    //                 "YourControllerName", null));
    //     }
    //
    //     // Handle sign-out failure
    //     // For example, display an error message
    //     var errorMessage = "Failed to sign out.";
    //     return new RedirectToActionResult("LogoutConfirmation",
    //         "YourControllerName",
    //         new {error = errorMessage});
    // }


    public async Task<IActionResult> LogoutAsync()
    {
        // Sign out the user
        var signOutTask = _signInManager.SignOutAsync();
        await signOutTask;

        if (signOutTask.IsCompletedSuccessfully)
        {
            // Handle successful sign-out
            // For example, redirect to a sign-out confirmation page
            return new RedirectToActionResult("LogoutConfirmation",
                "YourControllerName", null);
        }

        // Handle sign-out failure
        // For example, display an error message
        const string errorMessage = "Failed to sign out.";
        return new RedirectToActionResult("LogoutConfirmation",
            "YourControllerName",
            new {error = errorMessage});
    }
}