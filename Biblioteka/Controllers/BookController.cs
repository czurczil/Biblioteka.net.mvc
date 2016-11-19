using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biblioteka.Models;
using Biblioteka.Models.Data_Models;

namespace Biblioteka.Controllers
{
    public class BookController : Controller
    {
        private Models.Database db = new Models.Database();
        // GET: AddBook
        public ActionResult Index()
        {
            var books = db.Books.ToList();
            return View(books);            
        }

        [HttpGet]
        public ActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBook(CombinedDataModels newBook)
        {
            if (ModelState.IsValid)
            {
                Books book = new Books()
                {
                    title = newBook.Books.title,
                    year = newBook.Books.year,
                    description = newBook.Books.description,
                    cover = newBook.Books.cover
                };

                db.Books.Add(book);

                Authors author = new Authors()
                {
                    firstName = newBook.Authors.firstName,
                    lastName = newBook.Authors.lastName,
                    birthDate = newBook.Authors.birthDate,
                    birthPlace = newBook.Authors.birthPlace,
                    BIO = newBook.Authors.BIO,
                    photo = newBook.Authors.photo,
                    sex = newBook.Authors.sex
                };
                

                db.Authors.Add(author);

                db.TrySaveChanges();

                return RedirectToAction("Index", "Book");
            }
            return View(newBook);
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