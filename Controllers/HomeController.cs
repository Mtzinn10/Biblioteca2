using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Biblioteca2.Models;

namespace Biblioteca2.Controllers;
{
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        Autenticacao.CheckLogin(this);
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string login, string senha)
    {
        if (Autenticacao.verificacaoLogin(login, senha, this))
        {
            return RedirectToAction("Index");
        }
        else
        {
            ViewData["Erro"] = "Usuario ou Senha Podem Estar Incorretas";
            return View();
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
}