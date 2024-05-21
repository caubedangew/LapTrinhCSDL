namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTestDataToProduct : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Products(Name, Quantity, Price, TypeOfPhone, FirmId_Id) VALUES ('Iphone 15 Pro', 10, 1500, 5, 4)");
            Sql("INSERT INTO Products(Name, Quantity, Price, TypeOfPhone, FirmId_Id) VALUES ('Samsung Galaxy A54 5G', 10, 250, 2, 3)");
            Sql("INSERT INTO Products(Name, Quantity, Price, TypeOfPhone, FirmId_Id) VALUES ('Xiaomi Redmi Note 13', 10, 215, 4, 5)");
            Sql("INSERT INTO Products(Name, Quantity, Price, TypeOfPhone, FirmId_Id) VALUES ('OPPO Reno11', 10, 360, 4, 6)");
            Sql("INSERT INTO Products(Name, Quantity, Price, TypeOfPhone, FirmId_Id) VALUES ('Realme C65', 10, 190, 4, 7)");


        }
        
        public override void Down()
        {
        }
    }
}
