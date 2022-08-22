using System;
using System.Collections.Generic;

namespace NCCF_MVC_App.Models
{
    public partial class Member
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; } = null!;
        public int RoomId { get; set; }
        public int Age { get; set; }
        public int UnitId { get; set; }
        public string? PostHeld { get; set; }
    }
}
