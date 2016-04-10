namespace HPSMVC.SecurityMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fitbitdates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "dateStartFitBit", c => c.String());
            AddColumn("dbo.AspNetUsers", "dateEndFitBit", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "dateEndFitBit");
            DropColumn("dbo.AspNetUsers", "dateStartFitBit");
        }
    }
}
