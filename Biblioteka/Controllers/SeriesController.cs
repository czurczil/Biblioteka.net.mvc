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
    public class SeriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Series
        public ActionResult Index()
        {
            return View(db.Series.ToList());
        }

        // GET: Series/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Series series = db.Series.Find(id);
            if (series == null)
            {
                return HttpNotFound();
            }
            return View(series);
        }

        // GET: Series/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Series/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(long book_id,[Bind(Include ="id,series")] Series serie)
        {
            if (ModelState.IsValid)
            {
                long series_id;
                BooksController bc = new BooksController();

                if (bc.IsInDatabase(serie.series, null, 2) == false)
                {
                    db.Series.Add(serie);

                    db.SaveChanges();

                    series_id = serie.id;
                }
                else series_id = db.Series.Where(s => s.series == serie.series).Select(s => s.id).First();

                if (book_id != 0)
                {
                    Book_Series bs = new Book_Series()
                    {
                        BookId = book_id,
                        SeriesId = series_id
                    };

                    db.Book_Series.Add(bs);

                    db.TrySaveChanges();
                }

                return RedirectToAction("Details", "Books", new { id = book_id });
            }

            return View(serie);
        }

        // GET: Series/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Series series = db.Series.Find(id);
            if (series == null)
            {
                return HttpNotFound();
            }
            return View(series);
        }

        // POST: Series/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,series")] Series series)
        {
            if (ModelState.IsValid)
            {
                db.Entry(series).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(series);
        }

        // GET: Series/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Series series = db.Series.Find(id);
            if (series == null)
            {
                return HttpNotFound();
            }
            return View(series);
        }

        // POST: Series/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Series series = db.Series.Find(id);
            db.Series.Remove(series);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteRelation(long book_id, string series)
        {
            long id = db.Series.Where(s => s.series == series).Select(s => s.id).First();
            Book_Series bs = db.Book_Series.Where(s => s.SeriesId == id && s.BookId == book_id).First();
            db.Book_Series.Remove(bs);
            db.TrySaveChanges();
            return RedirectToAction("Details", "Books", new { id = book_id });
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
