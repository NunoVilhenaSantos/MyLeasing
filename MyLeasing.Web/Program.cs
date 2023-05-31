using Microsoft.EntityFrameworkCore;
using MyLeasing.Common.Data;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();


// Add services to the container.
// make sure to add the connection string in the appsettings.json file
// and the connection string name is the same as the one in the appsettings.json file
// in this case the connection string name is "DefaultConnection"
// builder.Services.AddDbContext<DataContext2>(
//     options =>
//         options.UseSqlServer(
//             builder.Configuration.GetConnectionString(
//                 "DefaultConnection")));


// Add services to the container.
// make sure to add the connection string in the appsettings.json file
// and the connection string name is the same as the one in the appsettings.json file
// in this case the connection string name is "LocalMySQLConnection"
// MySQL is not supported in .NET 6.0 yet, so we need to install the following package
// Microsoft.EntityFrameworkCore.Relational
// builder.Services.AddDbContext<DataContext1>(
//     options =>
//         options.UseMySQL(
//             builder.Configuration.GetConnectionString(
//                 "LocalMySQLConnection") ?? string.Empty));


// Add services to the container.
// make sure to add the connection string in the appsettings.json file
// and the connection string name is the same as the one in the appsettings.json file
// in this case the connection string name is "SqliteConnection"
// SQLite is not supported in .NET 6.0 yet, so we need to install the following package
// Microsoft.EntityFrameworkCore.Sqlite
builder.Services.AddDbContext<DataContext>(
    options =>
        options.UseSqlite(
            builder.Configuration.GetConnectionString(
                "SqliteConnection")));


builder.Services.AddAuthentication();

var app = builder.Build();

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