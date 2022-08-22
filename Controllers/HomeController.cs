using Microsoft.AspNetCore.Mvc;
using NCCF_MVC_App.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace NCCF_MVC_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NCCF_DatabaseContext _context;

        public HomeController(ILogger<HomeController> logger, NCCF_DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }     

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UsersProfile user)
        {
            var data = _context.UsersProfiles.Find(2);
            if (data == null)
            {
                return Content("Username not found");
            }

            //if (user.UserName != data.UserName && user.Password != data.Password)
            //{
            //    return Content("Invalid username or password");
            //}
            HttpContext.Session.SetString("UserSession", JsonConvert.SerializeObject(data));
            return (user.UserName == data.UserName && user.Password == data.Password) ? RedirectToAction("Index", "Dashboard") : Content("Invalid username or password");

            //_context.Add(user);
            //_context.SaveChanges();
            //return RedirectToAction("Index", "Members");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}