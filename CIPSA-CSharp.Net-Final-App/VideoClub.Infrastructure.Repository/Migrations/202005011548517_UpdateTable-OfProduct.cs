namespace VideoClub.Infrastructure.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableOfProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "QuantityDisc", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "NumberDisc");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "NumberDisc", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "QuantityDisc");
        }
    }
}
