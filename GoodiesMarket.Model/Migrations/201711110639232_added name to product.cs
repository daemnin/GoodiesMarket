namespace GoodiesMarket.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addednametoproduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Products", "Description", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Description", c => c.String());
            DropColumn("dbo.Products", "Name");
        }
    }
}
