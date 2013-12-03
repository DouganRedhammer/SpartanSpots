using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using Newtonsoft.Json;
using SpartanSpots.Models;

namespace SpartanSpots.Controllers
{
    public class BusinessController : Controller
    {
        
        private UsersContext db = new UsersContext();

        //
        // GET: /Business/
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Businesses.ToList());
        }

        //
        // GET: /Business/Details/5
        [Authorize]
        public ActionResult Details(int id = 0)
        {
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        //
        // GET: /Business/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Business/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Business business)
        {
            if (ModelState.IsValid)
            {
                business.NumOfReviews = 0;
                business.TotalRating = 0.0;
                db.Businesses.Add(business);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(business);
        }

        //
        // GET: /Business/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        //
        // POST: /Business/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Business business)
        {
            if (ModelState.IsValid)
            {
                db.Entry(business).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(business);
        }

        //
        // GET: /Business/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Business business = db.Businesses.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        //
        // POST: /Business/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Business business = db.Businesses.Find(id);
            db.Businesses.Remove(business);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        [Authorize]
        [HttpGet]
        public string Search()
        {
            string name = Request["BusinessName"];
            var results = db.Businesses.Where(x => x.Name.Contains(name)).ToList();
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.Append("[");
            int numOfResults = results.Count;
            foreach (Business result in results)
            {
                sBuilder.Append("{\"BusinessId\":\"" + result.Id + "\",\"BusinessName\":\"" + result.Name +"\"}");
                if (numOfResults > 1)
                    sBuilder.Append(",");
                numOfResults--;
            }
            sBuilder.Append("]");
            string len = sBuilder.ToString();

            if (results.Count == 0)
                return "[{\"Status\":\"empty\"}]";
            else
                return sBuilder.ToString();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JsonListBusiness()
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
                x.TotalRating,
                x.NumOfReviews,
                x.Category
            }
                );
            return Json(business, "text/x-json", JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JsonTopThreeRatedRestaurants()
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
                x.TotalRating,
                x.NumOfReviews,
                x.Category
            }
                ).Where(x => x.Category.Contains("Restaurant")).OrderByDescending(x => x.TotalRating).Take(3);

            return Json(business, "text/x-json", JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JsonTopThreeRatedBars()
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
                x.TotalRating,
                x.NumOfReviews,
                x.Category
            }
                ).Where(x => x.Category.Contains("Bar")).OrderByDescending(x => x.TotalRating).Take(3);

            return Json(business, "text/x-json", JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JsonDetails(int id)
        {
            Business business = db.Businesses.Find(id);


            if (business != null)
                return Json(new
                {

                    name         = business.Name,
                    id           = business.Id,
                    phone        = business.PhoneNumber,
                    address      = business.Address,
                    city         = business.City,
                    state        = business.State,
                    zip          = business.ZipCode,
                    hours        = business.Hours,
                    totalRating  = business.TotalRating,
                    numOfReviews = business.NumOfReviews,
                    category     = business.Category

                }, "text/x-json", JsonRequestBehavior.AllowGet);

            else
            {
                return Json("Empty", "text/x-json", JsonRequestBehavior.AllowGet);
            }
        }
    }
}