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
            // You would actually do something more useful here.
            // This just fudges some results for displaying in the View.
            var model = db.Businesses.Where(x => x.Name.Contains(searchText));
            return View(model);        
        }
    }

}

