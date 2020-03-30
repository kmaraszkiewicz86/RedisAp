using Microsoft.AspNetCore.Mvc;

namespace RedisUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}