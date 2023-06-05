using Microsoft.AspNetCore.Identity;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Helpers;

public class UserHelper : IUserHelper
{
    #region Constructor

    public UserHelper(UserManager<User> userManager,
        SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    #endregion


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

    #region MyRegion

    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    #endregion
}