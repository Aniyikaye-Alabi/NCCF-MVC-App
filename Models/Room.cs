using System;
using System.Collections.Generic;

namespace NCCF_MVC_App.Models
{
    public partial class Room
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; } = null!;
        public int NoOfSpace { get; set; }
        public string? RoomCondition { get; set; }
        public int? EquipmentId { get; set; }
    }
}
