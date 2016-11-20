using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biblioteka.Models;
using Biblioteka.Models.Data_Models;

namespace Biblioteka.Controllers
{
    public class GenresController : Controller
    {
        private Models.Database db = new Models.Database();

        // GET: Genres
        public ActionResult Index()
        {
            return View(db.Genres.ToList());
        }

        // GET: Genres/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genres genres = db.Genres.Find(id);
            if (genres == null)
            {
                return HttpNotFound();
            }
            return View(genres);
        }

        // GET: Genres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,genre")] Genres genres)
        {
            if (ModelState.IsValid)
            {
                db.Genres.Add(genres);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(genres);
        }

        // GET: Genres/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genres genres = db.Genres.Find(id);
            if (genres == null)
            {
                return HttpNotFound();
            }
            return View(genres);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,genre")] Genres genres)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genres).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genres);
        }

        // GET: Genres/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genres genres = db.Genres.Find(id);
            if (genres == null)
            {
                return HttpNotFound();
            }
            return View(genres);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Genres genres = db.Genres.Find(id);
            db.Genres.Remove(genres);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
