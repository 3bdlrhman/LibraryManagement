namespace LibraryManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookID = c.Int(nullable: false, identity: true),
                        BookName = c.String(nullable: false),
                        AuthorName = c.String(nullable: false),
                        TotalCopiesNumber = c.Int(nullable: false),
                        AvailableCopiesNumber = c.Int(nullable: false),
                        AvailableForBorrowing = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BookID);
            
            CreateTable(
                "dbo.Borrowers",
                c => new
                    {
                        BorrowerID = c.Int(nullable: false, identity: true),
                        BorrowerName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BorrowerID);
            
            CreateTable(
                "dbo.Borrowings",
                c => new
                    {
                        BorrowingID = c.Int(nullable: false, identity: true),
                        DeliverdBack = c.Boolean(nullable: false),
                        book_BookID = c.Int(),
                        borrower_BorrowerID = c.Int(),
                    })
                .PrimaryKey(t => t.BorrowingID)
                .ForeignKey("dbo.Books", t => t.book_BookID)
                .ForeignKey("dbo.Borrowers", t => t.borrower_BorrowerID)
                .Index(t => t.book_BookID)
                .Index(t => t.borrower_BorrowerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Borrowings", "borrower_BorrowerID", "dbo.Borrowers");
            DropForeignKey("dbo.Borrowings", "book_BookID", "dbo.Books");
            DropIndex("dbo.Borrowings", new[] { "borrower_BorrowerID" });
            DropIndex("dbo.Borrowings", new[] { "book_BookID" });
            DropTable("dbo.Borrowings");
            DropTable("dbo.Borrowers");
            DropTable("dbo.Books");
        }
    }
}
