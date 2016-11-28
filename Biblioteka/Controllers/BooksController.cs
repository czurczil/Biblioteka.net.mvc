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
    public class BooksController : Controller
    {
        private Models.Database db = new Models.Database();

        // GET: Books
        public ActionResult Index()
        {
             var BookView = new BookViewModel
            {
                Book = db.Books.ToList(),
                Author = db.Authors.ToList(),
                Genre = db.Genres.ToList(),
                Series = db.Series.ToList()
            };

            return View(BookView);
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
                long book_id;
                long author_id;
                long genre_id;
                long series_id;                
                //***************************check if title, author, genre and series are in database************************
                if (IsInDatabase(newBook.Books.title, null, 0) == false)
                {
                    Books book;
                    if (newBook.cover == null)
                        book = new Books()
                        {
                            title = newBook.Books.title,
                            year = newBook.Books.year,
                            description = newBook.Books.description,
                            cover = null
                        };
                    else
                    {
                        book = new Books()
                        {
                            title = newBook.Books.title,
                            year = newBook.Books.year,
                            description = newBook.Books.description,
                            cover = newBook.cover.FileName
                        };

                        newBook.cover.SaveAs(HttpContext.Server.MapPath(ConfigurationManager.AppSettings["bookCovers"]) + book.cover);
                    }

                    db.Books.Add(book);

                    db.TrySaveChanges();

                    book_id = book.id;
                }
                else book_id = db.Books.Where(b => b.title == newBook.Books.title).Select(b => b.id).First();

                if (IsInDatabase(newBook.Authors.firstName, newBook.Authors.lastName, 3) == false) {
                    Authors author;
                    if (newBook.photo == null)
                        author = new Authors()
                        {
                            firstName = newBook.Authors.firstName,
                            lastName = newBook.Authors.lastName,
                            birthDate = newBook.Authors.birthDate,
                            birthPlace = newBook.Authors.birthPlace,
                            BIO = newBook.Authors.BIO,
                            photo = null,
                            sex = newBook.Authors.sex
                        };
                    else
                    {
                        author = new Authors()
                        {
                            firstName = newBook.Authors.firstName,
                            lastName = newBook.Authors.lastName,
                            birthDate = newBook.Authors.birthDate,
                            birthPlace = newBook.Authors.birthPlace,
                            BIO = newBook.Authors.BIO,
                            photo = newBook.photo.FileName,
                            sex = newBook.Authors.sex
                        };

                        newBook.photo.SaveAs(HttpContext.Server.MapPath(ConfigurationManager.AppSettings["authorPhotos"]) + author.photo);
                    }

                    db.Authors.Add(author);

                    db.TrySaveChanges();

                    author_id = author.id;
                }
                else author_id = db.Authors.Where(a => a.firstName == newBook.Authors.firstName && a.lastName == newBook.Authors.lastName).Select(a => a.id).First();

                if (IsInDatabase(newBook.Genres.genre, null, 1) == false) {
                    Genres genre = new Genres()
                    {
                        genre = newBook.Genres.genre
                    };

                    db.Genres.Add(genre);

                    db.TrySaveChanges();

                    genre_id = genre.id;
                }
                else genre_id = db.Genres.Where(g => g.genre == newBook.Genres.genre).Select(g => g.id).First();

                if (IsInDatabase(newBook.Series.series, null, 2) == false)
                {
                    Series series = new Series()
                    {
                        series = newBook.Series.series
                    };

                    db.Series.Add(series);

                    db.TrySaveChanges();

                    series_id = series.id;
                }
                else series_id = db.Series.Where(s => s.series == newBook.Series.series).Select(s => s.id).First();

                if (BookIsBounded(book_id, author_id, 0) == false)
                {
                    Book_Authors bk = new Book_Authors()
                    {
                        BookId = book_id,
                        AuthorId = author_id
                    };

                    db.Book_Authors.Add(bk);
                }

                if (BookIsBounded(book_id, genre_id, 1) == false)
                {
                    Book_Genres bg = new Book_Genres()
                    {
                        BookId = book_id,
                        GenreId = genre_id
                    };

                    db.Book_Genres.Add(bg);
                }

                if (BookIsBounded(book_id, series_id, 2) == false)
                {
                    Book_Series bs = new Book_Series()
                    {
                        BookId = book_id,
                        SeriesId = series_id
                    };

                    db.Book_Series.Add(bs);
                }

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

            var editModel = new CombinedDataModels
            {
                Books = db.Books.Find(id)
            };
            return View(editModel);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CombinedDataModels editedBook)
        {
            if (ModelState.IsValid)
            {
                var books = db.Books.Find(editedBook.Books.id);
                books.title = editedBook.Books.title;
                books.year = editedBook.Books.year;
                books.description = editedBook.Books.description;
                books.isFavorite = editedBook.Books.isFavorite;
                books.isOnShelf = editedBook.Books.isOnShelf;
                books.isOnWishList = editedBook.Books.isOnWishList;
                if(editedBook.cover != null)
                {
                    books.cover = editedBook.cover.FileName;
                    editedBook.cover.SaveAs(HttpContext.Server.MapPath(ConfigurationManager.AppSettings["bookCovers"]) + books.cover);
                }
                

                db.Entry(books).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editedBook);
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

        public bool IsInDatabase(string data, string data2, int n)
        {
            using (var context = new Models.Database())
            {
                if (n == 0)
                {
                   if(context.Books.Any(b => b.title == data))
                        return true;
                    else return false;
                }
                else if (n == 1)
                {
                    if(context.Genres.Any(g => g.genre == data))
                        return true;
                    else return false;
                }
                else if (n == 2)
                {
                    if(context.Series.Any(s => s.series == data))
                        return true;
                    else return false;
                }
                else
                {
                    if(context.Authors.Any(a => a.firstName == data && a.lastName == data2))
                        return true;
                    else return false;
                }
            }
        }

        public bool BookIsBounded(long book_id, long second_id, int n)
        {
            using (var context = new Models.Database())
            {
                if (n == 0)
                {
                    if (context.Book_Authors.Any(ba => ba.BookId == book_id && ba.AuthorId == second_id))
                        return true;
                    else return false;
                }
                else if (n == 1)
                {
                    if (context.Book_Genres.Any(bg => bg.BookId == book_id && bg.GenreId == second_id))
                        return true;
                    else return false;
                }
                else
                {
                    if (context.Book_Series.Any(bs => bs.BookId == book_id && bs.SeriesId == second_id))
                        return true;
                    else return false;
                }
            }

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
