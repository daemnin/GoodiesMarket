namespace GoodiesMarket.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_timestamps_to_order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "LastModification", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "LastModification");
            DropColumn("dbo.Orders", "CreatedOn");
        }
    }
}
