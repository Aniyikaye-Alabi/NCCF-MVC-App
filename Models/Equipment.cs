using System;
using System.Collections.Generic;

namespace NCCF_MVC_App.Models
{
    public partial class Equipment
    {
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; } = null!;
        public int UnitId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateExpired { get; set; }
    }
}
