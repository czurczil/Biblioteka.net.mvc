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
using System.Configuration;

namespace Biblioteka.Controllers
{
    public class AuthorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Authors
        public ActionResult Index(string searchAuthor, string Sort)
        {
            var authors = from a in db.Authors select a;

            var sorting = new List<string>() { "Imię: alfabetycznie", "Imię: niealfabatycznie", "Nazwisko: alfabetycznie", "Nazwisko: niealfabatycznie" };
            ViewBag.Sort = new SelectList(sorting);

            if (!String.IsNullOrEmpty(searchAuthor))
            {
                authors = authors.Where(a => a.firstName.Contains(searchAuthor) || a.lastName.Contains(searchAuthor)
                || (a.firstName + " " + a.lastName).Contains(searchAuthor) || (a.lastName + " " + a.firstName).Contains(searchAuthor));
            }

            if(!String.IsNullOrEmpty(Sort))
            {
                switch (Sort)
                {
                    case "Imię: alfabetycznie":
                        authors = authors.OrderBy(a => a.firstName);
                        break;
                    case "Imię: niealfabatycznie":
                        authors = authors.OrderByDescending(a => a.firstName);
                        break;
                    case "Nazwisko: alfabetycznie":
                        authors = authors.OrderBy(a => a.lastName);
                        break;
                    case "Nazwisko: niealfabatycznie":
                        authors = authors.OrderByDescending(a => a.lastName);
                        break;
                    default:
                        break;
                }
            }

            return View(authors);
        }

        // GET: Authors/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authors authors = db.Authors.Find(id);
            if (authors == null)
            {
                return HttpNotFound();
            }

            var viewModel = new AuthorDetailsModel();

            viewModel.Author = new Authors
            {
                id = authors.id,
                firstName = authors.firstName,
                lastName = authors.lastName,
                birthDate = authors.birthDate,
                sex = authors.sex,
                birthPlace = authors.birthPlace,
                BIO = authors.BIO,
                photo = authors.photo
            };

            var details = RelatedData(id);

            viewModel.Books = details;

            return View(viewModel);
        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(long book_id,[Bind(Include = "id,firstName,lastName,birthDate,sex,birthPlace,BIO,photo")]Authors authors)
        {
            if (ModelState.IsValid)
            {
                long author_id;
                BooksController bc = new BooksController();

                if (bc.IsInDatabase(authors.firstName, authors.lastName, 3) == false)
                {
                    db.Authors.Add(authors);

                    db.SaveChanges();

                    author_id = authors.id;
                }
                else author_id = db.Authors.Where(a => a.firstName == authors.firstName && a.lastName == authors.lastName).Select(a => a.id).First();

                if (book_id != 0)
                {
                    Book_Authors bk = new Book_Authors()
                    {
                        BookId = book_id,
                        AuthorId = author_id
                    };

                    db.Book_Authors.Add(bk);

                    db.TrySaveChanges();
                }

                return RedirectToAction("Details", "Books", new { id = book_id });
            }

            return View(authors);
        }

        // GET: Authors/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authors authors = db.Authors.Find(id);
            if (authors == null)
            {
                return HttpNotFound();
            }
            var editModel = new AuthorDetailsModel();

            editModel.Author = new Authors
            {
                id = authors.id,
                firstName = authors.firstName,
                lastName = authors.lastName,
                birthDate = authors.birthDate,
                sex = authors.sex,
                birthPlace = authors.birthPlace,
                BIO = authors.BIO,
                photo = authors.photo
            };

            var details = RelatedData(id);

            editModel.Books = details;

            return View(editModel);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AuthorDetailsModel editedAuthor)
        {
            if (ModelState.IsValid)
            {
                var authors = db.Authors.Find(editedAuthor.Author.id);

                authors.firstName = editedAuthor.Author.firstName;
                authors.lastName = editedAuthor.Author.lastName;
                authors.birthDate = editedAuthor.Author.birthDate;
                authors.birthPlace = editedAuthor.Author.birthPlace;
                authors.BIO = editedAuthor.Author.BIO;
                authors.sex = editedAuthor.Author.sex;

                if (editedAuthor.photo != null)
                {
                    authors.photo = editedAuthor.photo.FileName;
                    editedAuthor.photo.SaveAs(HttpContext.Server.MapPath(ConfigurationManager.AppSettings["authorPhotos"]) + authors.photo);
                }

                db.Entry(authors).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editedAuthor);
        }

        // GET: Authors/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authors authors = db.Authors.Find(id);
            if (authors == null)
            {
                return HttpNotFound();
            }
            return View(authors);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Authors authors = db.Authors.Find(id);
            db.Authors.Remove(authors);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteRelation(long book_id, string fname, string lname)
        {
            long id = db.Authors.Where(a => a.firstName == fname && a.lastName == lname).Select(a => a.id).First();
            Book_Authors ba = db.Book_Authors.Where(a => a.AuthorId == id && a.BookId == book_id).First();
            db.Book_Authors.Remove(ba);
            db.TrySaveChanges();
            return RedirectToAction("Details", "Books", new { id = book_id });
        }

        public List<AuthorDetailsList> RelatedData(long? id)
        {
            var details = new List<AuthorDetailsList>();

            var books = db.Books.ToList();
            var book_authors = db.Book_Authors.ToList();

            foreach (var b in books)
                foreach (Book_Authors ba in book_authors)
                    if (b.id == ba.BookId && id == ba.AuthorId)
                            details.Add(new AuthorDetailsList { Books = new Books { title = b.title, cover = b.cover }, BooksChecked = true });

            return details;
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
