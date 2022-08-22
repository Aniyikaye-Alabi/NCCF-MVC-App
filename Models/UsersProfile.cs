using System;
using System.Collections.Generic;

namespace NCCF_MVC_App.Models
{
    public partial class UsersProfile
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
