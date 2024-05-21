namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTestDataToProductImages_TechnicalSpecifications : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO ProductImages(Path, ProductId_Id) VALUES ('https://res.cloudinary.com/dn84ltxow/image/upload/v1716152267/i1_azjtlg.png', 3)");
            Sql("INSERT INTO ProductImages(Path, ProductId_Id) VALUES ('https://res.cloudinary.com/dn84ltxow/image/upload/v1716152939/Remove-bg.ai_1716152875830_fvxu3b.png', 7)");
            Sql("INSERT INTO ProductImages(Path, ProductId_Id) VALUES ('https://res.cloudinary.com/dn84ltxow/image/upload/v1716152923/Remove-bg.ai_1716152890059_pi9qro.png', 6)");
            Sql("INSERT INTO ProductImages(Path, ProductId_Id) VALUES ('https://res.cloudinary.com/dn84ltxow/image/upload/v1716152928/Remove-bg.ai_1716152865437_z3rebs.png', 4)");
            Sql("INSERT INTO ProductImages(Path, ProductId_Id) VALUES ('https://res.cloudinary.com/dn84ltxow/image/upload/v1716152923/Remove-bg.ai_1716152899477_rfjjh1.png', 5)");
        }
        
        public override void Down()
        {
        }
    }
}
