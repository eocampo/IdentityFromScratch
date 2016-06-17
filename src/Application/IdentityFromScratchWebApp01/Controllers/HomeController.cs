using System;
using System.Web;
using System.Web.Mvc;

namespace IdentityFromScratchWebApp01.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() {
            return View();
        }
    }
}