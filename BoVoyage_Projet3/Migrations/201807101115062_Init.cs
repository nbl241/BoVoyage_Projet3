namespace BoVoyage_Projet3.Migration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgencesVoyage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Civilite = c.String(),
                        Nom = c.String(),
                        Prenom = c.String(),
                        Adresse = c.String(),
                        Telephone = c.String(),
                        DateNaissance = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Destinations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Continent = c.String(),
                        Pays = c.String(),
                        Region = c.String(),
                        Description = c.String(),
                        CreatedAt = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DossiersReservation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NumeroCarteBancaire = c.String(),
                        PrixTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IdVoyage = c.Int(nullable: false),
                        IdClient = c.Int(nullable: false),
                        IdParticipant = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clients", t => t.IdClient, cascadeDelete: true)
                .ForeignKey("dbo.Participants", t => t.IdParticipant, cascadeDelete: true)
                .ForeignKey("dbo.Voyages", t => t.IdVoyage, cascadeDelete: true)
                .Index(t => t.IdVoyage)
                .Index(t => t.IdClient)
                .Index(t => t.IdParticipant);
            
            CreateTable(
                "dbo.Participants",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Reduction = c.Single(nullable: false),
                        Civilite = c.String(),
                        Nom = c.String(),
                        Prenom = c.String(),
                        Adresse = c.String(),
                        Telephone = c.String(),
                        DateNaissance = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Voyages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateAller = c.DateTime(nullable: false),
                        DateRetour = c.DateTime(nullable: false),
                        PlacesDisponibles = c.Int(nullable: false),
                        TarifToutCompris = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IdAgenceVoyage = c.Int(nullable: false),
                        IdDestination = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AgencesVoyage", t => t.IdAgenceVoyage, cascadeDelete: true)
                .ForeignKey("dbo.Destinations", t => t.IdDestination, cascadeDelete: true)
                .Index(t => t.IdAgenceVoyage)
                .Index(t => t.IdDestination);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DossiersReservation", "IdVoyage", "dbo.Voyages");
            DropForeignKey("dbo.Voyages", "IdDestination", "dbo.Destinations");
            DropForeignKey("dbo.Voyages", "IdAgenceVoyage", "dbo.AgencesVoyage");
            DropForeignKey("dbo.DossiersReservation", "IdParticipant", "dbo.Participants");
            DropForeignKey("dbo.DossiersReservation", "IdClient", "dbo.Clients");
            DropIndex("dbo.Voyages", new[] { "IdDestination" });
            DropIndex("dbo.Voyages", new[] { "IdAgenceVoyage" });
            DropIndex("dbo.DossiersReservation", new[] { "IdParticipant" });
            DropIndex("dbo.DossiersReservation", new[] { "IdClient" });
            DropIndex("dbo.DossiersReservation", new[] { "IdVoyage" });
            DropTable("dbo.Voyages");
            DropTable("dbo.Participants");
            DropTable("dbo.DossiersReservation");
            DropTable("dbo.Destinations");
            DropTable("dbo.Clients");
            DropTable("dbo.AgencesVoyage");
        }
    }
}
