using VideoGameModel.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using VideoGameModel.Data;

namespace MyVideoGameProject

{
    public class HomeController : Controller
    {
        private readonly VideoGameContext _context;
        public HomeController(VideoGameContext context)
        {
            _context = context;
        }
        public IActionResult Chat()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
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