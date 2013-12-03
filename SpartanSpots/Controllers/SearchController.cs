using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpartanSpots.Models;

namespace SpartanSpots.Controllers
{
    public class SearchController : Controller
    {
        private UsersContext db = new UsersContext();
        //
        // GET: /Search/
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Results(string searchText)
        {
            ViewBag.SearchText = searchText;
            var model = db.Businesses.Where(x => x.Category.Contains(searchText));
            return View(model);        
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult JsonSearchBusiness(string searchText)
        {
            var business = db.Businesses.Select(x => new
            {
                x.Name,
                x.Id,
                x.PhoneNumber,
                x.Address,
                x.City,
                x.State,
                x.ZipCode,
                x.Hours,
                x.Category,
                x.TotalRating,
                x.NumOfReviews
            }
                ).Where(n => n.Category.Contains(searchText));
            return Json(business, "text/x-json", JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public ActionResult ListRestaurants()
        {

            var model = db.Businesses.Where(x => x.Category.Contains("Restaurant"));
            return View(model);
        }
        [Authorize]
        [HttpGet]
        public ActionResult ListBars()
        {

            var model = db.Businesses.Where(x => x.Category.Contains("Bar"));
            return View(model);
        }

    }

}

