namespace FilmViewer.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SliderTypeChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MainMovies", "SliderType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MainMovies", "SliderType", c => c.String(nullable: false));
        }
    }
}
