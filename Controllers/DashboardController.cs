using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NCCF_MVC_App.Models;
using Newtonsoft.Json;

namespace NCCF_MVC_App.Controllers
{
    public class DashboardController : Controller
    {
        private readonly NCCF_DatabaseContext _context;

        public DashboardController(NCCF_DatabaseContext context)
        {
            _context = context;
        }


        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Index()
        {
            var UsersessionData = JsonConvert.DeserializeObject<UsersProfile>(HttpContext.Session.GetString("UserSession"));
            //if (UsersessionData == null)
            //{
            //    return Content("No session data found");

            //}
            return UsersessionData != null ?
                        View(UsersessionData) :
                        Problem("User profile  is null.");
        }
    }
}
