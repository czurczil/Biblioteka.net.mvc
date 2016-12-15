using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Biblioteka.Models.Data_Models
{
    public class User_Books
    {
        public long id { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }
       // public string UserId { get; set; }

        [ForeignKey("BookId")]
        public virtual Books Book { get; set; }
        public long BookId { get; set; }

        public bool isFavorite { get; set; }
        public bool isRead { get; set; }
        public bool isOnWishList { get; set; }
    }
}