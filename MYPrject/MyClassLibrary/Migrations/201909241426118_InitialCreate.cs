namespace MyClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Room_ID = c.Int(nullable: false),
                        User_ID = c.Int(nullable: false),
                        Furn_ID = c.Int(nullable: false),
                        Starting = c.DateTime(nullable: false),
                        Ending = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Furniturs", t => t.Furn_ID, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.Room_ID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_ID, cascadeDelete: true)
                .Index(t => t.Room_ID)
                .Index(t => t.User_ID)
                .Index(t => t.Furn_ID);
            
            CreateTable(
                "dbo.Furniturs",
                c => new
                    {
                        Furn_ID = c.Int(nullable: false, identity: true),
                        Color = c.String(),
                        Name = c.String(),
                        Brand = c.String(),
                    })
                .PrimaryKey(t => t.Furn_ID);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Room_ID = c.Int(nullable: false, identity: true),
                        Furn_ID = c.Int(nullable: false),
                        price = c.Single(nullable: false),
                        County = c.String(maxLength: 250),
                        City = c.String(maxLength: 250),
                        floor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Room_ID)
                .ForeignKey("dbo.Furniturs", t => t.Furn_ID, cascadeDelete: false)
                .Index(t => t.Furn_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        User_ID = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false, maxLength: 250),
                        Lastname = c.String(nullable: false, maxLength: 250),
                        Bithday = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.User_ID);
            
            CreateTable(
                "dbo.Room_Furn",
                c => new
                    {
                        Room_ID = c.Int(nullable: false),
                        Furn_ID = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Room_ID, t.Furn_ID })
                .ForeignKey("dbo.Furniturs", t => t.Furn_ID, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.Room_ID, cascadeDelete: true)
                .Index(t => t.Room_ID)
                .Index(t => t.Furn_ID);
            
            CreateTable(
                "dbo.User_Book",
                c => new
                    {
                        User_ID = c.Int(nullable: false),
                        ID = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_ID, t.ID })
                .ForeignKey("dbo.Bookings", t => t.ID, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.User_ID, cascadeDelete: false)
                .Index(t => t.User_ID)
                .Index(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User_Book", "User_ID", "dbo.Users");
            DropForeignKey("dbo.User_Book", "ID", "dbo.Bookings");
            DropForeignKey("dbo.Room_Furn", "Room_ID", "dbo.Rooms");
            DropForeignKey("dbo.Room_Furn", "Furn_ID", "dbo.Furniturs");
            DropForeignKey("dbo.Bookings", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Bookings", "Room_ID", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "Furn_ID", "dbo.Furniturs");
            DropForeignKey("dbo.Bookings", "Furn_ID", "dbo.Furniturs");
            DropIndex("dbo.User_Book", new[] { "ID" });
            DropIndex("dbo.User_Book", new[] { "User_ID" });
            DropIndex("dbo.Room_Furn", new[] { "Furn_ID" });
            DropIndex("dbo.Room_Furn", new[] { "Room_ID" });
            DropIndex("dbo.Rooms", new[] { "Furn_ID" });
            DropIndex("dbo.Bookings", new[] { "Furn_ID" });
            DropIndex("dbo.Bookings", new[] { "User_ID" });
            DropIndex("dbo.Bookings", new[] { "Room_ID" });
            DropTable("dbo.User_Book");
            DropTable("dbo.Room_Furn");
            DropTable("dbo.Users");
            DropTable("dbo.Rooms");
            DropTable("dbo.Furniturs");
            DropTable("dbo.Bookings");
        }
    }
}
