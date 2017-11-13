using GuildCars.Data.Interface_and_Factory;
using GuildCars.Models.Tables;
using GuildCars.UI.Models;
using GuildCars.UI.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{

    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize(Roles = "admin")]
        public ActionResult Vehicles()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Add()
        {
            var repo = RepositoryFactory.GetRepository();
            AddEditCarVM addEditCarVM = new AddEditCarVM();
            addEditCarVM.Makes = new SelectList(repo.GetMakes(), "MakeId", "MakeName");
            addEditCarVM.Models = new SelectList(repo.GetModels(), "ModelId", "ModelName");
            addEditCarVM.Makes = new SelectList(repo.GetMakes(), "MakeId", "MakeName");
            addEditCarVM.BodyStyles = new SelectList(repo.GetBodyStyles(), "BodyStyleId", "BodyStyleName");
            addEditCarVM.Transmissions = new SelectList(repo.GetTransmissions(), "TransmissionId", "TransmissionType");
            addEditCarVM.ExteriorColors = new SelectList(repo.GetExteriorColors(), "ExteriorColorId", "ExteriorColorName");
            addEditCarVM.InteriorColors = new SelectList(repo.GetInteriorColors(), "InteriorColorId", "InteriorColorName");

            return View(addEditCarVM);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Add(AddEditCarVM addEditCarVM)
        {
            if (addEditCarVM.Type == "true")
                addEditCarVM.Car.IsNew = true;
            if (addEditCarVM.Car.ModelId==0)
            {
                ModelState.AddModelError("Model", "Please select a model");
            }
            if (addEditCarVM.Car.ModelId == 0)
            {
                ModelState.AddModelError("Make", "Please select a make");
            }

            if (addEditCarVM.Car.Year>DateTime.Now.Year+1 || addEditCarVM.Car.Year<2000)
            {
                ModelState.AddModelError("Year", "Year must be between 2000 and " + (DateTime.Now.Year + 1).ToString());
            }
            if (addEditCarVM.InteriorColorIds != null) { }
            else
            {
                ModelState.AddModelError("InteriorColor", "You must choose at least 1 interior color");
            }
            if (addEditCarVM.ExteriorColorIds != null) { }
            else
            {
                ModelState.AddModelError("ExteriorColor", "You must choose at least 1 exterior color");
            }
            if (addEditCarVM.Car.IsNew== true && addEditCarVM.Car.Milage >1000)
            {
                ModelState.AddModelError("Milage", "Cannot list as \"new\" if mileage is over 1000 miles");
            }
            //if (string.IsNullOrEmpty(addEditCarVM.Car.Vin) && !Regex.Match(addEditCarVM.Car.Vin, @"^(?<wmi>[A-HJ-NPR-Z\d]{3})(?<vds>[A-HJ-NPR-Z\d]{5})(?<check>[\dX])(?<vis>(?<year>[A-HJ-NPR-Z\d])(?<plant>[A-HJ-NPR-Z\d])(?<seq>[A-HJ-NPR-Z\d]{6}))$").Success)
            //{
            //    ModelState.AddModelError("Vin", "Please provide valid US VIN#");
            //}
            if (string.IsNullOrEmpty(addEditCarVM.Car.Vin))
            {
                ModelState.AddModelError("Vin", "Please provide VIN#");
            }
            if (addEditCarVM.Car.SalePrice<=0)
            {
                ModelState.AddModelError("SalePrice", "SalePrice must be positive number");
            }
            if (addEditCarVM.Car.Msrp <= 0)
            {
                ModelState.AddModelError("MSRP", "MSRP must be positive number");
            }
            if (addEditCarVM.Car.SalePrice > addEditCarVM.Car.Msrp)
            {
                ModelState.AddModelError("Sale/MSRP", "SalePrice must be less than MSRP");
            }
            if (addEditCarVM.UploadedFile==null)
            {
                ModelState.AddModelError("Image", "You must provide an image");
            }
            if (!addEditCarVM.UploadedFile.FileName.Contains(".jpg")&& !addEditCarVM.UploadedFile.FileName.Contains(".jpg")&& !addEditCarVM.UploadedFile.FileName.Contains(".png"))
            {
                ModelState.AddModelError("Image", "Images must be in either jpg, jpeg, or png format");
            }

            if (ModelState.IsValid)
            {
                string fileName = addEditCarVM.UploadedFile.FileName;

                var repo = RepositoryFactory.GetRepository();
                int newCarId = repo.AddCar(addEditCarVM.Car);
                string ext = " ";


                if (fileName.Contains(".jpg"))
                {
                    addEditCarVM.Car.ImageFileName = "images/inventory-" + newCarId.ToString() + ".jpg";
                    if (addEditCarVM.UploadedFile != null && addEditCarVM.UploadedFile.ContentLength > 0)
                    {
                        string path = Path.Combine(Server.MapPath("~/images"),
                            Path.GetFileName("inventory-" + newCarId.ToString() + ".jpg"));

                        addEditCarVM.UploadedFile.SaveAs(path);
                        ext = "jpg";
                    }
                }
                else if (fileName.Contains(".jpeg"))
                {
                    addEditCarVM.Car.ImageFileName = "images/inventory-" + newCarId.ToString() + ".jpeg";
                    if (addEditCarVM.UploadedFile != null && addEditCarVM.UploadedFile.ContentLength > 0)
                    {
                        string path = Path.Combine(Server.MapPath("~/images"),
                            Path.GetFileName("inventory-" + newCarId.ToString() + ".jpeg"));

                        addEditCarVM.UploadedFile.SaveAs(path);
                        ext = "jpeg";
                    }
                }
                else
                {
                    addEditCarVM.Car.ImageFileName = "images/inventory-" + newCarId.ToString() + ".png";
                    if (addEditCarVM.UploadedFile != null && addEditCarVM.UploadedFile.ContentLength > 0)
                    {
                        string path = Path.Combine(Server.MapPath("~/images"),
                            Path.GetFileName("inventory-" + newCarId.ToString() + ".png"));

                        addEditCarVM.UploadedFile.SaveAs(path);
                        ext = "png";
                    }
                }

                repo.CreateCarExteriorBridgeEnties(newCarId, addEditCarVM.ExteriorColorIds);
                repo.CreateCarInteriorBridgeEntries(newCarId, addEditCarVM.InteriorColorIds);
                repo.SaveCarImageFileName(newCarId, "images/inventory-" + newCarId.ToString() + "." + ext);

                return RedirectToAction("Edit/" + newCarId);
            }
            else
            {
                var repo = RepositoryFactory.GetRepository();
                addEditCarVM.Makes = new SelectList(repo.GetMakes(), "MakeId", "MakeName");
                addEditCarVM.Models = new SelectList(repo.GetModels(), "ModelId", "ModelName");
                addEditCarVM.Makes = new SelectList(repo.GetMakes(), "MakeId", "MakeName");
                addEditCarVM.BodyStyles = new SelectList(repo.GetBodyStyles(), "BodyStyleId", "BodyStyleName");
                addEditCarVM.Transmissions = new SelectList(repo.GetTransmissions(), "TransmissionId", "TransmissionType");
                addEditCarVM.ExteriorColors = new SelectList(repo.GetExteriorColors(), "ExteriorColorId", "ExteriorColorName");
                addEditCarVM.InteriorColors = new SelectList(repo.GetInteriorColors(), "InteriorColorId", "InteriorColorName");
                return View(addEditCarVM);
            }
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            var repo = RepositoryFactory.GetRepository();
            AddEditCarVM addEditCarVM = new AddEditCarVM();
            addEditCarVM.Car = repo.GetCarById(id);
            addEditCarVM.Makes = new SelectList(repo.GetMakes(), "MakeId", "MakeName");
            addEditCarVM.Models = new SelectList(repo.GetModels(), "ModelId", "ModelName");
            addEditCarVM.Makes = new SelectList(repo.GetMakes(), "MakeId", "MakeName");
            addEditCarVM.BodyStyles = new SelectList(repo.GetBodyStyles(), "BodyStyleId", "BodyStyleName");
            addEditCarVM.Transmissions = new SelectList(repo.GetTransmissions(), "TransmissionId", "TransmissionType");
            addEditCarVM.ExteriorColors = new SelectList(repo.GetExteriorColors(), "ExteriorColorId", "ExteriorColorName");
            addEditCarVM.InteriorColors = new SelectList(repo.GetInteriorColors(), "InteriorColorId", "InteriorColorName");

            addEditCarVM.Make = repo.GetMakeByModel(addEditCarVM.Car.ModelId).ToString();
            return View(addEditCarVM);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(AddEditCarVM addEditCarVM)
        {
            if (addEditCarVM.Type == "true")
                addEditCarVM.Car.IsNew = true;
            if (addEditCarVM.Car.ModelId == 0)
            {
                ModelState.AddModelError("Model", "Please select a model");
            }
            if (addEditCarVM.Car.ModelId == 0)
            {
                ModelState.AddModelError("Make", "Please select a make");
            }

            if (addEditCarVM.Car.Year > DateTime.Now.Year + 1 || addEditCarVM.Car.Year < 2000)
            {
                ModelState.AddModelError("Year", "Year must be between 2000 and " + (DateTime.Now.Year + 1).ToString());
            }
            if (addEditCarVM.InteriorColorIds != null) { }
            else
            {
                ModelState.AddModelError("InteriorColor", "You must choose at least 1 interior color");
            }
            if (addEditCarVM.ExteriorColorIds != null) { }
            else
            {
                ModelState.AddModelError("ExteriorColor", "You must choose at least 1 exterior color");
            }
            if (addEditCarVM.Car.IsNew == true && addEditCarVM.Car.Milage > 1000)
            {
                ModelState.AddModelError("Milage", "Cannot list as \"new\" if mileage is over 1000 miles");
            }
            //if (string.IsNullOrEmpty(addEditCarVM.Car.Vin) && !Regex.Match(addEditCarVM.Car.Vin, @"^(?<wmi>[A-HJ-NPR-Z\d]{3})(?<vds>[A-HJ-NPR-Z\d]{5})(?<check>[\dX])(?<vis>(?<year>[A-HJ-NPR-Z\d])(?<plant>[A-HJ-NPR-Z\d])(?<seq>[A-HJ-NPR-Z\d]{6}))$").Success)
            //{
            //    ModelState.AddModelError("Vin", "Please provide valid US VIN#");
            //}
            if (string.IsNullOrEmpty(addEditCarVM.Car.Vin))
            {
                ModelState.AddModelError("Vin", "Please provide VIN#");
            }
            if (addEditCarVM.Car.SalePrice <= 0)
            {
                ModelState.AddModelError("SalePrice", "SalePrice must be positive number");
            }
            if (addEditCarVM.Car.Msrp <= 0)
            {
                ModelState.AddModelError("MSRP", "MSRP must be positive number");
            }
            if (addEditCarVM.Car.SalePrice > addEditCarVM.Car.Msrp)
            {
                ModelState.AddModelError("Sale/MSRP", "SalePrice must be less than MSRP");
            }
            if (addEditCarVM.UploadedFile!=null)
            {
                if (!addEditCarVM.UploadedFile.FileName.Contains(".jpg") && !addEditCarVM.UploadedFile.FileName.Contains(".jpg") && !addEditCarVM.UploadedFile.FileName.Contains(".png"))
                {
                    ModelState.AddModelError("Image", "Images must be in either jpg, jpeg, or png format");
                }
            }

            if (ModelState.IsValid)
            {
                string fileName = null;
                string ext = " ";

                //<---if  a new file was loaded get it's name-->
                if (addEditCarVM.UploadedFile != null)
                {
                    fileName = addEditCarVM.UploadedFile.FileName;

                    if (fileName != null && fileName.Contains(".jpg"))
                    {
                        addEditCarVM.Car.ImageFileName = "images/inventory-" + addEditCarVM.Car.CarId.ToString() + ".jpg";
                        if (addEditCarVM.UploadedFile != null && addEditCarVM.UploadedFile.ContentLength > 0)
                        {
                            string path = Path.Combine(Server.MapPath("~/images"),
                                Path.GetFileName("inventory-" + addEditCarVM.Car.CarId.ToString() + ".jpg"));
                            ext = "jpg";

                            addEditCarVM.UploadedFile.SaveAs(path);
                        }
                    }
                    else if (fileName.Contains(".jpeg"))
                    {
                        addEditCarVM.Car.ImageFileName = "images/inventory-" + addEditCarVM.Car.CarId.ToString() + ".jpeg";
                        if (addEditCarVM.UploadedFile != null && addEditCarVM.UploadedFile.ContentLength > 0)
                        {
                            string path = Path.Combine(Server.MapPath("~/images"),
                                Path.GetFileName("inventory-" + addEditCarVM.Car.CarId.ToString() + ".jpeg"));

                            addEditCarVM.UploadedFile.SaveAs(path);
                            ext = "jpeg";

                        }
                    }
                    else
                    {
                        addEditCarVM.Car.ImageFileName = "images/inventory-" + addEditCarVM.Car.CarId.ToString() + ".png";
                        if (addEditCarVM.UploadedFile != null && addEditCarVM.UploadedFile.ContentLength > 0)
                        {
                            string path = Path.Combine(Server.MapPath("~/images"),
                                Path.GetFileName("inventory-" + addEditCarVM.Car.CarId.ToString() + ".png"));

                            addEditCarVM.UploadedFile.SaveAs(path);
                            ext = "png";
                        }
                    }
                }

                var repo = RepositoryFactory.GetRepository();
                if (fileName != null)
                {
                    repo.SaveCarImageFileName(addEditCarVM.Car.CarId, "images/inventory-" + addEditCarVM.Car.CarId.ToString() + "." + ext);
                }
                //<---if  a new file was create new ImageFileName and send to folder.  If filename was null, no overwrite should happen-->

                //<---Delete existing color bridges-->
                repo.DeleteCarExteriorBridgeEntries(addEditCarVM.Car.CarId);
                repo.DeleteCarInteriorBridgeEntries(addEditCarVM.Car.CarId);

                //<---Create new color bridges-->
                repo.CreateCarExteriorBridgeEnties(addEditCarVM.Car.CarId, addEditCarVM.ExteriorColorIds);
                repo.CreateCarInteriorBridgeEntries(addEditCarVM.Car.CarId, addEditCarVM.InteriorColorIds);

                //<---Save edited car-->
                repo.EditCar(addEditCarVM.Car);

                return RedirectToAction("Vehicles");
            }
            else
            {
                return View(addEditCarVM);
            }
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Users()
        {
            GuildCarsDbContext db = new GuildCarsDbContext();
            var repo = RepositoryFactory.GetRepository();

            var users = repo.GetUsers();

            return View(users);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult EditUser(string id)
        {
            var repo = RepositoryFactory.GetRepository();

            EditUserVM editUserVM = new EditUserVM();
            editUserVM.user = repo.GetUserById(id);


            return View(editUserVM);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditUser(EditUserVM model)
        {
            if (string.IsNullOrEmpty(model.user.FirstName))
            {
                ModelState.AddModelError("FirstName", "Please enter first name");
            }
            if (string.IsNullOrEmpty(model.user.LastName))
            {
                ModelState.AddModelError("LastName", "Please enter last name");
            }
            if (string.IsNullOrEmpty(model.user.Email) || !Regex.Match(model.user.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                ModelState.AddModelError("Email", "Please provide valid email address");
            }
            if (ModelState.IsValid)
            {
                var repo = RepositoryFactory.GetRepository();

                var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
                var authManager = HttpContext.GetOwinContext().Authentication;

                repo.SaveUser(model.user);
                userManager.RemovePassword(model.user.UserId);

                userManager.AddPassword(model.user.UserId, model.NewPassword);

                return RedirectToAction("Users");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Roles = "admin, sales")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin, sales")]
        public ActionResult ChangePassword(ChangePasswordVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var repo = RepositoryFactory.GetRepository();

            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
            var authManager = HttpContext.GetOwinContext().Authentication;
            var userId = User.Identity.GetUserId();

            userManager.RemovePassword(userId);

            userManager.AddPassword(userId, model.NewPassword);

            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult AddUser()
        {
            EditUserVM editUserVM = new EditUserVM();

            return View(editUserVM);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddUser(EditUserVM model)
        {
            if (string.IsNullOrEmpty(model.user.FirstName))
            {
                ModelState.AddModelError("FirstName", "Please enter first name");
            }
            if (string.IsNullOrEmpty(model.user.LastName))
            {
                ModelState.AddModelError("LastName", "Please enter last name");
            }
            if (string.IsNullOrEmpty(model.user.Email) || !Regex.Match(model.user.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                ModelState.AddModelError("Email", "Please provide valid email address");
            }

            if (ModelState.IsValid)
            {
                var context = new GuildCarsDbContext();
                var repo = RepositoryFactory.GetRepository();

                var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
                var authManager = HttpContext.GetOwinContext().Authentication;

                var user = new AppUser()
                {
                    FirstName = model.user.FirstName,
                    LastName = model.user.LastName,
                    Email = model.user.Email
                };

                model.user.UserId = user.Id;
                repo.CreateUser(model.user);

                return RedirectToAction("Users");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Makes()
        {
            var repo = RepositoryFactory.GetRepository();
            MakeVM model = new MakeVM();
            var makes = repo.GetMakes();
            var users = repo.GetUsers();
            model.Makes = (from m in makes
                           join u in users on m.AdminId equals u.UserId
                           select new MakeSummary
                           {
                            MakeName = m.MakeName,
                            DateAdded = m.DateAdded,
                            Email = u.Email
                           }).ToList();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Makes(MakeVM model)
        {
            if (ModelState.IsValid)
            {
                var repo = RepositoryFactory.GetRepository();
                Make make = new Make();
                make.AdminId = User.Identity.GetUserId();
                make.DateAdded = DateTime.Now;
                make.MakeName = model.NewMake;
                repo.SaveNewMake(make);
                return RedirectToAction("Makes");
            }
            else
            {
                return RedirectToAction("Makes");
            }
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Models()
        {
                var repo = RepositoryFactory.GetRepository();
                ModelsVM model = new ModelsVM();
                var models = repo.GetModels();
                var users = repo.GetUsers();
                var bodystyles = repo.GetBodyStyles();
                var makes = repo.GetMakes();
                model.Models = (from m in models
                                join u in users on m.AddedBy equals u.UserId
                                join b in bodystyles on m.BodyStyleId equals b.BodyStyleId
                                join k in makes on m.MakeId equals k.MakeId
                                select new ModelSummary
                                {
                                    ModelName = m.ModelName,
                                    DateAdded = m.DateAdded,
                                    Email = u.Email,
                                    MakeName = k.MakeName,
                                    BodystyleName = b.BodyStyleName
                                }).ToList();
                model.Makes = new SelectList(repo.GetMakes(), "MakeId", "MakeName");
                model.BodyStyles = new SelectList(repo.GetBodyStyles(), "BodyStyleId", "BodyStyleName");
                return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Models(ModelsVM model)
        {

            if (ModelState.IsValid)
            {
                var repo = RepositoryFactory.GetRepository();
                Model newModel = new Model();
                newModel.BodyStyleId = model.BodystyleId;
                newModel.ModelName = model.NewModel;
                newModel.MakeId = model.MakeId;
                newModel.DateAdded = DateTime.Now;
                newModel.AddedBy = User.Identity.GetUserId();

                repo.SaveNewModel(newModel);
                return RedirectToAction("Models");
            }
            else
            {
                return RedirectToAction("Models");
            }

        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Specials()
        {
            var repo = RepositoryFactory.GetRepository();
            SpecialsVM model = new SpecialsVM();
            model.Specials = repo.GetSpecials().ToList();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Specials(SpecialsVM model)
        {
            if (string.IsNullOrEmpty(model.NewSpecial.SpecialName))
            {
                ModelState.AddModelError("Title", "Please enter a name for the special");
            }
            if (string.IsNullOrEmpty(model.NewSpecial.SpecialName))
            {
                ModelState.AddModelError("Description", "Please enter a description for the special");
            }

            if (ModelState.IsValid)
            {
                string fileName = model.UploadedFile.FileName;
                string ext = " ";

                var repo = RepositoryFactory.GetRepository();
                int newSpecialId = repo.AddSpecial(model.NewSpecial);

                if (fileName.Contains(".jpg"))
                {
                    model.NewSpecial.ImageFileName = "images/special" + newSpecialId.ToString() + ".jpg";
                    if (model.UploadedFile != null && model.UploadedFile.ContentLength > 0)
                    {
                        string path = Path.Combine(Server.MapPath("~/images"),
                            Path.GetFileName("special" + newSpecialId.ToString() + ".jpg"));

                        model.UploadedFile.SaveAs(path);
                        ext = "jpg";
                    }
                }
                else if (fileName.Contains(".jpeg"))
                {
                    model.NewSpecial.ImageFileName = "images/special" + newSpecialId.ToString() + ".jpeg";
                    if (model.UploadedFile != null && model.UploadedFile.ContentLength > 0)
                    {
                        string path = Path.Combine(Server.MapPath("~/images"),
                            Path.GetFileName("special" + newSpecialId.ToString() + ".jpeg"));

                        model.UploadedFile.SaveAs(path);
                        ext = "jpeg";
                    }
                }
                else
                {
                    model.NewSpecial.ImageFileName = "images/special" + newSpecialId.ToString() + ".png";
                    if (model.UploadedFile != null && model.UploadedFile.ContentLength > 0)
                    {
                        string path = Path.Combine(Server.MapPath("~/images"),
                            Path.GetFileName("special" + newSpecialId.ToString() + ".png"));

                        model.UploadedFile.SaveAs(path);
                        ext = "png";
                    }
                }

                repo.SaveSpecialImageFileName(newSpecialId, "special" + newSpecialId.ToString() + "." + ext);

                return RedirectToAction("Specials");
            }
            else
            {
                var repo = RepositoryFactory.GetRepository();
                model.Specials = repo.GetSpecials().ToList();

                return View(model);
            }
        }
    }
}
