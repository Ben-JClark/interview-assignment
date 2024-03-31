using Microsoft.AspNetCore.Mvc;

namespace SeekaProject.Server.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
