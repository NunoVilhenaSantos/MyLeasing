using Microsoft.EntityFrameworkCore;
using MyLeasing.Common;
using MyLeasing.Common.DataContexts;
using MyLeasing.Common.MockRepositories;
using MyLeasing.Common.Repositories;
using MyLeasing.Common.Seeders;


// using MyLeasing.Web.Data;
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// make sure to add the connection string in the appsettings.json file
// and the connection string name is the same as the one in the appsettings.json file
// in this case the connection string name is "DefaultConnection"
// builder.Services.AddDbContext<DataContext>(
//     options =>
//         options.UseSqlServer(
//             builder.Configuration.GetConnectionString(
//                 "DefaultConnection")));


builder.Services.AddDbContext<DataContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString(
                "SomeeConnection")));


// Add services to the container.
// make sure to add the connection string in the appsettings.json file
// and the connection string name is the same as the one in the appsettings.json file
// in this case the connection string name is "LocalMySQLConnection"
// MySQL is not supported in .NET 6.0 yet,
// so we need to install the following package
// Microsoft.EntityFrameworkCore.Relational
// builder.Services.AddDbContext<DataContextMySql>(
//     options =>
//         options.UseMySQL(
//             builder.Configuration.GetConnectionString(
//                 "LocalMySQLConnection") ?? string.Empty ));


// Add services to the container.
// make sure to add the connection string in the appsettings.json file
// and the connection string name is the same as the one in the appsettings.json file
// in this case the connection string name is "SqliteConnection"
// SQLite is not supported in .NET 6.0 yet,
// so we need to install the following package
// Microsoft.EntityFrameworkCore.Sqlite
// builder.Services.AddDbContext<DataContextSQLite>(
//     options =>
//         options.UseSqlite(
//             builder.Configuration.GetConnectionString(
//                 "SqliteConnection")));


// Add services to the container.
builder.Services.AddTransient<Random>();
builder.Services.AddTransient<SeedDb>();

builder.Services.AddScoped<IRepository, Repository>();
// builder.Services.AddScoped<IRepository, MockRepository>();

// builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


// Add services to the container.
builder.Services.AddAuthentication();


// Add services to the container.
builder.Services.AddControllersWithViews();


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

app.Run();