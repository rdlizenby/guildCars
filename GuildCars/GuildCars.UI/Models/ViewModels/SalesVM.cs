using GuildCars.Models.Tables;
using GuildCars.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models.ViewModels
{
    public class SalesVM
    {
        public CarDetailVM CarDetailVM { get; set; }
        public Buyer Buyer { get; set; }
        public IEnumerable<SelectListItem> States { get; set; }
        public IEnumerable<SelectListItem> PurchaseTypes { get; set; }
    }
}