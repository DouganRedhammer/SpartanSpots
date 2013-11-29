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

            var model = db.Businesses.Where(x => x.Category.Contains(searchText));
            return View(model);        
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

        [Authorize]
        [HttpGet]
        public ActionResult TopThreeRatedRestaurants()
        {
            var m =
            ViewData.Model = db.Businesses.Where(x => x.Category.Contains("Restaurant")).Take(1);
            return View(m);
        }

        [Authorize]
        [HttpGet]
        public ActionResult TopThreeRatedBars()
        {

            var model = db.Businesses.Where(x => x.Category.Contains("Bar")).Take(3);
            return View("TopThreeRatedBars");
        }
    }

}

