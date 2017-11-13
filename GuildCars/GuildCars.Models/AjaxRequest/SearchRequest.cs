using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.AjaxRequest
{
    public class SearchRequest
    {
        public string SearchTerm { get; set; }
        public string MinPrice { get; set; }
        public string MaxPrice { get; set; }
        public string MinYear { get; set; }
        public string MaxYear { get; set; }
    }
}
