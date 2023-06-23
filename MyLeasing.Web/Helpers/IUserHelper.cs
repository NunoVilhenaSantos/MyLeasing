using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Models;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace MyLeasing.Web.Helpers;

public interface IUserHelper
{
    Task<User> GetUserByIdAsync(string id);


    Task<User> GetUserByEmailAsync(string email);


    Task<IdentityResult> AddUserAsync(User user, string password);


    Task CheckRoleAsync(string roleName);


    Task<SignInResult> LoginAsync(LoginViewModel model);


    Task LogOutAsync();


    Task<IActionResult> LogoutAsync();


    Task<IdentityResult> UpdateUserAsync(User user);


    Task<IdentityResult> ChangePasswordAsync(
        User user, string oldPassword, string newPassword);


    Task<IdentityResult> AddUserToRoleAsync(User user, string roleName);


    Task<IdentityResult> RemoveUserFromRoleAsync(User user, string roleName);


    Task<bool> IsUserInRoleAsync(User user, string roleName);
}