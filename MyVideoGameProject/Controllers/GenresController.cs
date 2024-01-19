using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyVideoGameProject.Controllers
{
    [Authorize(Roles = "Employee")]
    public class GenresController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
