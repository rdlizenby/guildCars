using GuildCars.Models.Tables;
using GuildCars.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models.ViewModels
{
    public class SalesReportVM
    {
        public List<SalesQuery> QueryResults { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UserId { get; set; }
    }
}