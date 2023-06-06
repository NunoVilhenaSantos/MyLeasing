using Microsoft.AspNetCore.Identity;
using MyLeasing.Web.Data.DataContexts;
using MyLeasing.Web.Data.Entities;
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
            "admin@disto_tudo_e_que_rouba_a_descarada.com",
            "admin@disto_tudo_e_que_rouba_a_descarada.com",
            "111222333", "Admin",
            "document's", "Calle Luna"
        );


        // await CheckRolesAsync(user);

        if (!_dataContext.Owners.Any())
        {
            await AddOwners(
                "Juan", "Zuluaga", "Calle Luna", user);
            await AddOwners(
                "Joaquim", "Alvenaria", "Calle Sol", user);
            await AddOwners(
                "Alberto", "Domingues", "Calle Luna", user);
            await AddOwners(
                "Mariana", "Alvarez", "Calle Sol", user);
            await AddOwners(
                "Lucia", "Liu", "Calle Luna", user);
            await AddOwners(
                "Renee", "Arriaga", "Calle Sol", user);
            await AddOwners(
                "Marcia", "Zuluaga", "Calle Luna", user);
            await AddOwners(
                "Ernesto", "Guevara", "Calle Sol", user);
            await AddOwners(
                "El", "Che", "Calle Luna", user);
            await AddOwners(
                "Claudia", "Arroz", "Calle Sol", user);
        }


        if (!_dataContext.Lessee.Any())
        {
            await AddLessees(
                "Roberto", "Rossellini", "Calle Luna", user);
            await AddLessees(
                "Giuseppe", "Tornatore", "Calle Luna", user);
            await AddLessees(
                "Ingrid", "Bergman", "Calle Sol", user);
            await AddLessees(
                "Gina", "Lollobrigida", "Calle Sol", user);
            await AddLessees(
                "Isabella", "Rossellini", "Calle Luna", user);
            await AddLessees(
                "Monica", "Bellucci", "Calle Sol", user);
            await AddLessees(
                "Giovanna", "Ralli", "Calle Luna", user);
            await AddLessees(
                "Valeria", "Golino", "Calle Sol", user);
            await AddLessees(
                "Sophia", "Loren", "Calle Luna", user);
            await AddLessees(
                "Claudia", "Cardinale", "Calle Sol", user);
        }

        await _dataContext.SaveChangesAsync();
    }


    private async Task<User> CheckUserAsync(
        string firstName, string lastName,
        string userName,
        string? email,
        string phoneNumber, string role,
        string document, string address,
        string password = "123456")
    {
        var user = await _userHelper.GetUserByEmailAsync(email);

        switch (user)
        {
            case null:
            {
                user = role switch
                {
                    "Owner" => new User
                    {
                        Document = document,
                        FirstName = firstName,
                        LastName = lastName,
                        Address = address,
                        UserName = userName,
                        Email = email,
                        PhoneNumber = phoneNumber
                    },
                    "Lessee" => new User
                    {
                        Document = document,
                        FirstName = firstName,
                        LastName = lastName,
                        Address = address,
                        UserName = userName,
                        Email = email,
                        PhoneNumber = phoneNumber
                    },
                    "User" => new User
                    {
                        Document = document,
                        FirstName = firstName,
                        LastName = lastName,
                        Address = address,
                        UserName = userName,
                        Email = email,
                        PhoneNumber = phoneNumber
                    },
                    _ => null
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
    }


    private async Task AddOwners(
        string firstName, string lastName, string address, User user)
    {
        var document = _random.Next(100000, 999999999).ToString();
        var fixedPhone = _random.Next(1000000, 99999999).ToString();
        var cellPhone = _random.Next(1000000, 99999999).ToString();
        var addressFull = address + ", " + _random.Next(1, 9999);

        _dataContext.Owners.Add(new Owner
            {
                Document = document,
                FirstName = firstName,
                LastName = lastName,
                FixedPhone = fixedPhone,
                CellPhone = cellPhone,
                Address = addressFull,
                User = await CheckUserAsync(
                    firstName, lastName,
                    $"{firstName}.{lastName}@rouba_a_descarada.com",
                    $"{firstName}.{lastName}@rouba_a_descarada.com",
                    $"{cellPhone}", "Owner",
                    document, addressFull
                )
            }
        );

        // await _dataContext.SaveChangesAsync();
    }


    private async Task AddLessees(
        string firstName, string lastName, string address, User user)
    {
        var document = _random.Next(100000, 999999999).ToString();
        var fixedPhone = _random.Next(1000000, 99999999).ToString();
        var cellPhone = _random.Next(1000000, 99999999).ToString();
        var addressFull = address + ", " + _random.Next(1, 9999);

        _dataContext.Lessee.Add(new Lessee
            {
                Document = document,
                FirstName = firstName,
                LastName = lastName,
                FixedPhone = fixedPhone,
                CellPhone = cellPhone,
                Address = addressFull,
                User = await CheckUserAsync(
                    firstName, lastName,
                    $"{firstName}.{lastName}@rouba_a_descarada.com",
                    $"{firstName}.{lastName}@rouba_a_descarada.com",
                    $"{cellPhone}", "Lessee",
                    document, addressFull
                )
            }
        );

        // await _dataContext.SaveChangesAsync();
    }


    #region Attributes

    private readonly Random _random = new();
    private readonly DataContext _dataContext;

    private readonly IUserHelper _userHelper;

    // private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    #endregion
}