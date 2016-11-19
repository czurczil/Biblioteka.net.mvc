using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteka.Models.Data_Models
{
    public class Genres
    {
        public long id { get; set; }
        public string genre { get; set; }

        public virtual ICollection<Book_Genres> Book_Genres { get; set; }
    }
}