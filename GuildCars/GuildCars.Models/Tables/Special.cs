using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class Special
    {
        public int SpecialId { get; set; }
        public string SpecialName { get; set; }
        public string SpecialDescription { get; set; }
        public string ImageFileName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
