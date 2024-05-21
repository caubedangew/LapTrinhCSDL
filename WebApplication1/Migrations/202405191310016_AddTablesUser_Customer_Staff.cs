namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTablesUser_Customer_Staff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 15),
                        Password = c.String(nullable: false, maxLength: 50),
                        Avatar = c.String(nullable: false),
                        Created_date = c.DateTime(nullable: false),
                        Updated_date = c.DateTime(nullable: false),
                        Is_active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Salary = c.Single(nullable: false),
                        Start_working_date = c.DateTime(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Staffs", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Customers", "User_Id", "dbo.Users");
            DropIndex("dbo.Staffs", new[] { "User_Id" });
            DropIndex("dbo.Customers", new[] { "User_Id" });
            DropTable("dbo.Staffs");
            DropTable("dbo.Users");
            DropTable("dbo.Customers");
        }
    }
}
