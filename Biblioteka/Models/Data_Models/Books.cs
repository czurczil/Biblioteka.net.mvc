using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteka.Models.Data_Models
{
    public class Books
    {
        public long id { get; set; }
        public string title { get; set; }
        public int year { get; set; }
        public string description { get; set; }
        public string cover { get; set; }
        public bool isFavorite { get; set; }
        public bool isOnShelf { get; set; }
        public bool isOnWishList { get; set; }

        public virtual ICollection<Authors> Author { get; set; }

        public virtual ICollection<Genres> Genre { get; set; }

        public virtual ICollection<Series> Serie { get; set; }


    }
}