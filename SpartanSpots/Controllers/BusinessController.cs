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
            
            ViewBag.TopThreeResturaunts = this.topThreeResturaunts();
            ViewBag.TopThreeBars = this.topThreeBars();
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
      // [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
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

        [Authorize]
        [HttpGet]
        public String topThreeResturaunts()
        {
            return "Top resturaunts not implemented yet.";
        }

        [Authorize]
        [HttpGet]
        public String topThreeBars()
        {
            return "Top bar not implemented yet.";
        }

        [Authorize]
        [HttpGet]
        public string CalculateRating(int id)
        {
            String t = id.ToString();
            return t;
        }
        public ActionResult YourView(int id)
        {

            //pMomdel code here

            ViewBag.BlaResult = CalculateRating(id);
            return View();
        }

    }
}