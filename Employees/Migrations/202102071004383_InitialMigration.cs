namespace Employees.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        bname = c.String(nullable: false),
                        authors_Id = c.Int(),
                        geners_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Geners", t => t.geners_Id)
                .ForeignKey("dbo.Authors", t => t.authors_Id)
                .Index(t => t.authors_Id)
                .Index(t => t.geners_Id);
            
            CreateTable(
                "dbo.Geners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        gname = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "authors_Id", "dbo.Authors");
            DropForeignKey("dbo.Books", "geners_Id", "dbo.Geners");
            DropIndex("dbo.Books", new[] { "geners_Id" });
            DropIndex("dbo.Books", new[] { "authors_Id" });
            DropTable("dbo.Geners");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
