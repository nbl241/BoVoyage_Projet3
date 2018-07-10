namespace BoVoyage_Projet3.Migration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModiffeDossierReservation : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Reservations", newName: "DossiersReservation");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.DossiersReservation", newName: "Reservations");
        }
    }
}
