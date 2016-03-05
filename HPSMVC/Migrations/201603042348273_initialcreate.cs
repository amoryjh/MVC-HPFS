namespace HPSMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 20),
                        Content = c.String(maxLength: 250),
                        Date = c.DateTime(),
                        By = c.String(nullable: false, maxLength: 50),
                        Viewer = c.String(nullable: false),
                        Link = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.File",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Date = c.DateTime(),
                        Category = c.String(maxLength: 50),
                        Type = c.String(nullable: false, maxLength: 50),
                        Data = c.Binary(nullable: false),
                        EventID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Event", t => t.EventID)
                .Index(t => t.EventID);
            
            CreateTable(
                "dbo.Index",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 20),
                        Content = c.String(nullable: false, maxLength: 250),
                        ButtonText = c.String(maxLength: 20),
                        ButtonLink = c.String(maxLength: 100),
                        Image = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Program",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 40),
                        Content = c.String(nullable: false, maxLength: 500),
                        Image = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.File", "EventID", "dbo.Event");
            DropIndex("dbo.File", new[] { "EventID" });
            DropTable("dbo.Program");
            DropTable("dbo.Index");
            DropTable("dbo.File");
            DropTable("dbo.Event");
        }
    }
}
