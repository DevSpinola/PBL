using Microsoft.AspNetCore.Mvc;
using PBL.Models;
using System.Diagnostics;

namespace PBL.Controllers
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult BancoDeDados() // Adicionando a ação para a nova view
        {
            return View("BancoDeDados"); // Retorna a View correspondente ao banco de dados
        }
        public IActionResult Grafico() // Adicionando a ação para a nova view
        {
            string resultado1 = HttpContext.Request.Query["resultado1"];
            string resultado2 = HttpContext.Request.Query["resultado2"];
          
            
            ViewBag.Resultado1 = resultado1;
            ViewBag.Resultado2 = string.IsNullOrEmpty(resultado2) ? "" : resultado2;
            return View("Grafico"); // Retorna a View correspondente ao G
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}