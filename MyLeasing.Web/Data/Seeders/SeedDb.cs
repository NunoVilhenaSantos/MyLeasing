using Microsoft.AspNetCore.Identity;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Data.DataContexts;
using MyLeasing.Web.Helpers;

namespace MyLeasing.Web.Data.Seeders;

public class SeedDb
{
    public SeedDb(
        IUserHelper userHelper,
        DataContext dataContext
        // UserManager<User> userManager,
        // RoleManager<IdentityRole> roleManager
    )
    {
        _userHelper = userHelper;
        _dataContext = dataContext;
        // _userManager = userManager;
        // _roleManager = roleManager;
    }


    public async Task SeedAsync()
    {
        await _dataContext.Database.EnsureCreatedAsync();


        var user = await CheckUserAsync(
            "Juan", "Zuluaga",
            email:
            "admin@disto_tudo_e_que_rouba_a_descarada.com",
            phoneNumber: "123456789", role: "Admin",
            document: "document's", address: "Calle Luna",
            password: "123456"
        );


        // await CheckRolesAsync(user);

        // if (_dataContext.Owners.Any()) return;

        AddOwners(
            "Juan", "Zuluaga", "Calle Luna", user);
        AddOwners(
            "Joaquim", "Alvenaria", "Calle Sol", user);
        AddOwners(
            "Alberto", "Domingues", "Calle Luna", user);
        AddOwners(
            "Mariana", "Alvarez", "Calle Sol", user);
        AddOwners(
            "Lucia", "Liu", "Calle Luna", user);
        AddOwners(
            "Renee", "Arriaga", "Calle Sol", user);
        AddOwners(
            "Marcia", "Zuluaga", "Calle Luna", user);
        AddOwners(
            "Ernesto", "Guevara", "Calle Sol", user);
        AddOwners(
            "El", "Che", "Calle Luna", user);
        AddOwners(
            "Claudia", "Arroz", "Calle Sol", user);
    }


    private async Task<User> CheckUserAsync(
        string firstName = "Nuno", string lastName = "Santos",
        string userName = "admin@disto_tudo_e_que_rouba_a_descarada.com",
        string email = "admin@disto_tudo_e_que_rouba_a_descarada.com",
        string phoneNumber = "123456789", string role = "Admin",
        string document = "document", string address = "address",
        string password = "123456")
    {
        var user = await _userHelper.GetUserByEmailAsync(email);

        switch (user)
        {
            case null:
            {
                user = new User
                {
                    Document = document,
                    FirstName = firstName,
                    LastName = lastName,
                    Address = address,
                    UserName = userName,
                    Email = email,
                    PhoneNumber = phoneNumber
                };

                var result = await _userHelper.AddUserAsync(user, password);

                if (result != IdentityResult.Success)
                    throw new InvalidOperationException(
                        "Could not create the user in Seeder");
                break;
            }
        }

        return user;
    }

    private async Task<IdentityResult?> CheckRolesAsync(User user)
    {
        return await _roleManager.FindByIdAsync(user.Id) switch
        {
            null => await CreateRoleAsync(user.UserName),
            _ => null
        };
    }


    private async Task<IdentityResult> CreateRoleAsync(string? admin)
    {
        return await _roleManager.CreateAsync(new IdentityRole("Admin"));
        throw new NotImplementedException();
    }


    private void AddOwners(
        string firstName, string lastName, string address, User user)
    {
        _dataContext.Owners.Add(new Owner
            {
                Document = _random.Next(100000, 999999999).ToString(),
                FirstName = firstName,
                LastName = lastName,
                FixedPhone = _random.Next(1000000, 99999999).ToString(),
                CellPhone = _random.Next(1000000, 99999999).ToString(),
                Address = address + ", " + _random.Next(1, 9999),
                User = user
            }
        );

        _dataContext.SaveChanges();
    }


    #region Attributes

    private readonly Random _random = new();
    private readonly DataContext _dataContext;

    private readonly IUserHelper _userHelper;

    // private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    #endregion
}