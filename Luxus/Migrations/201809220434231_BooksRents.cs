namespace Luxus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BooksRents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Desc = c.String(),
                        Rating = c.Int(),
                        Status = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                        Image = c.String(),
                        Shared = c.Boolean(),
                        PageNo = c.Int(),
                    })
                .PrimaryKey(t => t.BookID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Rents",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        BookID = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        RecipientName = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserID, t.BookID })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rents", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Books", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Rents", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Books", new[] { "UserID" });
            DropColumn("dbo.AspNetUsers", "FullName");
            DropTable("dbo.Rents");
            DropTable("dbo.Books");
        }
    }
}
