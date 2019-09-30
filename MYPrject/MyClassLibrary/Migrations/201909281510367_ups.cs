namespace MyClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ups : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Furniturs", "HourlyCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Rooms", "price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Users", "Balance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Balance", c => c.Double(nullable: false));
            AlterColumn("dbo.Rooms", "price", c => c.Single(nullable: false));
            AlterColumn("dbo.Furniturs", "HourlyCost", c => c.String());
        }
    }
}
