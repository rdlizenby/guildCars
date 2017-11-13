using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace GuildCars.UI.Models.ViewModels
{
    public class MakeVM
    {
        [Required]
        public string NewMake { get; set; }
        public List<MakeSummary> Makes { get; set; }
    }
}