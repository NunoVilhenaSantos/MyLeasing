using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data.DataContexts;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Data.Repositories;
using MyLeasing.Web.Data.Repositories.Interfaces;
using MyLeasing.Web.Data.Seeders;
using MyLeasing.Web.Helpers;


// Configurações do host

// using MyLeasing.Web.Data;
var builder = WebApplication.CreateBuilder(args);


// -----------------------------------------------------------------------------
//
// Database connection via data-context
//
// -----------------------------------------------------------------------------


//builder.Services.AddDbContext<DataContext>(options =>
//{
//    options.UseSqlServer(connectionStringSommee);
//    options.UseSqlite(builder.Configuration.GetConnectionString("MyLeasing-NunoVilhenaSantos.mssql.somee.com"));
//    options.UseSqlite(builder.Configuration.GetConnectionString("MyLeasing-NunoVilhenaSantos-SQLite"));
//}
//);
// ...

builder.Services.AddDbContext<DataContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString(
                "MyLeasing-NunoVilhenaSantos.mssql.somee.com")));


builder.Services.AddDbContext<DataContextSQLite>(
    options =>
        options.UseSqlite(
            builder.Configuration.GetConnectionString(
                "MyLeasing-NunoVilhenaSantos-SQLite") ?? string.Empty));


builder.Services.AddDbContext<DataContextPostgreSQL>(
    options =>
        options.UseNpgsql(
            builder.Configuration.GetConnectionString(
                "MyLeasing-NunoVilhenaSantos-PostgreSQL")));


builder.Services.AddDbContext<DataContextMySql>(
    options =>
        options.UseMySQL(
            builder.Configuration.GetConnectionString(
                "MyLeasing-NunoVilhenaSantos-MySQL") ?? string.Empty));


// -----------------------------------------------------------------------------
//
// User services via Identity service
//
// -----------------------------------------------------------------------------

// Add services to the container.
// adding the identity service before the AddControllersWithViews service
builder.Services.AddIdentity<User, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequireDigit = false;
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.AllowedForNewUsers = true;
    })
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<DataContext>();


builder.Services
    .AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth",
        config =>
        {
            config.Cookie.Name = "SuperShop.Cookie";
            config.LoginPath = "/Home/Authenticate";
        });

// -----------------------------------------------------------------------------
//
// Seeding databases via seeder
//
// -----------------------------------------------------------------------------

// Add services to the container.
// builder.Services.AddTransient<Random>();
builder.Services.AddTransient<SeedDb>();


// -----------------------------------------------------------------------------
//
// instantiated repositories via services added to the container
//
// -----------------------------------------------------------------------------

// Add helpers

// add user-helper service to the container
builder.Services.AddScoped<IUserHelper, UserHelper>();
// builder.Services.AddScoped<IUserHelper, MockUserHelper>();

// add identity service to the container
// builder.Services.AddScoped<IdentityUser, User>();
builder.Services.AddScoped<UserManager<User>, UserManager<User>>();
builder.Services.AddScoped<SignInManager<User>, SignInManager<User>>();
builder.Services
    .AddScoped<RoleManager<IdentityRole>, RoleManager<IdentityRole>>();


builder.Services.AddScoped<IImageHelper, ImageHelper>();
builder.Services.AddScoped<IStorageHelper, StorageHelper>();
builder.Services.AddScoped<IConverterHelper, ConverterHelper>();


//services.AddScoped<ICountryRepository, CountryRepository>();


// builder.Services.AddScoped<GCPConfigOptions>();
// builder.Services.AddScoped<AWSConfigOptions>();
// builder.Services.AddScoped<ICloudStorageService, CloudStorageService>();


// -----------------------------------------------------------------------------
//
// instantiated repositories via services added to the container
//
// -----------------------------------------------------------------------------

// Repositories
//
// OwnerRepository
//
// builder.Services.AddScoped<IRepository, Repository>();
// builder.Services.AddScoped<IRepository, MockRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
// builder.Services.AddScoped<IOwnerRepository, MockOwnerRepository>();

//
// LesseesRepository
//
// builder.Services.AddScoped<IRepository, Repository>();
// builder.Services.AddScoped<IRepository, MockRepository>();
builder.Services.AddScoped<ILesseeRepository, LesseeRepository>();
// builder.Services.AddScoped<ILesseeRepository, MockLesseeRepository>();


// Add services to the container.
builder.Services.AddAuthentication();


// Add services to the container.
builder.Services.AddRazorPages().AddRazorPagesOptions(options => { });
builder.Services.AddControllersWithViews().AddViewLocalization();
builder.Services.AddControllersWithViews();


builder.Services.AddApplicationInsightsTelemetry(
    builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);


//builder.Services.AddAzureClients(clientBuilder =>
//{
//    clientBuilder.AddBlobServiceClient(
//        builder.Configuration["Storages:AzureBlobKeySuperShopNuno:blob"],
//        true);
//    clientBuilder.AddQueueServiceClient(
//        builder.Configuration["Storages:AzureBlobKeySuperShopNuno:queue"],
//        true);
//});
//builder.Services.AddAzureClients(clientBuilder =>
//{
//    clientBuilder.AddBlobServiceClient(
//        builder.Configuration["Storages:AzureBlobKeyGlobalGamesNuno:blob"],
//        true);
//    clientBuilder.AddQueueServiceClient(
//        builder.Configuration["Storages:AzureBlobKeyGlobalGamesNuno:queue"],
//        true);
//});
//builder.Services.AddAzureClients(clientBuilder =>
//{
//    clientBuilder.AddBlobServiceClient(
//        builder.Configuration["Storages:AzureBlobKeyMyLeasingNuno:blob"],
//        true);
//    clientBuilder.AddQueueServiceClient(
//        builder.Configuration["Storages:AzureBlobKeyMyLeasingNuno:queue"],
//        true);
//});


builder.Logging.ClearProviders();
//builder.Logging.AddAzureWebAppDiagnostics();


var app = builder.Build();


RunSeeding();

void RunSeeding()
{
    var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

    using var scope = scopeFactory.CreateScope();

    var seeder = scope.ServiceProvider.GetService<SeedDb>();

    seeder?.SeedAsync().Wait();
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days.
    // You may want to change this for production scenarios,
    // see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");


//public static IHostBuilder CreateHostBuilder(string[] args) =>
//    Host.CreateDefaultBuilder(args)
//        .ConfigureWebHostDefaults(webBuilder =>
//        {
//            webBuilder.UseStartup<Startup>()
//                .ConfigureLogging(logging =>
//                {
//                    logging.AddAzureWebAppDiagnostics();
//                    logging.AddAzureBlobStorage(options =>
//                    {
//                        options.SasToken = "<your_sas_token>";
//                    });
//                });
//        });


app.Run();