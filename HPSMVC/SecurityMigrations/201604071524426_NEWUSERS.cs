namespace HPSMVC.SecurityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NEWUSERS : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FitBitProgress", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "FitBitGoal", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "FitBitGoal");
            DropColumn("dbo.AspNetUsers", "FitBitProgress");
        }
    }
}
