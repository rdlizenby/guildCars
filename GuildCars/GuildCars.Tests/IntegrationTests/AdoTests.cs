using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using GuildCars.Data.ADO;
using System.Data.SqlClient;
using GuildCars.Models.AjaxRequest;
using GuildCars.Models.Tables;

namespace GuildCars.Tests.IntegrationTests
{
    [TestFixture]
    public class AdoTests
    {
        [SetUp]
        public void Setup()
        {
            using (SqlConnection cn = new SqlConnection("Server=localhost;Database=GuildCars;User Id=sa; Password=apprentice;"))
            {
                SqlCommand cmd = new SqlCommand("dbReset", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        [Test]
        public void CanGetHomeIndexVM()
        {
            var repo = new ADORepository();
            var model = repo.GetHomeIndexVM();
            var sample = model.CarShorts.ToList()[0];

            Assert.AreEqual(5, model.CarShorts.Count());
            Assert.AreEqual(4, model.Specials.Count());
            Assert.AreEqual(1, sample.CarId);
            Assert.AreEqual(2017, sample.Year);
            Assert.AreEqual("Honda", sample.Make);
            Assert.AreEqual("CRV", sample.Model);
            Assert.AreEqual(23500, sample.SalePrice);
            Assert.AreEqual("images/crv2017.jpg", sample.ImageFileName);
        }

        [Test]
        public void CanGetSpecials()
        {
            var repo = new ADORepository();
            var specials = repo.GetSpecials().ToList();
            var special = specials[0];

            Assert.AreEqual(4, specials.Count());
            Assert.AreEqual("0% APR, 60 Months", special.SpecialName);
            Assert.AreEqual("special1.jpg", special.ImageFileName);
            Assert.AreEqual("0% Annual Percentage Rate (APR) up to 36 months. 0% Annual Percentage Rate (APR) up to 60 months. 0.9% Annual Percentage Rate (APR) up to 66 months. 1.9% Annual Percentage Rate (APR) up to 72 months. Plus $500 Cash Back APR financing available, subject to credit approval..", special.SpecialDescription);
            Assert.AreEqual(new DateTime(2017, 6, 1), special.StartDate);
            Assert.AreEqual(new DateTime(2017, 12, 31), special.EndDate);
        }

        [Test]
        public void NewSearch()
        {
            var repo = new ADORepository();
            SearchRequest searchRequest = new SearchRequest
            {
                MaxPrice = "24000",
                MinPrice = "0",
                MaxYear = "20000",
                MinYear = "0",
                SearchTerm = ""
            };
            var result = repo.NewSearch(searchRequest).ToList();

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(1, result[0].CarId);
            Assert.AreEqual(2, result[1].CarId);

            searchRequest.MaxPrice = "2000000";
            searchRequest.MinPrice = "25000";
            searchRequest.MaxYear = "20000";
            searchRequest.MinYear = "0";
            searchRequest.SearchTerm = "";

            result = repo.NewSearch(searchRequest).ToList();

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(5, result[0].CarId);
            Assert.AreEqual(4, result[1].CarId);

            searchRequest.MaxPrice = "2000000";
            searchRequest.MinPrice = "0";
            searchRequest.MaxYear = "2017";
            searchRequest.MinYear = "0";
            searchRequest.SearchTerm = "";

            result = repo.NewSearch(searchRequest).ToList();

            Assert.AreEqual(3, result.Count());

            searchRequest.MaxPrice = "2000000";
            searchRequest.MinPrice = "0";
            searchRequest.MaxYear = "20000";
            searchRequest.MinYear = "2018";
            searchRequest.SearchTerm = "";

            result = repo.NewSearch(searchRequest).ToList();

            Assert.AreEqual(2, result.Count());

            searchRequest.MaxPrice = "2000000";
            searchRequest.MinPrice = "0";
            searchRequest.MaxYear = "20000";
            searchRequest.MinYear = "0";
            searchRequest.SearchTerm = "2017";

            result = repo.NewSearch(searchRequest).ToList();

            Assert.AreEqual(3, result.Count());

            searchRequest.SearchTerm = "Honda";

            result = repo.NewSearch(searchRequest).ToList();

            Assert.AreEqual(2, result.Count());

            searchRequest.SearchTerm = "Ho";

            result = repo.NewSearch(searchRequest).ToList();

            Assert.AreEqual(2, result.Count());

            searchRequest.SearchTerm = "nn";

            result = repo.NewSearch(searchRequest).ToList();

            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void CanGetBodyStyles()
        {
            var repo = new ADORepository();
            var bodyStyles = repo.GetBodyStyles();

            Assert.AreEqual(7, bodyStyles.Count());
            Assert.AreEqual("Crossover", bodyStyles[0].BodyStyleName);
        }

        [Test]
        public void CanGetRoles()
        {
            var repo = new ADORepository();
            var roles = repo.GetRoles().ToList();

            Assert.AreEqual(2, roles.Count());
            Assert.AreEqual("55faca68-569f-4bae-8d39-6fd9610b21dd", roles[0].DbId);
            Assert.AreEqual("admin", roles[0].RoleName);
        }

        [Test]
        public void CanGetPurchaseTypes()
        {
            var repo = new ADORepository();
            var purchaseTypes = repo.GetPurchaseTypes();

            Assert.AreEqual(3, purchaseTypes.Count());
            Assert.AreEqual("Bank Finance", purchaseTypes[0].PurchaseTypeName);
        }

        [Test]
        public void CanGetMakes()
        {
            var repo = new ADORepository();
            var makes = repo.GetMakes();

            Assert.AreEqual(4, makes.Count());
            Assert.AreEqual("Honda", makes[0].MakeName);
            Assert.AreEqual(new DateTime(2017, 10, 1), makes[0].DateAdded);
            Assert.AreEqual("9248ee20-2032-4c74-8455-14f69ab4a384", makes[0].AdminId);
        }

        [Test]
        public void CanGetModels()
        {
            var repo = new ADORepository();
            var models = repo.GetModels();

            Assert.AreEqual(8, models.Count());
            Assert.AreEqual("CRV", models[0].ModelName);
            Assert.AreEqual(1, models[0].MakeId);
            Assert.AreEqual(1, models[0].BodyStyleId);
            Assert.AreEqual(new DateTime(2017, 10, 1), models[0].DateAdded);
            Assert.AreEqual("9248ee20-2032-4c74-8455-14f69ab4a384", models[0].AddedBy);
        }

        [Test]
        public void CanGetTransmissions()
        {
            var repo = new ADORepository();
            var transmissions = repo.GetTransmissions();

            Assert.AreEqual(2, transmissions.Count());
            Assert.AreEqual("Automatic", transmissions[0].TransmissionType);
        }

        [Test]
        public void CanGetStates()
        {
            var repo = new ADORepository();
            var states = repo.GetStates();

            Assert.AreEqual(12, states.Count());
            Assert.AreEqual("Alabama", states[0].StateName);
        }

        [Test]
        public void CanGetExteriorColors()
        {
            var repo = new ADORepository();
            var exteriorColors = repo.GetExteriorColors();

            Assert.AreEqual(10, exteriorColors.Count());
            Assert.AreEqual("White", exteriorColors[0].ExteriorColorName);
        }

        [Test]
        public void CanGetInteriorColors()
        {
            var repo = new ADORepository();
            var interiorColors = repo.GetInteriorColors();

            Assert.AreEqual(6, interiorColors.Count());
            Assert.AreEqual("Grey", interiorColors[0].InteriorColorName);
        }

        [Test]
        public void CanGetCarById()
        {
            var repo = new ADORepository();
            var car= repo.GetCarById(1);

            Assert.AreEqual(1, car.CarId);
            Assert.AreEqual(1, car.ModelId);
            Assert.AreEqual(true, car.IsNew);
            Assert.AreEqual(1, car.TransmissionId);
            Assert.AreEqual(2017, car.Year);
            Assert.AreEqual(180, car.Milage);
            Assert.AreEqual("1G4GB5EG7AF110257", car.Vin);
            Assert.AreEqual(23500, car.SalePrice);
            Assert.AreEqual(24000, car.Msrp);
            Assert.AreEqual("This is a new Honda CRV", car.Description);
            Assert.AreEqual("images/crv2017.jpg", car.ImageFileName);
            Assert.AreEqual(false, car.IsSold);
            Assert.IsNull(car.PurchasePrice);
            Assert.IsNull(car.SaleDate);
            Assert.IsNull(car.SoldBy);
            Assert.IsNull(car.BuyerId);
            Assert.AreEqual(new DateTime(2017, 10, 1), car.AddedDate);
            Assert.AreEqual("9248ee20-2032-4c74-8455-14f69ab4a384", car.AddedBy);
            Assert.IsNull(car.PurchaseTypeId);
        }

        [Test]
        public void CanGetAllCars()
        {
            var repo = new ADORepository();
            var car = repo.GetAllCars();

            Assert.AreEqual(10, car.Count());
            Assert.AreEqual(1, car[0].CarId);
            Assert.AreEqual(1, car[0].ModelId);
            Assert.AreEqual(true, car[0].IsNew);
            Assert.AreEqual(1, car[0].TransmissionId);
            Assert.AreEqual(2017, car[0].Year);
            Assert.AreEqual(180, car[0].Milage);
            Assert.AreEqual("1G4GB5EG7AF110257", car[0].Vin);
            Assert.AreEqual(23500, car[0].SalePrice);
            Assert.AreEqual(24000, car[0].Msrp);
            Assert.AreEqual("This is a new Honda CRV", car[0].Description);
            Assert.AreEqual("images/crv2017.jpg", car[0].ImageFileName);
            Assert.AreEqual(false, car[0].IsSold);
            Assert.IsNull(car[0].PurchasePrice);
            Assert.IsNull(car[0].SaleDate);
            Assert.IsNull(car[0].SoldBy);
            Assert.IsNull(car[0].BuyerId);
            Assert.AreEqual(new DateTime(2017, 10, 1), car[0].AddedDate);
            Assert.AreEqual("9248ee20-2032-4c74-8455-14f69ab4a384", car[0].AddedBy);
            Assert.IsNull(car[0].PurchaseTypeId);
        }
    }
}
