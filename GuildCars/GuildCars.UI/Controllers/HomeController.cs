using GuildCars.Data.Interface_and_Factory;
using GuildCars.Models.Tables;
using GuildCars.UI.Models;
using GuildCars.UI.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
            var authManager = HttpContext.GetOwinContext().Authentication;

            // attempt to load the user with this password
            AppUser user = userManager.Find(model.Email, model.Password);

            // user will be null if the password or user name is bad
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password");

                return View(model);
            }
            else
            {
                // successful login, set up their cookies and send them on their way
                var identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authManager.SignIn(new AuthenticationProperties { IsPersistent = model.RememberMe }, identity);

                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            var repo = RepositoryFactory.GetRepository();          
            return View(repo.GetHomeIndexVM());
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Specials()
        {
            var repo = RepositoryFactory.GetRepository();
            return View(repo.GetSpecials());
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Contact(string vin)
        {
            ViewBag.Vin = vin;
            ContactRequest contactRequest = new ContactRequest();
            if (vin != null)
                contactRequest.Message = "Regarding VIN # " + vin;
            return View(contactRequest);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Contact(ContactRequest contactRequest)
        {
            if (ModelState.IsValid)
            {
                var repo = RepositoryFactory.GetRepository();
                repo.PostContactRequest(contactRequest);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(contactRequest);
            }
        }
    }
}