using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

        [DisplayName("Ulubione")]
        public bool isFavorite { get; set; }
        public bool isRead { get; set; }
        public bool isOnWishList { get; set; }

        [Range(0, 10)]
        [DisplayName("Ocena")]
        public int rating { get; set; }
        
        [DisplayName("Komentarz")]
        public string comment { get; set; }
    }
}