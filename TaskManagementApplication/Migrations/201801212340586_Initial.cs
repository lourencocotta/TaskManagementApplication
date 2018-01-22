namespace TaskManagementApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaskName = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        FinishDate = c.DateTime(nullable: false),
                        TaskListId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaskLists", t => t.TaskListId, cascadeDelete: true)
                .Index(t => t.TaskListId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "TaskListId", "dbo.TaskLists");
            DropIndex("dbo.Tasks", new[] { "TaskListId" });
            DropTable("dbo.Tasks");
            DropTable("dbo.TaskLists");
        }
    }
}
