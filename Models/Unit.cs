using System;
using System.Collections.Generic;

namespace NCCF_MVC_App.Models
{
    public partial class Unit
    {
        public int UnitId { get; set; }
        public string Name { get; set; } = null!;
        public int? EquipmentId { get; set; }
    }
}
