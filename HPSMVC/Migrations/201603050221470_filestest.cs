namespace HPSMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class filestest : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.File", new[] { "EventID" });
            AlterColumn("dbo.File", "EventID", c => c.Int());
            CreateIndex("dbo.File", "EventID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.File", new[] { "EventID" });
            AlterColumn("dbo.File", "EventID", c => c.Int(nullable: false));
            CreateIndex("dbo.File", "EventID");
        }
    }
}
