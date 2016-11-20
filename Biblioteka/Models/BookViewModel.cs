using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Biblioteka.Models.Data_Models;

namespace Biblioteka.Models
{
    public class BookViewModel
    {
        public List<Books> Book { get; set; }
        public List<Authors> Author { get; set; }
        public List<Genres> Genre { get; set; }
        public List<Series> Series { get; set; }
    }
}