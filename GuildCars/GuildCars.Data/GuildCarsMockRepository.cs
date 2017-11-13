using GuildCars.Data.Interface_and_Factory;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Models.ViewModels;
using GuildCars.Models.AjaxRequest;
using GuildCars.Data.LinqMethod;

namespace GuildCars.Data
{
    public class GuildCarsMockRepository//:IRepository
    {
        static IEnumerable<BodyStyle> bodyStyles;
        static IEnumerable<Buyer> buyers;
        static IEnumerable<Car> cars;
        static IEnumerable<ContactRequest> contactRequests;
        static IEnumerable<ExteriorColor> exteriorColors;
        static IEnumerable<InteriorColor> interiorColors;
        static IEnumerable<Make> makes;
        static IEnumerable<Model> models;
        static IEnumerable<PurchaseType> purchaseTypes;
        static IEnumerable<Special> specials;
        static IEnumerable<State> states;
        static IEnumerable<Transmission> transmissions;
        static IEnumerable<CarInteriorColorBridge> carInteriorColorBridge;
        static IEnumerable<CarExteriorColorBridge> carExteriorColorBridge;
        static IEnumerable<Role> roles;

        static int contactIdCount = 5;
        static int buyerIdCount = 4;
        static int carIdCount = 11;
        static int userIdCount = 5;

        public GuildCarsMockRepository()
        {
            roles = new List<Role>()
            {
                new Role {RoleId = 1, RoleName = "Sales"},
                new Role {RoleId = 2, RoleName = "Admin"},
                new Role {RoleId = 3, RoleName = "Disabled"}
            };

            makes = new List<Make>()
            {
                new Make { MakeId = 1, MakeName = "Honda", DateAdded = new DateTime(2017, 10, 1), AdminId = "adminGuid123"},
                new Make { MakeId = 2, MakeName = "Toyota", DateAdded = new DateTime(2017, 10, 2), AdminId = "adminGuid123"},
                new Make { MakeId = 3, MakeName = "Lexus", DateAdded = new DateTime(2017, 10, 3), AdminId = "adminGuid456"},
                new Make { MakeId = 4, MakeName = "Chevrolet", DateAdded = new DateTime(2017, 10, 4), AdminId = "adminGuid456"}
            };

            models = new List<Model>()
            {
                new Model { ModelId = 1, ModelName = "CRV", MakeId = 1, BodyStyleId = 1, DateAdded = new DateTime(2017, 10, 1), AddedBy = "adminGuid123" },
                new Model { ModelId = 2, ModelName = "Accord", MakeId = 1, BodyStyleId = 2, DateAdded = new DateTime(2017, 10, 1), AddedBy = "adminGuid123" },
                new Model { ModelId = 3, ModelName = "Tacoma", MakeId = 2, BodyStyleId = 3, DateAdded = new DateTime(2017, 10, 2), AddedBy = "adminGuid123" },
                new Model { ModelId = 4, ModelName = "Sienna", MakeId = 2, BodyStyleId = 4, DateAdded = new DateTime(2017, 10, 2), AddedBy = "adminGuid123" },
                new Model { ModelId = 5, ModelName = "LS", MakeId = 3, BodyStyleId = 2, DateAdded = new DateTime(2017, 10, 3), AddedBy = "adminGuid456" },
                new Model { ModelId = 6, ModelName = "RC", MakeId = 3, BodyStyleId = 5, DateAdded = new DateTime(2017, 10, 3), AddedBy = "adminGuid456" },
                new Model { ModelId = 7, ModelName = "Malibu", MakeId = 4, BodyStyleId = 6, DateAdded = new DateTime(2017, 10, 4), AddedBy = "adminGuid456" },
                new Model { ModelId = 8, ModelName = "Traverse", MakeId = 4, BodyStyleId = 7, DateAdded = new DateTime(2017, 10, 5), AddedBy = "adminGuid456" }
            };

            bodyStyles = new List<BodyStyle>()
            {
                new BodyStyle { BodyStyleId = 1, BodyStyleName = "Crossover"},
                new BodyStyle { BodyStyleId = 2, BodyStyleName = "Sedan"},
                new BodyStyle { BodyStyleId = 3, BodyStyleName = "Truck"},
                new BodyStyle { BodyStyleId = 4, BodyStyleName = "Van"},
                new BodyStyle { BodyStyleId = 5, BodyStyleName = "Sports Car"},
                new BodyStyle { BodyStyleId = 6, BodyStyleName = "Station Wagon"},
                new BodyStyle { BodyStyleId = 7, BodyStyleName = "SUV"},
            };

            exteriorColors = new List<ExteriorColor>()
            {
                new ExteriorColor { ExteriorColorId = 1, ExteriorColorName = "White"},
                new ExteriorColor { ExteriorColorId = 2, ExteriorColorName = "Black"},
                new ExteriorColor { ExteriorColorId = 3, ExteriorColorName = "Tan"},
                new ExteriorColor { ExteriorColorId = 4, ExteriorColorName = "Gold"},
                new ExteriorColor { ExteriorColorId = 5, ExteriorColorName = "Red"},
                new ExteriorColor { ExteriorColorId = 6, ExteriorColorName = "Blue"},
                new ExteriorColor { ExteriorColorId = 7, ExteriorColorName = "Green"},
                new ExteriorColor { ExteriorColorId = 8, ExteriorColorName = "Orange"},
                new ExteriorColor { ExteriorColorId = 9, ExteriorColorName = "Silver"},
                new ExteriorColor { ExteriorColorId = 10, ExteriorColorName = "Brown"}
            };

            interiorColors = new List<InteriorColor>()
            {
                new InteriorColor { InteriorColorId = 1, InteriorColorName = "Grey"},
                new InteriorColor { InteriorColorId = 2, InteriorColorName = "Black"},
                new InteriorColor { InteriorColorId = 3, InteriorColorName = "White"},
                new InteriorColor { InteriorColorId = 4, InteriorColorName = "Tan"},
                new InteriorColor { InteriorColorId = 5, InteriorColorName = "Beige"},
                new InteriorColor { InteriorColorId = 6, InteriorColorName = "Maroon"}
            };

            transmissions = new List<Transmission>()
            {
                new Transmission { TransmissionId = 1, TransmissionType = "Automatic"},
                new Transmission { TransmissionId = 2, TransmissionType = "Manual"}
            };

            specials = new List<Special>()
            {
                new Special { SpecialId = 1, SpecialName = "0% APR, 60 Months", ImageFileName = "special1.jpg", SpecialDescription = "0% Annual Percentage Rate (APR) up to 36 months. 0% Annual Percentage Rate (APR) up to 60 months. 0.9% Annual Percentage Rate (APR) up to 66 months. 1.9% Annual Percentage Rate (APR) up to 72 months. Plus $500 Cash Back APR financing available, subject to credit approval..", StartDate = new DateTime(2017, 6, 1), EndDate = new DateTime(2017, 12, 31)},
                new Special { SpecialId = 2, SpecialName = "Military Discount", ImageFileName = "special2.jpg",SpecialDescription = "$400 Military Bonus For Qualified Customers.  Customers eligible for the Military Specialty Incentive Program must be an active member of, honorably discharged from, retired from, or on disability with the United States Armed Forces or Reserves (includes those that have “national” status from another country and are serving in the United States military) or the spouse of the eligible participant.",StartDate = new DateTime(2017, 4, 1), EndDate = new DateTime(2017, 10, 31)},
                new Special { SpecialId = 3, SpecialName = "Student Program", ImageFileName = "special3.jpg",SpecialDescription = "$400 College Student & Graduate Bonus for Qualified Buyers. We are helping recent graduates make the transition from school to their careers by making it easy and affordable to buy a new Kia vehicle by offering a $400 discount incentive in addition to certain other incentives. Not all incentive programs are compatible.", StartDate = new DateTime(2017, 1, 1), EndDate = new DateTime(2017, 12, 31)},
                new Special { SpecialId = 4, SpecialName = "Uber Driver Program", ImageFileName = "special4.jpg", SpecialDescription = "$1,000 Uber Driver Partner Incentive for Qualfied Buyers. Customers eligible for the Uber Driver Partner Specialty Incentive program must be an active Uber Driver Partner and the primary buyer of the vehicle at the time of purchase.", StartDate = new DateTime(2017, 4, 1), EndDate = new DateTime(2017, 8, 31)},
                new Special { SpecialId = 5, SpecialName = "Lyft Driver Program", ImageFileName = "special5.jpg", SpecialDescription = "$1,000 Lyft Driver Partner Incentive for Qualfied Buyers. Customers eligible for the Lyft Driver Partner Specialty Incentive program must be an active Lyft Driver Partner and the primary buyer of the vehicle at the time of purchase.", StartDate = new DateTime(2017, 9, 1), EndDate = new DateTime(2017, 12, 31)}
            };

            contactRequests = new List<ContactRequest>()
            {
                new ContactRequest { ContactRequestId = 1, Name = "Joe Smith", Email = "prospectiveBuyer1@gmail.com", Phone = "123-456-7890", Message = "I like fast cars...", SalesPersonId = "salesGuid123", RespondedOn = new DateTime(2017, 10, 2)},
                new ContactRequest { ContactRequestId = 2, Name = "Bob Evans", Email = "prospectiveBuyer2@gmail.com", Phone = "223-456-7890", Message = "Can I get $15,000 trade-in on my 1983 Toyota Corolla?", SalesPersonId = "salesGuid123", RespondedOn = new DateTime(2017, 10, 4)},
                new ContactRequest { ContactRequestId = 3, Name = "Joe Evans", Email = "prospectiveBuyer3@gmail.com", Phone = "323-456-7890", Message = "I'm really good at Call of Duty.  You think that's close enough to being in the military to get the discount?"},
                new ContactRequest { ContactRequestId = 4, Name = "Bob Smith", Email = "prospectiveBuyer4@gmail.com", Phone = "423-456-7890", Message = "What's the deal with dropping your Uber partnership and going with Lyft?"}
            };

            states = new List<State>()
            {
                new State { StateAbbreviation = "AL", StateName = "Alabama"},
                new State { StateAbbreviation = "CT", StateName = "Connecticut"},
                new State { StateAbbreviation = "IL", StateName = "Illinois"},
                new State { StateAbbreviation = "IN", StateName = "Indiana"},
                new State { StateAbbreviation = "KY", StateName = "Kentucky"},
                new State { StateAbbreviation = "MI", StateName = "Michigan"},
                new State { StateAbbreviation = "MN", StateName = "Minnesota"},
                new State { StateAbbreviation = "MS", StateName = "Mississippi"},
                new State { StateAbbreviation = "NY", StateName = "New York"},
                new State { StateAbbreviation = "OH", StateName = "Ohio"},
                new State { StateAbbreviation = "TN", StateName = "Tennessee"},
                new State { StateAbbreviation = "WA", StateName = "Washington"},
            };

            purchaseTypes = new List<PurchaseType>()
            {
                new PurchaseType { PurchaseTypeId = 1, PurchaseTypeName = "Bank Finance"},
                new PurchaseType { PurchaseTypeId = 2, PurchaseTypeName = "Cash"},
                new PurchaseType { PurchaseTypeId = 3, PurchaseTypeName = "Dealer Finance"}
            };

            buyers = new List<Buyer>()
            {
                new Buyer { BuyerId = 1, Name = "Cameran Cochran", Email="cCochran@gmail.com", Phone="111-222-3333", Street1 = "387-7170 Nibh St.", Street2 = "Apt 3", City = "Biloxi", StateId = 1, ZipCode = 38506},
                new Buyer { BuyerId = 2, Name = "Sheila Marsh", Email="sMarsh@gmail.com", Phone="222-333-4444", Street1 = "Ap #665-3410 Feugiat Rd.", City = "Hamme", StateId = 2, ZipCode = 70916},
                new Buyer { BuyerId = 3, Name = "Brent Haynes", Email="bHaynes@gmail.com", Phone="333-444-5555", Street1 = "2393 Nulla. St.", City = "Opheylissem", StateId = 4, ZipCode = 67677}
            };

            cars = new List<Car>()
            {
                new Car {CarId = 1, ModelId = 1, IsNew = true, Year = 2017,  Milage = 180, TransmissionId = 1, Vin = "1G4GB5EG7AF110257", Msrp = 24000, IsFeatured = true,IsSold = false, Description = "This is a new Honda CRV", AddedBy = "adminGuid123", AddedDate = new DateTime(2017, 10, 1), SalePrice = 23500, ImageFileName = "images/crv2017.jpg"},
                new Car {CarId = 2, ModelId = 2, IsNew = true, Year = 2017,  Milage = 220, TransmissionId = 2, Vin = "1FTPX28Z3XKC90533", Msrp = 22500, IsFeatured = false,IsSold = true, Description = "This is a brand new Honda Accord", AddedBy = "adminGuid456", AddedDate = new DateTime(2017, 10, 2), SalePrice = 22000, ImageFileName = "images/accord2017.jpg", BuyerId = 1, PurchasePrice = 22000, PurchaseTypeId = 1, SoldBy = "salesGuid123", SaleDate = new DateTime(2017, 10, 2)},
                new Car {CarId = 3, ModelId = 3, IsNew = true, Year = 2017,  Milage = 498, TransmissionId = 1, Vin = "JF1AF21B4DA191572", Msrp = 24550, IsFeatured = true,IsSold = false, Description = "This is a brand new Toyota Tacoma", AddedBy = "adminGuid123", AddedDate = new DateTime(2017, 10, 3), SalePrice = 24500, ImageFileName = "images/tacoma2017.jpg"},
                new Car {CarId = 4, ModelId = 4, IsNew = true, Year = 2018,  Milage = 15, TransmissionId = 1, Vin = "1M2P286C5TM063936", Msrp = 29750, IsFeatured = false,IsSold = false, Description = "This is a brand new Toyota Sienna", AddedBy = "adminGuid456", AddedDate = new DateTime(2017, 10, 4), SalePrice = 29500, ImageFileName = "images/sienna2017.jpg"},
                new Car {CarId = 5, ModelId = 5, IsNew = true, Year = 2018,  Milage = 0, TransmissionId = 1, Vin = "1C6RD7MT0CS225625", Msrp = 72500, IsFeatured = true,IsSold = false, Description = "This is a brand new Lexus LS", AddedBy = "adminGuid123", AddedDate = new DateTime(2017, 10, 5), SalePrice = 69500, ImageFileName = "images/ls2017.jpg"},
                new Car {CarId = 6, ModelId = 6, IsNew = false, Year = 2015, Milage = 54288, TransmissionId = 2, Vin = "KMHCU4AE3CU005446", Msrp = 21700, IsFeatured = true,IsSold = false, Description = "This is a used Lexus RC", AddedBy = "adminGuid123", AddedDate = new DateTime(2017, 10, 6), SalePrice = 38000, ImageFileName = "images/rc2017.jpg"},
                new Car {CarId = 7, ModelId = 7, IsNew = false, Year = 2013, Milage = 73560, TransmissionId = 1, Vin = "1GKET12P146124900", Msrp = 28500, IsFeatured = false, IsSold = false, Description = "This is a used Chevy Malibu", AddedBy = "adminGuid456", AddedDate = new DateTime(2017, 10, 7), SalePrice = 14300, ImageFileName = "images/malibu2013.jpg"},
                new Car {CarId = 8, ModelId = 8, IsNew = false, Year = 2012,  Milage = 68234, TransmissionId = 1, Vin = "JH2RC44392K602727", Msrp = 28700, IsFeatured = false,IsSold = true, Description = "This is a used Chevy Traverse", AddedBy = "adminGuid456", AddedDate = new DateTime(2017, 10, 8), SalePrice = 45500, ImageFileName = "images/traverse2012.jpg", BuyerId = 2, PurchasePrice = 43000, PurchaseTypeId = 3, SoldBy = "salesGuid456", SaleDate = new DateTime(2017, 9, 25)},
                new Car {CarId = 9, ModelId = 1, IsNew = false, Year = 2015,  Milage = 27001, TransmissionId = 1, Vin = "WVWVD63B84E006972", Msrp = 21500, IsFeatured = true,IsSold = false, Description = "This is a used Honda CRV", AddedBy = "adminGuid123", AddedDate = new DateTime(2017, 10, 9), SalePrice = 21500, ImageFileName = "images/crv2016.jpg"},
                new Car {CarId = 10, ModelId = 2, IsNew = false, Year = 2006,  Milage = 117392, TransmissionId = 1, Vin = "1FTMF1E89AKE99651", Msrp = 17500, IsFeatured = false,IsSold = true, Description = "This is a used Honda Accord", AddedBy = "adminGuid456", AddedDate = new DateTime(2017, 10, 10), SalePrice = 9500, ImageFileName = "images/accord2006.jpg", BuyerId = 3, PurchasePrice = 9250, PurchaseTypeId = 2, SoldBy = "salesGuid123", SaleDate = new DateTime(2017, 10, 4)}
            };

            //users = new List<User>()
            //{
            //    new User {UserId = "salesGuid123", FirstName = "Billy", LastName = "Bob", Email = "sales123@guildCars.com", Password = "testing123"},
            //    new User {UserId = "salesGuid345", FirstName = "Sally", LastName = "Sue", Email = "sales123@guildCars.com", Password = "testing123"},
            //    new User {UserId = "adminGuid123", FirstName = "Jim", LastName = "Jones", Email = "admin123@guildCars.com", Password = "testing123" },
            //    new User {UserId = "adminGuid345", FirstName = "John", LastName = "Jones", Email = "admin123@guildCars.com", Password = "testing123"},
            //};

            carExteriorColorBridge = new List<CarExteriorColorBridge>()
            {
                new CarExteriorColorBridge {CarId = 1, ExteriorColorId = 1},
                new CarExteriorColorBridge {CarId = 2, ExteriorColorId = 2},
                new CarExteriorColorBridge {CarId = 3, ExteriorColorId = 3},
                new CarExteriorColorBridge {CarId = 4, ExteriorColorId = 4},
                new CarExteriorColorBridge {CarId = 5, ExteriorColorId = 5},
                new CarExteriorColorBridge {CarId = 6, ExteriorColorId = 6},
                new CarExteriorColorBridge {CarId = 6, ExteriorColorId = 7},
                new CarExteriorColorBridge {CarId = 7, ExteriorColorId = 8},
                new CarExteriorColorBridge {CarId = 8, ExteriorColorId = 9},
                new CarExteriorColorBridge {CarId = 9, ExteriorColorId = 10},
                new CarExteriorColorBridge {CarId = 9, ExteriorColorId = 1},
                new CarExteriorColorBridge {CarId = 10, ExteriorColorId = 2},

            };

            carInteriorColorBridge = new List<CarInteriorColorBridge>()
            {
                new CarInteriorColorBridge {CarId = 1, InteriorColorId = 1},
                new CarInteriorColorBridge {CarId = 2, InteriorColorId = 2},
                new CarInteriorColorBridge {CarId = 2, InteriorColorId = 3},
                new CarInteriorColorBridge {CarId = 3, InteriorColorId = 4},
                new CarInteriorColorBridge {CarId = 4, InteriorColorId = 5},
                new CarInteriorColorBridge {CarId = 5, InteriorColorId = 6},
                new CarInteriorColorBridge {CarId = 6, InteriorColorId = 1},
                new CarInteriorColorBridge {CarId = 6, InteriorColorId = 2},
                new CarInteriorColorBridge {CarId = 7, InteriorColorId = 3},
                new CarInteriorColorBridge {CarId = 8, InteriorColorId = 4},
                new CarInteriorColorBridge {CarId = 9, InteriorColorId = 5},
                new CarInteriorColorBridge {CarId = 10, InteriorColorId = 6}
            };
        }

        public List<ContactRequest> GetContacts()
        {
            return contactRequests.ToList();
        }

        public List<State> GetStates()
        {
            return states.ToList();
        }

        public CarDetailVM GetDetailsVM(int id)
        {
            CarDetailVM carDetails = (from c in cars
                              join mod in models on c.ModelId equals mod.ModelId
                              join mk in makes on mod.MakeId equals mk.MakeId
                              join t in transmissions on c.TransmissionId equals t.TransmissionId
                              join b in bodyStyles on mod.BodyStyleId equals b.BodyStyleId
                              where c.CarId == id
                              select new CarDetailVM
                              {
                                  CarId = c.CarId,
                                  Year = c.Year,
                                  Make = mk.MakeName,
                                  Model = mod.ModelName,
                                  BodyStyle = b.BodyStyleName,
                                  TransmitionType = t.TransmissionType,
                                  Milage = c.Milage,
                                  Vin = c.Vin,
                                  SalePrice = c.SalePrice,
                                  Msrp = c.Msrp,
                                  Description = c.Description,
                                  ImageFileName = c.ImageFileName,
                                  IsSold = c.IsSold
                              }).FirstOrDefault();

            carDetails.InteriorColor = (from c in cars
                                 join ib in carInteriorColorBridge on c.CarId equals ib.CarId
                                 join ic in interiorColors on ib.InteriorColorId equals ic.InteriorColorId
                                 where c.CarId == carDetails.CarId
                                 select ic.InteriorColorName).ToList();

            carDetails.ExteriorColor = (from c in cars
                                 join e in carExteriorColorBridge on c.CarId equals e.CarId
                                 join eb in exteriorColors on e.ExteriorColorId equals eb.ExteriorColorId
                                 where c.CarId == carDetails.CarId
                                 select eb.ExteriorColorName).ToList();

            return carDetails;
        }

        public HomeIndexVM GetHomeIndexVM()
        {
            HomeIndexVM homeIndexVM = new HomeIndexVM()
            {
                Specials = specials,
                CarShorts = from c in cars
                            join mod in models on c.ModelId equals mod.ModelId
                            join mk in makes on mod.MakeId equals mk.MakeId
                            where c.IsFeatured == true
                            select new CarShortList
                            {
                                CarId = c.CarId,
                                Year = c.Year,
                                Make = mk.MakeName,
                                Model = mod.ModelName,
                                SalePrice = c.SalePrice,
                                ImageFileName =c.ImageFileName 
                            }
            };
            return homeIndexVM;
        }

        public IEnumerable<Special> GetSpecials()
        {
            return specials;
        }

        public List<PurchaseType> GetPurchaseTypes()
        {
            return purchaseTypes.ToList();
        }    
        public IEnumerable<CarDetailVM> NewSearch(SearchRequest searchRequest)
        {
            int minPrice = int.Parse(searchRequest.MinPrice);
            int maxPrice = int.Parse(searchRequest.MaxPrice);
            int minYear = int.Parse(searchRequest.MinYear);
            int maxYear = int.Parse(searchRequest.MaxYear);
            var carResults = (from c in cars
                             join mod in models on c.ModelId equals mod.ModelId
                             join mk in makes on mod.MakeId equals mk.MakeId
                             join t in transmissions on c.TransmissionId equals t.TransmissionId
                             join b in bodyStyles on mod.BodyStyleId equals b.BodyStyleId
                             where c.IsNew == true && c.Year >= minYear && c.Year <= maxYear && c.SalePrice > minPrice && c.SalePrice < maxPrice
                             orderby c.SalePrice descending
                             select new CarDetailVM
                             {
                                 CarId = c.CarId,
                                 Year = c.Year,
                                 Make = mk.MakeName,
                                 Model = mod.ModelName,
                                 BodyStyle = b.BodyStyleName,
                                 TransmitionType = t.TransmissionType,
                                 Milage = c.Milage,
                                 Vin = c.Vin,
                                 SalePrice = c.SalePrice,
                                 Msrp = c.Msrp,
                                 Description = c.Description,
                                 ImageFileName = c.ImageFileName
                             }).Take(20).ToList();

            List<CarDetailVM> copy = new List<CarDetailVM>(carResults);

            foreach (CarDetailVM car in copy)
            {
               
                car.InteriorColor = (from c in cars
                                     join ib in carInteriorColorBridge on c.CarId equals ib.CarId
                                     join ic in interiorColors on ib.InteriorColorId equals ic.InteriorColorId
                                     where c.CarId == car.CarId
                                     select ic.InteriorColorName).ToList();

                car.ExteriorColor = (from c in cars
                                     join e in carExteriorColorBridge on c.CarId equals e.CarId
                                     join eb in exteriorColors on e.ExteriorColorId equals eb.ExteriorColorId
                                     where c.CarId == car.CarId
                                     select eb.ExteriorColorName).ToList();                                    
            }

            var lastFilter = (from c in carResults
                              where c.Year.ToString() == searchRequest.SearchTerm || c.Make.ToLower().Contains(searchRequest.SearchTerm.ToLower()) || c.Model.ToLower().Contains(searchRequest.SearchTerm.ToLower())
                              select c).Distinct();

            return lastFilter;
        }

        public void PostContactRequest(ContactRequest contactRequest)
        {
            contactRequest.ContactRequestId = contactIdCount;
            contactIdCount++;
            List<ContactRequest> list = contactRequests.ToList();
            list.Add(contactRequest);
            contactRequests = list;
        }

        public IEnumerable<CarDetailVM> SalesSearch(SearchRequest searchRequest)
        {
            int minPrice = int.Parse(searchRequest.MinPrice);
            int maxPrice = int.Parse(searchRequest.MaxPrice);
            int minYear = int.Parse(searchRequest.MinYear);
            int maxYear = int.Parse(searchRequest.MaxYear);
            var carResults = (from c in cars
                              join mod in models on c.ModelId equals mod.ModelId
                              join mk in makes on mod.MakeId equals mk.MakeId
                              join t in transmissions on c.TransmissionId equals t.TransmissionId
                              join b in bodyStyles on mod.BodyStyleId equals b.BodyStyleId
                              where c.IsSold == false && c.Year >= minYear && c.Year <= maxYear && c.SalePrice > minPrice && c.SalePrice < maxPrice
                              orderby c.SalePrice descending
                              select new CarDetailVM
                              {
                                  CarId = c.CarId,
                                  Year = c.Year,
                                  Make = mk.MakeName,
                                  Model = mod.ModelName,
                                  BodyStyle = b.BodyStyleName,
                                  TransmitionType = t.TransmissionType,
                                  Milage = c.Milage,
                                  Vin = c.Vin,
                                  SalePrice = c.SalePrice,
                                  Msrp = c.Msrp,
                                  Description = c.Description,
                                  ImageFileName = c.ImageFileName
                              }).Take(20).ToList();

            List<CarDetailVM> copy = new List<CarDetailVM>(carResults);

            foreach (CarDetailVM car in copy)
            {

                car.InteriorColor = (from c in cars
                                     join ib in carInteriorColorBridge on c.CarId equals ib.CarId
                                     join ic in interiorColors on ib.InteriorColorId equals ic.InteriorColorId
                                     where c.CarId == car.CarId
                                     select ic.InteriorColorName).ToList();

                car.ExteriorColor = (from c in cars
                                     join e in carExteriorColorBridge on c.CarId equals e.CarId
                                     join eb in exteriorColors on e.ExteriorColorId equals eb.ExteriorColorId
                                     where c.CarId == car.CarId
                                     select eb.ExteriorColorName).ToList();
            }

            var lastFilter = (from c in carResults
                              where c.Year.ToString() == searchRequest.SearchTerm || c.Make.ToLower().Contains(searchRequest.SearchTerm.ToLower()) || c.Model.ToLower().Contains(searchRequest.SearchTerm.ToLower())
                              select c).Distinct();

            return lastFilter;
        }

        public IEnumerable<CarDetailVM> UsedSearch(SearchRequest searchRequest)
        {
            int minPrice = int.Parse(searchRequest.MinPrice);
            int maxPrice = int.Parse(searchRequest.MaxPrice);
            int minYear = int.Parse(searchRequest.MinYear);
            int maxYear = int.Parse(searchRequest.MaxYear);
            var carResults = (from c in cars
                              join mod in models on c.ModelId equals mod.ModelId
                              join mk in makes on mod.MakeId equals mk.MakeId
                              join t in transmissions on c.TransmissionId equals t.TransmissionId
                              join b in bodyStyles on mod.BodyStyleId equals b.BodyStyleId
                              where c.IsNew == false && c.Year >= minYear && c.Year <= maxYear && c.SalePrice > minPrice && c.SalePrice < maxPrice
                              orderby c.SalePrice descending
                              select new CarDetailVM
                              {
                                  CarId = c.CarId,
                                  Year = c.Year,
                                  Make = mk.MakeName,
                                  Model = mod.ModelName,
                                  BodyStyle = b.BodyStyleName,
                                  TransmitionType = t.TransmissionType,
                                  Milage = c.Milage,
                                  Vin = c.Vin,
                                  SalePrice = c.SalePrice,
                                  Msrp = c.Msrp,
                                  Description = c.Description,
                                  ImageFileName = c.ImageFileName
                              }).Take(20).ToList();

            List<CarDetailVM> copy = new List<CarDetailVM>(carResults);

            foreach (CarDetailVM car in copy)
            {

                car.InteriorColor = (from c in cars
                                     join ib in carInteriorColorBridge on c.CarId equals ib.CarId
                                     join ic in interiorColors on ib.InteriorColorId equals ic.InteriorColorId
                                     where c.CarId == car.CarId
                                     select ic.InteriorColorName).ToList();

                car.ExteriorColor = (from c in cars
                                     join e in carExteriorColorBridge on c.CarId equals e.CarId
                                     join eb in exteriorColors on e.ExteriorColorId equals eb.ExteriorColorId
                                     where c.CarId == car.CarId
                                     select eb.ExteriorColorName).ToList();
            }

            var lastFilter = (from c in carResults
                              where c.Year.ToString() == searchRequest.SearchTerm || c.Make.ToLower().Contains(searchRequest.SearchTerm.ToLower()) || c.Model.ToLower().Contains(searchRequest.SearchTerm.ToLower())
                              select c).Distinct();

            return lastFilter;
        }

        public int SaveBuyer(Buyer buyer)
        {
            buyer.BuyerId = buyerIdCount;
            buyerIdCount++;
            List<Buyer> list = buyers.ToList();
            list.Add(buyer);
            buyers = list;

            return buyerIdCount - 1;
        }

        public Car GetCarById(int id)
        {
            Car car = (from c in cars
                       where c.CarId == id
                       select c).FirstOrDefault();
            return car;
        }

        public void SaveCar(Car car)
        {
            cars = cars.Where(i => i.CarId != car.CarId).ToList();
            List<Car> list = cars.ToList();
            list.Add(car);
            cars = list;
        }

        public List<Make> GetMakes()
        {
            return makes.ToList();
        }

        public List<Model> GetModels()
        {
            return models.ToList();
        }

        public List<BodyStyle> GetBodyStyles()
        {
            return bodyStyles.ToList();
        }

        public List<Transmission> GetTransmissions()
        {
            return transmissions.ToList();
        }

        public List<InteriorColor> GetInteriorColors()
        {
            return interiorColors.ToList();
        }

        public List<ExteriorColor> GetExteriorColors()
        {
            return exteriorColors.ToList();
        }

        public List<Model> GetFilteredModels(int id)
        {
            var filteredModels = from m in models
                                 where m.MakeId == id
                                 select m;
            return filteredModels.ToList();
        }

        public string GetBodystyle(int id)
        {
            var filteredBodystyle = (from b in bodyStyles
                                     join m in models on b.BodyStyleId equals m.BodyStyleId
                                     where m.ModelId == id
                                     select b.BodyStyleName).FirstOrDefault();
            return filteredBodystyle;
        }

        public int AddCar(Car car)
        {
            car.CarId = carIdCount;
            carIdCount++;
            List<Car> list = cars.ToList();
            list.Add(car);
            cars = list;

            return carIdCount - 1;
        }

        public void CreateCarExteriorBridgeEnties(int newCarId, string[] exteriorColorIds)
        {
            foreach (string c in exteriorColorIds)
            {
                CarExteriorColorBridge newBridge = new CarExteriorColorBridge();
                newBridge.CarId = newCarId;
                newBridge.ExteriorColorId = int.Parse(c);
                List<CarExteriorColorBridge> list = carExteriorColorBridge.ToList();
                list.Add(newBridge);
                carExteriorColorBridge = list;
            }
        }

        public void CreateCarInteriorBridgeEntries(int newCarId, string[] interiorColorIds)
        {
            foreach (string c in interiorColorIds)
            {
                CarInteriorColorBridge newBridge = new CarInteriorColorBridge();
                newBridge.CarId = newCarId;
                newBridge.InteriorColorId = int.Parse(c);
                List<CarInteriorColorBridge> list = carInteriorColorBridge.ToList();
                list.Add(newBridge);
                carInteriorColorBridge = list;
            }
        }

        public int GetMakeByModel(int modelId)
        {
            var makeId = (from m in makes
                          join mod in models on m.MakeId equals mod.MakeId
                          where mod.ModelId == modelId
                          select m.MakeId).FirstOrDefault();

            return makeId;
        }

        public void DeleteCarExteriorBridgeEntries(int carId)
        {
            var bridge = (from e in carExteriorColorBridge
                          where e.CarId != carId
                          select e);
            carExteriorColorBridge = bridge;
        }

        public void DeleteCarInteriorBridgeEntries(int carId)
        {
            var bridge = (from i in carInteriorColorBridge
                          where i.CarId != carId
                          select i);
            carInteriorColorBridge = bridge;
        }

        public void EditCar(Car car)
        {
            var _cars = (from c in cars
                         where c.CarId != car.CarId
                         select c).ToList();

            _cars.Add(car);
            cars = _cars;
        }

        public void DeleteCar(int carId)
        {
            var _cars = (from c in cars
                         where c.CarId != carId
                         select c).ToList();
            cars = _cars;
        }

        public string GetImageFileName(int carId)
        {
            var fileName = (from c in cars
                           where c.CarId == carId
                           select c.ImageFileName).FirstOrDefault();

            return fileName;
        }

        //public IEnumerable<User> GetUsers()
        //{
        //    return users;
        //}

        //public IEnumerable<Role> GetRoles()
        //{
        //    return roles;
        //}

        //public User GetUserById(string id)
        //{
        //    var user = (from u in users
        //                where u.UserId == id
        //                select u).FirstOrDefault();
        //    return user;
        //}

        //public void SaveUser(User user)
        //{
        //    var _users = (from u in users
        //                 where u.UserId != user.UserId
        //                 select u).ToList();

        //    _users.Add(user);

        //    users = _users;
        //}

        //public void AddUser(User user)
        //{
        //    user.UserId = userIdCount.ToString();
        //    userIdCount++;
        //    List<User> list = users.ToList();
        //    list.Add(user);
        //    users = list;
        //}
    }
}

