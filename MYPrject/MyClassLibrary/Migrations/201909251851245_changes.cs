namespace MyClassLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Furniturs", "HourlyCost", c => c.String());
            AddColumn("dbo.Users", "Balance", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Balance");
            DropColumn("dbo.Furniturs", "HourlyCost");
        }
    }
}
