using Microsoft.AspNetCore.Mvc;
using MyLeasing.Web.Models;
using System.Diagnostics;

namespace MyLeasing.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None,
        NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Contacts()
    {
        return View();
        // throw new NotImplementedException();
    }

    public IActionResult About()
    {
        return View();
        // throw new NotImplementedException();
    }
}