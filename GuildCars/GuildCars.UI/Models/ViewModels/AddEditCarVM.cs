using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models.ViewModels
{
    public class AddEditCarVM
    {
        public Car Car { get; set; }
        public string Make { get; set; }
        public string Type { get; set; }
        public IEnumerable<SelectListItem> Makes { get; set; }
        public IEnumerable<SelectListItem> Models { get; set; }
        public IEnumerable<SelectListItem> BodyStyles { get; set; }
        public IEnumerable<SelectListItem> Transmissions { get; set; }
        public IEnumerable<SelectListItem> InteriorColors { get; set; }
        public IEnumerable<SelectListItem> ExteriorColors { get; set; }
        public HttpPostedFileBase UploadedFile { get; set; }
        public string[] ExteriorColorIds { get; set; }
        public string[] InteriorColorIds { get; set; }
    }
}