using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpartanSpots.Models;

namespace SpartanSpots.Controllers
{
    public class ImageController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Image/
        [Authorize]
        public ActionResult Index()
        {
            var images = db.Images.Include(i => i.Business);
            return View(images.ToList());
        }

        //
        // GET: /Image/Details/5
        [Authorize]
        public ActionResult Details(int id = 0)
        {
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        //
        // GET: /Image/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.BusinessId = new SelectList(db.Businesses, "Id", "Name");
            return View();
        }

        //
        // POST: /Image/Create
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Image image)
        {
            if (ModelState.IsValid)
            {

                Image newImage = new Image();
                HttpPostedFileBase file = Request.Files["OriginalLocation"];
                newImage.Name = image.Name;
                newImage.Alt = image.Alt;
                newImage.BusinessId = image.BusinessId;
                if (file != null)
                    newImage.ContentType = file.ContentType;
                else
                    return RedirectToAction("Index");
                Int32 length = file.ContentLength;
                byte[] tempImage = new byte[length];
                file.InputStream.Read(tempImage, 0, length);
                newImage.Data = tempImage;
                db.Images.Add(newImage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(image);
        }
        [Authorize]
        public ActionResult GetImage(int id)
        {
            Image image = db.Images.Find(id);

            byte[] _image = image.Data;
            return File(_image, image.ContentType);

        }

        //
        // GET: /Image/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessId = new SelectList(db.Businesses, "Id", "Name", image.BusinessId);
            return View(image);
        }

        //
        // POST: /Image/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Image image)
        {
            if (ModelState.IsValid)
            {
                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusinessId = new SelectList(db.Businesses, "Id", "Name", image.BusinessId);
            return View(image);
        }

        //
        // GET: /Image/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Image image = db.Images.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        //
        // POST: /Image/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Image image = db.Images.Find(id);
            db.Images.Remove(image);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}