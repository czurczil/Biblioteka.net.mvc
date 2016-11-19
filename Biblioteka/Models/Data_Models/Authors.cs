using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Biblioteka.Models.Data_Models
{
    public class Authors
    {
        public long id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime? birthDate { get; set; }
        public string sex { get; set; }
        public string birthPlace { get; set; }
        public string BIO { get; set; }
        public string photo { get; set; }

        public virtual ICollection<Books> Book { get; set; }
    }
}