namespace EntityMigrationsLecture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RentalHistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RentalHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActionTaken = c.Int(nullable: false),
                        StatusDate = c.DateTime(nullable: false),
                        Vehicle_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vehicles", t => t.Vehicle_Id)
                .Index(t => t.Vehicle_Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        Manager_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Manager_Id)
                .Index(t => t.Manager_Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Location_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.Location_Id)
                .Index(t => t.Location_Id);
            
            AddColumn("dbo.Vehicles", "HomeLocation_Id", c => c.Int());
            CreateIndex("dbo.Vehicles", "HomeLocation_Id");
            AddForeignKey("dbo.Vehicles", "HomeLocation_Id", "dbo.Locations", "Id");
            DropColumn("dbo.Vehicles", "Color");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "Color", c => c.String());
            DropForeignKey("dbo.Locations", "Manager_Id", "dbo.Employees");
            DropForeignKey("dbo.Vehicles", "HomeLocation_Id", "dbo.Locations");
            DropForeignKey("dbo.Employees", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.RentalHistories", "Vehicle_Id", "dbo.Vehicles");
            DropIndex("dbo.Employees", new[] { "Location_Id" });
            DropIndex("dbo.Locations", new[] { "Manager_Id" });
            DropIndex("dbo.RentalHistories", new[] { "Vehicle_Id" });
            DropIndex("dbo.Vehicles", new[] { "HomeLocation_Id" });
            DropColumn("dbo.Vehicles", "HomeLocation_Id");
            DropTable("dbo.Employees");
            DropTable("dbo.Locations");
            DropTable("dbo.RentalHistories");
        }
    }
}
