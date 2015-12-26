namespace UserImageUploadAzure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.UserImages", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.UserImages", "UserId");
            RenameColumn(table: "dbo.UserImages", name: "ApplicationUser_Id", newName: "UserId");
            AlterColumn("dbo.UserImages", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserImages", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserImages", new[] { "UserId" });
            AlterColumn("dbo.UserImages", "UserId", c => c.String());
            RenameColumn(table: "dbo.UserImages", name: "UserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.UserImages", "UserId", c => c.String());
            CreateIndex("dbo.UserImages", "ApplicationUser_Id");
        }
    }
}
