using GuildCars.Models.AjaxRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models.ViewModels
{
    public class InventoryVM
    {
        public List<InvSummary> UsedVehicles { get; set; }
        public List<InvSummary> NewVehicles { get; set; }
    }
}