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
    public class BooksController : Controller
    {
        private Models.Database db = new Models.Database();

        // GET: Books
        public ActionResult Index()
        {
            var book = db.Books.ToList();

            var author = db.Authors.ToList();

            var BookView = new BookViewModel
            {
                Book = book,
                Author = author
            };
            return View(book);
        }

        // GET: Books/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.Books.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CombinedDataModels newBook)
        {
            if (ModelState.IsValid)
            {
                Books book = new Books()
                {
                    title = newBook.Books.title,
                    year = newBook.Books.year,
                    description = newBook.Books.description,
                    cover = newBook.Books.cover,
                    Author = new List<Authors>
                    {
                        new Authors()
                        {
                            firstName = newBook.Authors.firstName,
                            lastName = newBook.Authors.lastName,
                            birthDate = newBook.Authors.birthDate,
                            birthPlace = newBook.Authors.birthPlace,
                            BIO = newBook.Authors.BIO,
                            photo = newBook.Authors.photo,
                            sex = newBook.Authors.sex
                        }
                    },
                    Genre = new List<Genres>
                    {
                        new Genres()
                        {
                            genre = newBook.Genres.genre
                        }
                    },
                    Serie = new List<Series>
                    {
                        new Series()
                        {
                            series = newBook.Series.series
                        }
                    }
                };

                db.Books.Add(book);

                db.TrySaveChanges();

                return RedirectToAction("Index", "Books");
            }
            return View(newBook);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.Books.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,year,description,cover,isFavorite,isOnShelf,isOnWishList")] Books books)
        {
            if (ModelState.IsValid)
            {
                db.Entry(books).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(books);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Books books = db.Books.Find(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Books books = db.Books.Find(id);
            db.Books.Remove(books);
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
