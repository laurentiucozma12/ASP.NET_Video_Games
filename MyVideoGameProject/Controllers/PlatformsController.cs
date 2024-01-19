using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyVideoGameProject.Controllers
{
    [Authorize(Roles = "Employee")]
    public class PlatformsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
