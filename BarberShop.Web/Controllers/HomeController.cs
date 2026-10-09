using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BarberShop.Models;

namespace BarberShop.Controllers;

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

    public IActionResult Login() => RedirectToAction("Login", "Account");

    public IActionResult Agendamento() => View();
}
