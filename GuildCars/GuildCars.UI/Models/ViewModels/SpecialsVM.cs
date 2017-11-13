using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models.ViewModels
{
    public class SpecialsVM
    {
        public List<Special> Specials { get; set; }
        public Special NewSpecial { get; set; }

        [Required(ErrorMessage ="You must upload a banner image with the special")]
        public HttpPostedFileBase UploadedFile { get; set; }
    }
}