using GuildCars.Data.Interface_and_Factory;
using GuildCars.Models.Tables;
using GuildCars.UI.Models.ViewModels;
using Microsoft.AspNet.Identity;
using GuildCars.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace GuildCars.UI.Controllers
{
    [Authorize(Roles = "sales")]
    public class SalesController : Controller
    {
        // GET: Sales
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Purchase(int id)
        {
            var repo = RepositoryFactory.GetRepository();
            SalesVM salesVM = new SalesVM();
            salesVM.CarDetailVM = repo.GetDetailsVM(id);
            salesVM.Buyer = new Buyer();
            salesVM.States = new SelectList(repo.GetStates(), "StateId", "StateAbbreviation");
            salesVM.PurchaseTypes = new SelectList(repo.GetPurchaseTypes(), "PurchaseTypeId", "PurchaseTypeName");
            
            return View(salesVM);
        }

        [HttpPost]
        public ActionResult Purchase(SalesVM salesVM)
        {
            var repo = RepositoryFactory.GetRepository();

            if (string.IsNullOrEmpty(salesVM.Buyer.Name))
            {
                ModelState.AddModelError("Name","Please enter buyer name");
            }
            if (string.IsNullOrEmpty(salesVM.Buyer.Street1))
            {
                ModelState.AddModelError("Street", "Please enter buyer's address");
            }
            if (string.IsNullOrEmpty(salesVM.Buyer.City))
            {
                ModelState.AddModelError("City", "Please enter buyer's city");
            }
            if (salesVM.Buyer.ZipCode==0)
            {
                ModelState.AddModelError("Zipcode", "Please enter buyer's zipcode");
            }
            else if (!Regex.Match(salesVM.Buyer.ZipCode.ToString(), @"^\d{5}(?:[-\s]\d{4})?$").Success)
            {
                ModelState.AddModelError("ZipcodeFormat", "Zipcode not in acceptable format");
            }
            if (string.IsNullOrEmpty(salesVM.Buyer.Phone)&& (string.IsNullOrEmpty(salesVM.Buyer.Email)))
            {
                ModelState.AddModelError("PhoneOrEmail", "Please enter either buyer's phone or email");
            }
            if (!string.IsNullOrEmpty(salesVM.Buyer.Phone) && !Regex.Match(salesVM.Buyer.Phone, @"^(\(?\d{3}\)?-? *\d{3}-? *-?\d{4})$").Success)
            {
                ModelState.AddModelError("Phone", "Phone number not valid format");
            }
            if (!string.IsNullOrEmpty(salesVM.Buyer.Email) && !Regex.Match(salesVM.Buyer.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                ModelState.AddModelError("Email", "Email not valid format");
            }
            var carReload = repo.GetCarById(salesVM.CarDetailVM.CarId);
            if (salesVM.CarDetailVM.PurchasePrice < .95*carReload.SalePrice)
            {
                ModelState.AddModelError("PurchasePrice", "Purchase Price too low");
            }
            if (salesVM.CarDetailVM.PurchasePrice >carReload.Msrp)
            {
                ModelState.AddModelError("PurchasePrice", "Purchase Price higher than MSRP");
            }
            if (ModelState.IsValid)
            {
                int buyerId = repo.SaveBuyer(salesVM.Buyer);
                Car car = repo.GetCarById(salesVM.CarDetailVM.CarId);
                car.IsSold = true;
                car.BuyerId = buyerId;
                car.PurchasePrice = salesVM.CarDetailVM.PurchasePrice;
                car.PurchaseTypeId = salesVM.CarDetailVM.PurchaseTypeId;
                car.SoldBy = User.Identity.GetUserId();
                car.SaleDate = DateTime.Today;

                repo.SaveCar(car);

                return RedirectToAction("Index");
            }
            else
            {
                salesVM.CarDetailVM = repo.GetDetailsVM(salesVM.CarDetailVM.CarId);
                salesVM.States = new SelectList(repo.GetStates(), "StateId", "StateAbbreviation");
                salesVM.PurchaseTypes = new SelectList(repo.GetPurchaseTypes(), "PurchaseTypeId", "PurchaseTypeName");
                return View(salesVM);
            }
        }
    }
}