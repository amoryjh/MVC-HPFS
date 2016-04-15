namespace HPSMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contactx2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Address = c.String(nullable: false, maxLength: 100),
                        City = c.String(nullable: false, maxLength: 50),
                        Province = c.String(nullable: false, maxLength: 25),
                        PostalCode = c.String(nullable: false, maxLength: 7),
                        Telephone = c.String(nullable: false, maxLength: 20),
                        Fax = c.String(maxLength: 20),
                        Hours = c.String(nullable: false, maxLength: 50),
                        Message = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Contact");
        }
    }
}
