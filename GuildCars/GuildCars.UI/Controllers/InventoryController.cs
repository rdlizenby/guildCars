using GuildCars.Data.Interface_and_Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory
        [AllowAnonymous]
        public ActionResult New()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Used()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var repo = RepositoryFactory.GetRepository();
            return View(repo.GetDetailsVM(id));
        }
    }
}