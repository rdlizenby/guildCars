using GuildCars.Data.Interface_and_Factory;
using GuildCars.UI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Sales()
        {
            var repo = RepositoryFactory.GetRepository();
            SalesReportVM model = new SalesReportVM();
            model.Users =  new SelectList(repo.GetUsers(), "UserId", "Email");

            model.QueryResults = repo.GetSalesReport(null, new DateTime(1900, 1, 1), DateTime.Now.AddDays(1));
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Inventory()
        {
            var repo = RepositoryFactory.GetRepository();
            InventoryVM model = new InventoryVM();
            model.NewVehicles = repo.InventoryQuery(true);
            model.UsedVehicles = repo.InventoryQuery(false);
            return View(model);
        }
    }
}