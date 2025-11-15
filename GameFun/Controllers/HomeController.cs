using System.Diagnostics;
using GameFun.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameFun.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameServices _gameServices;

        public HomeController(IGameServices gameServices)
        {
            _gameServices=gameServices;
        }

        public IActionResult Index()
        {
            var games = _gameServices.GetAll();
            return View(games);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
