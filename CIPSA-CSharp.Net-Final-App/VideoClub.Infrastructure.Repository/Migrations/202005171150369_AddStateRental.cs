namespace VideoClub.Infrastructure.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStateRental : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rentals", "State", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rentals", "State");
        }
    }
}
