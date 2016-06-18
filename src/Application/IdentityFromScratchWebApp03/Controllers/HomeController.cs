using System.Web.Mvc;

namespace IdentityFromScratchWebApp03.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() {
            return View();
        }
    }
}