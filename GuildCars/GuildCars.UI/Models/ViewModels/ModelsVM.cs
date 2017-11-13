using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models.ViewModels
{
    public class ModelsVM
    {
        public IEnumerable<SelectListItem> BodyStyles { get; set; }
        public IEnumerable<SelectListItem> Makes { get; set; }
        public List<ModelSummary> Models { get; set; }

        [Required]
        public string NewModel { get; set; }

        [Required]
        public int BodystyleId { get; set; }

        [Required]
        public int MakeId { get; set; }
    }
}