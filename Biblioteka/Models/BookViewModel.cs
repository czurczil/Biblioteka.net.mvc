using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Biblioteka.Models.Data_Models;

namespace Biblioteka.Models
{
    public class BookDetailsModel
    {
        public Books Book { get; set; }
        public List<BookDetailsList> Details { get; set; }

        public bool isFavorite { get; set; }
        public bool isRead { get; set; }
        public bool isOnWishList { get; set; }
        public int rating { get; set; }
        public string comment { get; set; }

        public HttpPostedFileBase cover { get; set; }
    }

    public class BookDetailsList
    {
        public Authors Author { get; set; }
        public Genres Genre { get; set; }
        public Series Series { get; set; }

        public bool AuthorChecked { get; set; }
        public bool GenreChecked { get; set; }
        public bool SeriesChecked { get; set; }

    }

    public class AuthorDetailsModel
    {
        public Authors Author { get; set; }
        public List<AuthorDetailsList> Books { get; set; }

        public HttpPostedFileBase photo { get; set; }
    }
    public class AuthorDetailsList
    {
        public Books Books { get; set; }
        public bool BooksChecked { get; set; }
    }
}