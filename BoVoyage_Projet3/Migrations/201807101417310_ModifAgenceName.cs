namespace BoVoyage_Projet3.Migration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifAgenceName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AgencesVoyage", "Nom", c => c.String());
            DropColumn("dbo.AgencesVoyage", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AgencesVoyage", "Name", c => c.String());
            DropColumn("dbo.AgencesVoyage", "Nom");
        }
    }
}
