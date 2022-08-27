using NCCF_MVC_App.Models;

namespace NCCF_MVC_App.DTOs
{
    public class MemberDto:Member
    {
        //public Member List<Mem> { get; set; }
        public string RoomName { get; set; }
        public string Name { get; set; }
    }
}
