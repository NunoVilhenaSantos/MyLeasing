using MyLeasing.Common.Data.Entities;

namespace MyLeasing.Common.Data.Seeders;

public class SeedDb
{
    #region Attributes

    private readonly DataContext _dataContext;
    private readonly Random _random;

    #endregion


    public SeedDb(DataContext dataContext, Random random)
    {
        _dataContext = dataContext;
        _random = random;
    }


    public async Task SeedAsync()
    {
        await _dataContext.Database.EnsureCreatedAsync();

        // if (_dataContext.Owners.Any()) return;

        AddOwners("Juan", "Zuluaga", "Calle Luna");
        AddOwners("Joaquim", "Alvenaria", "Calle Sol");
        AddOwners("Alberto", "Domingues", "Calle Luna");
        AddOwners("Mariana", "Alvarez", "Calle Sol");
        AddOwners("Lucia", "Liu", "Calle Luna");
        AddOwners("Renee", "Arriaga", "Calle Sol");
        AddOwners("Marcia", "Zuluaga", "Calle Luna");
        AddOwners("Ernesto", "Guevara", "Calle Sol");
        AddOwners("El", "Che", "Calle Luna");
        AddOwners("Claudia", "Arroz", "Calle Sol");

        // await CheckRolesAsync();
        // await CheckUserAsync("1010", "Juan", "Zuluaga");
    }


    private void AddOwners(string firstName, string lastName, string address)
    {
        _dataContext.Owners.Add(new Owner
            {
                Document = _random.Next(100000, 999999999).ToString(),
                FirstName = firstName,
                LastName = lastName,
                FixedPhone = _random.Next(1000000, 99999999).ToString(),
                CellPhone = _random.Next(1000000, 99999999).ToString(),
                Address = address + ", " + _random.Next(1, 100),
            }
        );

        _dataContext.SaveChanges();
    }
}