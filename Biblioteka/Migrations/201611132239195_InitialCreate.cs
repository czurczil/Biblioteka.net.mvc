namespace Biblioteka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        firstName = c.String(),
                        lastName = c.String(),
                        birthDate = c.DateTime(nullable: false),
                        sex = c.String(),
                        birthPlace = c.String(),
                        BIO = c.String(),
                        photo = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Book_Authors",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        AuthorId = c.Long(nullable: false),
                        BookId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.AuthorId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        title = c.String(),
                        year = c.Int(nullable: false),
                        description = c.String(),
                        cover = c.String(),
                        isFavorite = c.Boolean(nullable: false),
                        isOnShelf = c.Boolean(nullable: false),
                        isOnWishList = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Book_Genres",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        GenreId = c.Long(nullable: false),
                        BookId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.GenreId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        genre = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Book_Series",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        SeriesId = c.Long(nullable: false),
                        BookId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Series", t => t.SeriesId, cascadeDelete: true)
                .Index(t => t.SeriesId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Series",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        series = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Book_Series", "SeriesId", "dbo.Series");
            DropForeignKey("dbo.Book_Series", "BookId", "dbo.Books");
            DropForeignKey("dbo.Book_Genres", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Book_Genres", "BookId", "dbo.Books");
            DropForeignKey("dbo.Book_Authors", "BookId", "dbo.Books");
            DropForeignKey("dbo.Book_Authors", "AuthorId", "dbo.Authors");
            DropIndex("dbo.Book_Series", new[] { "BookId" });
            DropIndex("dbo.Book_Series", new[] { "SeriesId" });
            DropIndex("dbo.Book_Genres", new[] { "BookId" });
            DropIndex("dbo.Book_Genres", new[] { "GenreId" });
            DropIndex("dbo.Book_Authors", new[] { "BookId" });
            DropIndex("dbo.Book_Authors", new[] { "AuthorId" });
            DropTable("dbo.Series");
            DropTable("dbo.Book_Series");
            DropTable("dbo.Genres");
            DropTable("dbo.Book_Genres");
            DropTable("dbo.Books");
            DropTable("dbo.Book_Authors");
            DropTable("dbo.Authors");
        }
    }
}
