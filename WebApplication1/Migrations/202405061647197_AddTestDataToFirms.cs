namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTestDataToProducts_Firms_TechnicalSpecifications_ProductsImages : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Firms(Name) VALUES ('Samsung')");
            Sql("INSERT INTO Firms(Name) VALUES ('Apple')");
            Sql("INSERT INTO Firms(Name) VALUES ('Xiaomi')");
            Sql("INSERT INTO Firms(Name) VALUES ('Oppo')");
            Sql("INSERT INTO Firms(Name) VALUES ('Realme')");
        }
        
        public override void Down()
        {
        }
    }
}
