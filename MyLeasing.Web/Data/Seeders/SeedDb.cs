using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data.DataContexts;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Helpers;

namespace MyLeasing.Web.Data.Seeders;

public class SeedDb
{
    private readonly Random _random = new();
    private readonly DataContextMSSQL _dataContextMssql;
    private readonly DataContextMySql _dataContextMySql;
    private readonly DataContextSQLite _dataContextSqLite;

    private readonly IUserHelper _userHelper;

    // private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;


    public const string MyLeasingAdminsNuno =
        "nuno.santos.26288@formandos.cinel.pt";

    public const string MyLeasingAdminsDiogo =
        "diogo.alves.28645@formandos.cinel.pt";

    public const string MyLeasingAdminsRuben =
        "ruben.corrreia.28257@formandos.cinel.pt";

    public const string MyLeasingAdminsTatiane =
        "tatiane.avellar.24718@formandos.cinel.pt";

    public const string MyLeasingAdminsJorge =
        "jorge.pinto.28720@formandos.cinel.pt";

    public const string MyLeasingAdminsJoel =
        "joel.rangel.22101@formandos.cinel.pt";

    public const string MyLeasingAdminsLicinio =
        "licinio.do.rosario@formandos.cinel.pt";


    // Injeção de dependência do IWebHostEnvironment
    private readonly IWebHostEnvironment _hostingEnvironment;

    public string PlaceHolders;


    public SeedDb(
        IUserHelper userHelper,
        DataContextMSSQL dataContextMssql,
        DataContextSQLite dataContextSqLite,
        DataContextMySql dataContextMySql,
        IWebHostEnvironment hostingEnvironment
        // UserManager<User> userManager,
        // RoleManager<IdentityRole> roleManager
    )
    {
        _userHelper = userHelper;
        _dataContextMssql = dataContextMssql;
        _dataContextMySql = dataContextMySql;
        _dataContextSqLite = dataContextSqLite;
        _hostingEnvironment = hostingEnvironment;
        // _userManager = userManager;
        // _roleManager = roleManager;
    }


    public async Task SeedAsync()
    {
        await _dataContextMssql.Database.EnsureCreatedAsync();
        await _dataContextMySql.Database.EnsureCreatedAsync();
        await _dataContextSqLite.Database.EnsureCreatedAsync();

        await AddUsers(MyLeasingAdminsJorge,
            "Calle Luna", "Admin", "123456");
        await AddUsers(MyLeasingAdminsRuben,
            "Calle Luna", "Admin", "123456");
        await AddUsers(MyLeasingAdminsTatiane,
            "Calle Luna", "Admin", "123456");
        await AddUsers(MyLeasingAdminsJoel,
            "Calle Luna", "Admin", "123456");
        await AddUsers(MyLeasingAdminsLicinio,
            "Calle Luna", "Admin", "123456");
        await AddUsers(MyLeasingAdminsDiogo,
            "Calle Luna", "Admin", "123456");
        await AddUsers(MyLeasingAdminsNuno,
            "Calle Luna", "Admin", "123456");


        var user = await CheckUserAsync(
            "Juan", "Zuluaga",
            "admin@disto_tudo_e_que_rouba_a_descarada.com",
            "admin@disto_tudo_e_que_rouba_a_descarada.com",
            "111222333", "Admin",
            "document's", "Calle Luna"
        );

        // await CheckRolesAsync(user);

        if (!_dataContextMssql.Owners.Any())
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


        if (!_dataContextMssql.Lessees.Any())
        {
            await AddLessees(
                "Roberto", "Rossellini", "Calle Luna", user);
            await AddLessees(
                "Giuseppe", "Tornatore", "Calle Luna", user);
            await AddLessees(
                "Federico", "Fellini", "Rimini", user);
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

        // if (!_dataContextMssql.Properties.Any())
        // {
        //     await AddProperties(user);
        // }

        // verificar se existem os placeholders no sistema
        AddPlaceHolders();


        await _dataContextMssql.SaveChangesAsync();
        // await BackupData();
    }


    private async Task BackupData()
    {
        try
        {
            var backupDateTime = DateTime.Now;
            var backupFileName =
                $"backup_{backupDateTime.ToString("yyyyMMdd_HHmmss")}.db";

            // Criar uma nova instância do contexto usando o nome da base de dados de backup
            var backupOptionsBuilder =
                new DbContextOptionsBuilder<DataContextMySql>()
                    .UseSqlServer($"Data Source={backupFileName}");

            using (var backupContext =
                   new DataContextMySql(backupOptionsBuilder.Options))
            {
                // Certifique-se de que a nova base de dados de backup seja criada
                await backupContext.Database.EnsureCreatedAsync();

                // Obter todos os dados das tabelas relevantes da base de dados principal
                var owners = _dataContextMssql.Owners.ToList();
                var lessees = _dataContextMssql.Lessees.ToList();
                var roles = _dataContextMssql.Roles.ToList();
                var users = _dataContextMssql.Users.ToList();
                var roleClaims = _dataContextMssql.RoleClaims.ToList();
                var userClaims = _dataContextMssql.UserClaims.ToList();
                var userLogins = _dataContextMssql.UserLogins.ToList();
                var userRoles = _dataContextMssql.UserRoles.ToList();
                var userTokens = _dataContextMssql.UserTokens.ToList();

                // Adicionar os dados obtidos à base de dados de backup
                backupContext.Owners.AddRange(owners);
                backupContext.Lessees.AddRange(lessees);
                backupContext.Roles.AddRange(roles);
                backupContext.Users.AddRange(users);
                backupContext.RoleClaims.AddRange(roleClaims);
                backupContext.UserClaims.AddRange(userClaims);
                backupContext.UserLogins.AddRange(userLogins);
                backupContext.UserRoles.AddRange(userRoles);
                backupContext.UserTokens.AddRange(userTokens);

                // Adicione outras tabelas conforme necessário

                // Salvar as alterações na base de dados de backup
                await backupContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            // Lidar com qualquer exceção que possa ocorrer durante a operação
            Console.WriteLine(
                "Ocorreu um erro ao fazer o backup " +
                "para a base de dados SQLite: " +
                _dataContextMssql.Database.GetDbConnection().Database + "\n" +
                ex.Message);
        }
    }


    private void AddPlaceHolders()
    {
        var baseDirectory = _hostingEnvironment.ContentRootPath;
        var diretorioBase =
            Path.GetFullPath(Path.Combine(baseDirectory, "Helpers/Images"));

        var origem = Path.Combine(_hostingEnvironment.ContentRootPath,
            "Helpers", "Images");
        var destino = Path.Combine(_hostingEnvironment.WebRootPath,
            "images", "PlaceHolders");
        PlaceHolders = Path.Combine(_hostingEnvironment.WebRootPath,
            "images", "PlaceHolders");


        // Cria o diretório se não existir
        var folderPath = Path.Combine(
            Directory.GetCurrentDirectory(), "wwwroot", "images",
            "PlaceHolders");
        Directory.CreateDirectory(destino);
        Path.Exists(destino);


        // Obtém todos os caminhos dos arquivos na pasta de origem
        var arquivos = Directory.GetFiles(origem);


        // Itera sobre os caminhos dos arquivos e copia cada um para a pasta de destino
        foreach (var arquivo in arquivos)
        {
            var nomeArquivo = Path.GetFileName(arquivo);
            var caminhoDestino = Path.Combine(destino, nomeArquivo);
            File.Copy(arquivo, caminhoDestino, true);
        }

        Console.WriteLine("Placeholders adicionados com sucesso!");
    }

    private async Task AddUsers(
        string email, string address,
        string role, string password
    )
    {
        var userSplit = email.Split(
            '.', StringSplitOptions.RemoveEmptyEntries);

        var document = _random.Next(100000, 999999999).ToString();
        var fixedPhone = _random.Next(1000000, 99999999).ToString();
        var cellPhone = _random.Next(1000000, 99999999).ToString();
        var addressFull = address + ", " + _random.Next(1, 9999);

        await CheckUserAsync(
            userSplit[0], userSplit[1],
            email,
            email,
            cellPhone, role,
            document,
            addressFull, password
        );
    }


    private async Task<User> CheckUserAsync(
        string firstName, string lastName,
        string userName,
        string email,
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
                    "Lessees" => new User
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
                    "Admin" => new User
                    {
                        Document = document,
                        FirstName = firstName,
                        LastName = lastName,
                        Address = address,
                        UserName = userName,
                        Email = email,
                        PhoneNumber = phoneNumber
                    },
                    _ => throw new InvalidOperationException(
                        "The role is not valid")
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

        _dataContextMssql.Owners.Add(new Owner
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

        // await _dataContextMssql.SaveChangesAsync();
    }


    private async Task AddLessees(
        string firstName, string lastName, string address, User user)
    {
        var document = _random.Next(100000, 999999999).ToString();
        var fixedPhone = _random.Next(1000000, 99999999).ToString();
        var cellPhone = _random.Next(1000000, 99999999).ToString();
        var addressFull = address + ", " + _random.Next(1, 9999);

        _dataContextMssql.Lessees.Add(new Lessee
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
                    $"{cellPhone}", "Lessees",
                    document, addressFull
                )
            }
        );

        // await _dataContextMssql.SaveChangesAsync();
    }
}