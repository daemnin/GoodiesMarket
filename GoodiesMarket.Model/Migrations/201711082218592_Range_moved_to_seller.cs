namespace GoodiesMarket.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Range_moved_to_seller : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sellers", "Range", c => c.Int());
            DropColumn("dbo.Users", "Range");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Range", c => c.Int(nullable: false));
            DropColumn("dbo.Sellers", "Range");
        }
    }
}
