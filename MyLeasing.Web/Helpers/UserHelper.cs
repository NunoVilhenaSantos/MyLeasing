using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Models;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace MyLeasing.Web.Helpers;

public class UserHelper : IUserHelper
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;


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


    public async Task<User> GetUserByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }


    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }


    public Task<IdentityResult> AddUserAsync(User user, string password)
    {
        return _userManager.CreateAsync(user, password);
    }


    public async Task CheckRoleAsync(string roleName)
    {
        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
            await _roleManager.CreateAsync(new IdentityRole
            {
                Name = roleName
            });
    }


    public async Task<SignInResult>
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


    public async Task<IActionResult> LogoutAsync()
    {
        // Sign out the user
        var signOutTask = _signInManager.SignOutAsync();
        await signOutTask;

        if (signOutTask.IsCompletedSuccessfully)
            // Handle successful sign-out
            // For example, redirect to a sign-out confirmation page
            return new RedirectToActionResult("LogoutConfirmation",
                "YourControllerName", null);

        // Handle sign-out failure
        // For example, display an error message
        const string errorMessage = "Failed to sign out.";
        return new RedirectToActionResult("LogoutConfirmation",
            "YourControllerName",
            new { error = errorMessage });
    }


    public async Task<IdentityResult> UpdateUserAsync(User user)
    {
        return await _userManager.UpdateAsync(user);
    }


    public async Task<IdentityResult> ChangePasswordAsync(
        User user, string oldPassword, string newPassword)
    {
        return await _userManager.ChangePasswordAsync(
            user, oldPassword, newPassword);
    }


    public async Task<IdentityResult> AddUserToRoleAsync(
        User user, string roleName)
    {
        return await _userManager.AddToRoleAsync(user, roleName);
    }


    public async Task<IdentityResult> RemoveUserFromRoleAsync(
        User user, string roleName)
    {
        return await _userManager.RemoveFromRoleAsync(user, roleName);
    }


    public async Task<bool> IsUserInRoleAsync(User user, string roleName)
    {
        return await _userManager.IsInRoleAsync(user, roleName);
    }
}