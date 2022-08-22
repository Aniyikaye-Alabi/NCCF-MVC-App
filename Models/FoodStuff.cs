using System;
using System.Collections.Generic;

namespace NCCF_MVC_App.Models
{
    public partial class FoodStuff
    {
        public int FoodStuffId { get; set; }
        public string Name { get; set; } = null!;
        public decimal? Price { get; set; }
        public int? QuantityAvailable { get; set; }
        public int? EquipmentId { get; set; }
        public int? UnitId { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
