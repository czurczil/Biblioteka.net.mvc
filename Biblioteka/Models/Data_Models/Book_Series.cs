﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Biblioteka.Models.Data_Models
{
    public class Book_Series
    {
        public long id { get; set; }

        [ForeignKey("SeriesId")]
        public virtual Series Series { get; set; }
        public long SeriesId { get; set; }

        [ForeignKey("BookId")]
        public virtual Books Book { get; set; }
        public long BookId { get; set; }
    }
}