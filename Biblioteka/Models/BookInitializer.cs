using Biblioteka.Models.Data_Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Biblioteka.Models
{
    public class BookInitializer :DropCreateDatabaseIfModelChanges<Database>
    {
        protected override void Seed(Database context)
        {

        }
    }
}