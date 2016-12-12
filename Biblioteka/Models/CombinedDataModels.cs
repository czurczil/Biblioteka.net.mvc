using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Biblioteka.Models.Data_Models;

namespace Biblioteka.Models
{
    public class CombinedDataModels
    {
        public Authors Authors { get; set; }
        public Books Books { get; set; }
        public Genres Genres { get; set; }
        public Series Series { get; set; }

        public HttpPostedFileBase cover { get; set; }
        public HttpPostedFileBase photo { get; set; }
    }
    public class CombinedListDataModels
    {
        public List<Books> Books { get; set; }
        public List<Authors> Authors { get; set; }
        public List<Genres> Genres { get; set; }
        public List<Series> Series { get; set; }
    }
}