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
        private Models.Database db = new Models.Database();

        // GET: Authors
        public ActionResult Index()
        {
            return View(db.Authors.ToList());
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
            return View(authors);
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
        public ActionResult Create([Bind(Include = "id,firstName,lastName,birthDate,sex,birthPlace,BIO,photo")] Authors authors)
        {
            if (ModelState.IsValid)
            {
                db.Authors.Add(authors);
                db.SaveChanges();
                return RedirectToAction("Index");
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
            var editModel = new CombinedDataModels
            {
                Authors = db.Authors.Find(id)
            };
            return View(editModel);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CombinedDataModels editedAuthor)
        {
            if (ModelState.IsValid)
            {
                var authors = db.Authors.Find(editedAuthor.Authors.id);

                authors.firstName = editedAuthor.Authors.firstName;
                authors.lastName = editedAuthor.Authors.lastName;
                authors.birthDate = editedAuthor.Authors.birthDate;
                authors.birthPlace = editedAuthor.Authors.birthPlace;
                authors.BIO = editedAuthor.Authors.BIO;
                authors.sex = editedAuthor.Authors.sex;

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
