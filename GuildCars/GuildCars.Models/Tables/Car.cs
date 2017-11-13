using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class Car
    {
        public int CarId { get; set; }
        public int ModelId { get; set; }
        public bool IsNew { get; set; }
        public int TransmissionId { get; set; }
        public int Year { get; set; }
        public int Milage { get; set; }
        public string Vin { get; set; }
        public int Msrp { get; set; }
        public int SalePrice { get; set; }
        public string Description { get; set; }
        public string ImageFileName { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsSold { get; set; }
        public int? BuyerId { get; set; }
        public int? PurchasePrice { get; set; }
        public string SoldBy { get; set; }
        public DateTime? SaleDate { get; set; }
        public DateTime AddedDate { get; set; }
        public string AddedBy { get; set; }
        public int? PurchaseTypeId { get; set; }
    }
}
