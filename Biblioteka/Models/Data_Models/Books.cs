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

        public virtual ICollection<Book_Authors> Book_Authors { get; set; }

        public virtual ICollection<Book_Genres> Book_Genres { get; set; }

        public virtual ICollection<Book_Series> Book_Series { get; set; }

        public virtual ICollection<User_Books> User_Books { get; set; }


    }
}