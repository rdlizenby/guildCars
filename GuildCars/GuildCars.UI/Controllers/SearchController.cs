using GuildCars.Data.Interface_and_Factory;
using GuildCars.Models.AjaxRequest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GuildCars.UI.Controllers
{
    public class SearchController : ApiController
    {
        [Route("search/new")]
        [AcceptVerbs("POST")]
        public IHttpActionResult SearchNew(SearchRequest searchRequest)
        {
            var repo = RepositoryFactory.GetRepository();
            return Ok(repo.NewSearch(searchRequest));
        }

        [Route("search/used")]
        [AcceptVerbs("POST")]
        public IHttpActionResult SearchUsed(SearchRequest searchRequest)
        {
            var repo = RepositoryFactory.GetRepository();
            return Ok(repo.UsedSearch(searchRequest));
        }

        [Route("x/y")]
        [AcceptVerbs("POST")]
        public IHttpActionResult SalesQuery (SalesQueryRequest query)
        {
            var repo = RepositoryFactory.GetRepository();
            if (query.EndDate == new DateTime(1, 1, 1))
                query.EndDate = DateTime.Now.AddDays(1);
            if (query.StartDate == new DateTime(1, 1, 1))
                query.StartDate = new DateTime(1950,1,1);
            var result = repo.GetSalesReport(query.UserId, query.StartDate, query.EndDate);
            return Ok(result);
        }

        [Route("search/sales")]
        [AcceptVerbs("POST")]
        public IHttpActionResult SearchSales(SearchRequest searchRequest)
        {
            var repo = RepositoryFactory.GetRepository();
            return Ok(repo.SalesSearch(searchRequest));
        }

        [Route("make/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetFilteredModels(int id)
        {
            var repo = RepositoryFactory.GetRepository();
            return Ok(repo.GetFilteredModels(id));
        }

        [Route("model/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetFilteredBodystyles(int id)
        {
            var repo = RepositoryFactory.GetRepository();
            return Ok(repo.GetBodystyle(id));
        }

        [Route("admin/deleteCar/{carId}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult DeleteCar(int carId)
        {
            var repo = RepositoryFactory.GetRepository();
            string filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/"), repo.GetImageFileName(carId));
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            repo.DeleteCar(carId);
            return Ok();
        }

        [Route("admin/deleteSpecial/{specialId}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult DeleteSpecial(int specialId)
        {
            var repo = RepositoryFactory.GetRepository();
            repo.DeleteSpecial(specialId);
            return Ok();
        }

    }
}
