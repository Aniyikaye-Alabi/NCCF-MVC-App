using Microsoft.AspNetCore.Mvc;
using NCCF_MVC_App.Models;

namespace NCCF_MVC_App.Controllers
{
    public class PersonController : Controller
    {
        public ActionResult Person()
        {
            var person1 = new Person();
            person1.Name = "Asta";
            return View(person1);
            //return Redirect("Anime");
        }
        public ActionResult Anime()
        {
            return Content("Hello Black Clover");
        }

        public ActionResult Edit(int id)
        {
            return Content("ID:" + id);
        }

        public ActionResult PageSizeSortBy(int? PageSize, string? SortBy)
        {
            if (!PageSize.HasValue)
            {
                PageSize = 1;
            }
            if (string.IsNullOrWhiteSpace(SortBy))
            {
                SortBy = "Name";
            }
            return Content(String.Format("PageSize={0}&SortBy={1}", PageSize, SortBy));
        }

        public ActionResult PersonBirthday(int Year, int Month)
        {
            return Content($"{Year}/{Month}");
        }

    }
}
