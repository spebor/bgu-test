using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction(nameof(SwitchController.Index), "Switch");
        }
    }
}
