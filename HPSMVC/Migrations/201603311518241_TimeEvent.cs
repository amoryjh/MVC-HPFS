namespace HPSMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Event", "Time", c => c.String(maxLength: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Event", "Time");
        }
    }
}
