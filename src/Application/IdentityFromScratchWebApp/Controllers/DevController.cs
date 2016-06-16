using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdentityFromScratchWebApp.Models;
using System.Data.Entity;

namespace IdentityFromScratchWebApp.Controllers
{
    public class DevController : Controller
    {
        // GET: Dev
        public ActionResult Index() {
            return View();
        }

        public ActionResult CreateDatabase() {
            ApplicationDbContext dbContext = ApplicationDbContext.Create();
            dbContext.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole("Developers"));
            return Redirect("~/Dev");
        }

        public ActionResult CreateSchema() {
            //var configuration = new Migrations.Configuration();
            //var migrator = new System.Data.Entity.Migrations.DbMigrator(configuration);
            //migrator.Update();
            return Redirect("~/Dev");
        }

        public ActionResult DropSchema() {
            //var configuration = new Migrations.Configuration();
            //var migrator = new System.Data.Entity.Migrations.DbMigrator(configuration);
            //migrator.Update("0");
            return Redirect("~/Dev");
        }
    }
}