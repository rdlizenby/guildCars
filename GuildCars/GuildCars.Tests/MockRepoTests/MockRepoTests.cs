using GuildCars.Data;
using GuildCars.Models.AjaxRequest;
using GuildCars.Models.Tables;
using GuildCars.Models.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Tests.MockRepoTests
{
    [TestFixture]
    public class MockRepoTests
    {
        [Test]
        public void CanGetCarDetailsById()
        {
            var repo = new GuildCarsMockRepository();
            var carDetails = repo.GetDetailsVM(1);

            Assert.AreEqual(1, carDetails.CarId);
            Assert.AreEqual(2017, carDetails.Year);
            Assert.AreEqual("Honda", carDetails.Make);
            Assert.AreEqual("CRV", carDetails.Model);
            Assert.AreEqual("Crossover", carDetails.BodyStyle);
            Assert.AreEqual("Automatic", carDetails.TransmitionType);
            Assert.AreEqual("Grey", carDetails.InteriorColor[0]);
            Assert.AreEqual("White", carDetails.ExteriorColor[0]);
            Assert.AreEqual(180, carDetails.Milage);
            Assert.AreEqual("1G4GB5EG7AF110257", carDetails.Vin);
            Assert.AreEqual(23500, carDetails.SalePrice);
            Assert.AreEqual(24000, carDetails.Msrp);
            Assert.AreEqual("This is a new Honda CRV", carDetails.Description);
            Assert.AreEqual("images/crv2017.jpg", carDetails.ImageFileName);
            Assert.AreEqual(false, carDetails.IsSold);
        }

        [Test]
        public void CanGetHomeIndexVM()
        {
            var repo = new GuildCarsMockRepository();
            var vm = repo.GetHomeIndexVM();
            List<Special> vmSpecials = vm.Specials.ToList();
            List<CarShortList> vmCars = vm.CarShorts.ToList();

            Assert.AreEqual(5, vm.Specials.Count());
            Assert.AreEqual("0% APR, 60 Months", vmSpecials[0].SpecialName);
            Assert.AreEqual("special1.jpg", vmSpecials[0].ImageFileName);
            Assert.AreEqual("0% Annual Percentage Rate (APR) up to 36 months. 0% Annual Percentage Rate (APR) up to 60 months. 0.9% Annual Percentage Rate (APR) up to 66 months. 1.9% Annual Percentage Rate (APR) up to 72 months. Plus $500 Cash Back APR financing available, subject to credit approval..", vmSpecials[0].SpecialDescription);
            Assert.AreEqual(new DateTime(2017, 6, 1), vmSpecials[0].StartDate);
            Assert.AreEqual(new DateTime(2017, 12, 31), vmSpecials[0].EndDate);

            Assert.AreEqual(5, vmCars.Count());
            Assert.AreEqual(1, vmCars[0].CarId);
            Assert.AreEqual(2017, vmCars[0].Year);
            Assert.AreEqual("Honda", vmCars[0].Make);
            Assert.AreEqual("CRV", vmCars[0].Model);
            Assert.AreEqual(23500, vmCars[0].SalePrice);
            Assert.AreEqual("images/crv2017.jpg", vmCars[0].ImageFileName);
        }

        [Test]
        public void CanSearchNewByOnlySearchTerm()
        {
            var repo = new GuildCarsMockRepository();
            var searchRequest = new SearchRequest();

            searchRequest.SearchTerm = "honda";
            searchRequest.MinPrice = "0";
            searchRequest.MaxPrice = "200000";
            searchRequest.MinYear = "2000";
            searchRequest.MaxYear = "20000";

            var results = repo.NewSearch(searchRequest);
            Assert.AreEqual(2, results.Count());

            searchRequest.SearchTerm = "hon";
            results = repo.NewSearch(searchRequest);
            Assert.AreEqual(2, results.Count());

            searchRequest.SearchTerm = "Hondai";
            results = repo.NewSearch(searchRequest);
            Assert.IsEmpty(results);

            searchRequest.SearchTerm = "Accord";
            results = repo.NewSearch(searchRequest);
            Assert.AreEqual(1, results.Count());

            searchRequest.SearchTerm = "c";
            results = repo.NewSearch(searchRequest);
            Assert.AreEqual(3, results.Count());

            searchRequest.SearchTerm = "2017";
            results = repo.NewSearch(searchRequest);
            Assert.AreEqual(3, results.Count());

            searchRequest.SearchTerm = "201";
            results = repo.NewSearch(searchRequest);
            Assert.AreEqual(0, results.Count());
        }

        [Test]
        public void CanSearchUsedByOnlySearchTerm()
        {
            var repo = new GuildCarsMockRepository();
            var searchRequest = new SearchRequest();

            searchRequest.SearchTerm = "chevrolet";
            searchRequest.MinPrice = "0";
            searchRequest.MaxPrice = "200000";
            searchRequest.MinYear = "2000";
            searchRequest.MaxYear = "20000";

            var results = repo.UsedSearch(searchRequest);
            Assert.AreEqual(2, results.Count());

            searchRequest.SearchTerm = "chev";
            results = repo.UsedSearch(searchRequest);
            Assert.AreEqual(2, results.Count());

            searchRequest.SearchTerm = "Chevrolette";
            results = repo.UsedSearch(searchRequest);
            Assert.IsEmpty(results);

            searchRequest.SearchTerm = "Malibu";
            results = repo.UsedSearch(searchRequest);
            Assert.AreEqual(1, results.Count());

            searchRequest.SearchTerm = "v";
            results = repo.UsedSearch(searchRequest);
            Assert.AreEqual(3, results.Count());

            searchRequest.SearchTerm = "2015";
            results = repo.UsedSearch(searchRequest);
            Assert.AreEqual(2, results.Count());

            searchRequest.SearchTerm = "201";
            results = repo.UsedSearch(searchRequest);
            Assert.AreEqual(0, results.Count());
        }

        [Test]
        public void CanSearchNewByOnlyMinPrice()
        {
            var repo = new GuildCarsMockRepository();
            var searchRequest = new SearchRequest();

            searchRequest.SearchTerm = "";
            searchRequest.MinPrice = "25000";
            searchRequest.MaxPrice = "200000";
            searchRequest.MinYear = "2000";
            searchRequest.MaxYear = "20000";

            var results = repo.NewSearch(searchRequest);
            Assert.AreEqual(2, results.Count());
        }

        [Test]
        public void CanSearchNewByOnlyMaxPrice()
        {
            var repo = new GuildCarsMockRepository();
            var searchRequest = new SearchRequest();

            searchRequest.SearchTerm = "";
            searchRequest.MinPrice = "0";
            searchRequest.MaxPrice = "25000";
            searchRequest.MinYear = "2000";
            searchRequest.MaxYear = "20000";

            var results = repo.NewSearch(searchRequest);
            Assert.AreEqual(3, results.Count());
        }

        [Test]
        public void CanSearchNewByOnlyMinYear()
        {
            var repo = new GuildCarsMockRepository();
            var searchRequest = new SearchRequest();

            searchRequest.SearchTerm = "";
            searchRequest.MinPrice = "0";
            searchRequest.MaxPrice = "200000";
            searchRequest.MinYear = "2018";
            searchRequest.MaxYear = "20000";

            var results = repo.NewSearch(searchRequest);
            Assert.AreEqual(2, results.Count());
        }

        [Test]
        public void CanSearchNewByOnlyMaxYear()
        {
            var repo = new GuildCarsMockRepository();
            var searchRequest = new SearchRequest();

            searchRequest.SearchTerm = "";
            searchRequest.MinPrice = "0";
            searchRequest.MaxPrice = "200000";
            searchRequest.MinYear = "0";
            searchRequest.MaxYear = "2017";

            var results = repo.NewSearch(searchRequest);
            Assert.AreEqual(3, results.Count());
        }

        [Test]
        public void CanSearchOldByOnlyMinPrice()
        {
            var repo = new GuildCarsMockRepository();
            var searchRequest = new SearchRequest();

            searchRequest.SearchTerm = "";
            searchRequest.MinPrice = "25000";
            searchRequest.MaxPrice = "200000";
            searchRequest.MinYear = "2000";
            searchRequest.MaxYear = "20000";

            var results = repo.UsedSearch(searchRequest);
            Assert.AreEqual(2, results.Count());
        }

        [Test]
        public void CanSearchOldByOnlyMaxPrice()
        {
            var repo = new GuildCarsMockRepository();
            var searchRequest = new SearchRequest();

            searchRequest.SearchTerm = "";
            searchRequest.MinPrice = "0";
            searchRequest.MaxPrice = "25000";
            searchRequest.MinYear = "2000";
            searchRequest.MaxYear = "20000";

            var results = repo.UsedSearch(searchRequest);
            Assert.AreEqual(3, results.Count());
        }

        [Test]
        public void CanSearchOldByOnlyMinYear()
        {
            var repo = new GuildCarsMockRepository();
            var searchRequest = new SearchRequest();

            searchRequest.SearchTerm = "";
            searchRequest.MinPrice = "0";
            searchRequest.MaxPrice = "200000";
            searchRequest.MinYear = "2013";
            searchRequest.MaxYear = "20000";

            var results = repo.UsedSearch(searchRequest);
            Assert.AreEqual(3, results.Count());
        }

        [Test]
        public void CanSearchOldByOnlyMaxYear()
        {
            var repo = new GuildCarsMockRepository();
            var searchRequest = new SearchRequest();

            searchRequest.SearchTerm = "";
            searchRequest.MinPrice = "0";
            searchRequest.MaxPrice = "200000";
            searchRequest.MinYear = "0";
            searchRequest.MaxYear = "2013";

            var results = repo.UsedSearch(searchRequest);
            Assert.AreEqual(3, results.Count());
        }

        [Test]
        public void CanPostContactRequest()
        {
            var repo = new GuildCarsMockRepository();
            ContactRequest contactRequest = new ContactRequest();

            contactRequest.Email = "testing@gmail.com";
            contactRequest.Name = "Test Testerson";
            contactRequest.Message = "Testing 1, 2, 3";

            repo.PostContactRequest(contactRequest);
            List<ContactRequest> contacts = repo.GetContacts();
            var contact = (from c in contacts
                           where c.ContactRequestId == 5
                           select c).FirstOrDefault();

            Assert.AreEqual(5, contacts.Count());
            Assert.AreEqual( "testing@gmail.com", contact.Email);
            Assert.IsNull(contact.Phone);
            Assert.AreEqual("Test Testerson", contact.Name);
        }
    }
}
