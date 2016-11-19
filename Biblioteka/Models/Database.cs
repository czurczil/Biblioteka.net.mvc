using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Biblioteka.Models.Data_Models;
using System.Data.Entity.Infrastructure;

namespace Biblioteka.Models
{
    public class Database: DbContext
    {
        public DbSet<Books> Books { get; set; }
        public DbSet<Authors> Authors { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<Series> Series { get; set; }

        public Database() : base("name=DefaultConnection") { }

        public bool TrySaveChanges()
        {
            try
            {
                SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

    }
}