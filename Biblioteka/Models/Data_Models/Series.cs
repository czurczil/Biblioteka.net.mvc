using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Biblioteka.Models.Data_Models
{
    public class Series
    {
        public long id { get; set; }

        [DisplayName("Cykl")]
        public string series { get; set; }

        public virtual ICollection<Book_Series> Book_Series { get; set; }
    }
}