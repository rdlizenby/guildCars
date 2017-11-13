using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.ViewModels
{
    public class CarDetailVM
    {
        public int CarId { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string BodyStyle { get; set; }
        public string TransmitionType { get; set; }
        public List<string> InteriorColor { get; set; }
        public List<string> ExteriorColor { get; set; }
        public int Milage { get; set; }
        public string Vin { get; set; }
        public int SalePrice { get; set; }
        public int Msrp { get; set; }
        public string Description { get; set; }
        public string ImageFileName { get; set; }
        public bool IsSold { get; set; }
        public int PurchaseTypeId { get; set; }
        public int PurchasePrice { get; set; }
    }
}
