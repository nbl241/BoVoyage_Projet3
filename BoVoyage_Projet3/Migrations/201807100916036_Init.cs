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
                        CreatedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Personnes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Civilite = c.String(),
                        Nom = c.String(),
                        Prenom = c.String(),
                        Adresse = c.String(),
                        Telephone = c.String(),
                        DateNaissance = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
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
                        CreatedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NumeroCarteBancaire = c.String(),
                        PrixTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IdVoyage = c.Int(nullable: false),
                        IdClient = c.Int(nullable: false),
                        IdParticipant = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clients", t => t.IdClient)
                .ForeignKey("dbo.Participants", t => t.IdParticipant)
                .ForeignKey("dbo.Voyages", t => t.IdClient, cascadeDelete: true)
                .Index(t => t.IdClient)
                .Index(t => t.IdParticipant);
            
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
                        CreatedAt = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        DeletedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AgencesVoyage", t => t.IdAgenceVoyage, cascadeDelete: true)
                .ForeignKey("dbo.Destinations", t => t.IdDestination, cascadeDelete: true)
                .Index(t => t.IdAgenceVoyage)
                .Index(t => t.IdDestination);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Personnes", t => t.ID)
                .Index(t => t.ID);
            
            CreateTable(
                "dbo.Participants",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Reduction = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Personnes", t => t.ID)
                .Index(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Participants", "ID", "dbo.Personnes");
            DropForeignKey("dbo.Clients", "ID", "dbo.Personnes");
            DropForeignKey("dbo.Reservations", "IdClient", "dbo.Voyages");
            DropForeignKey("dbo.Voyages", "IdDestination", "dbo.Destinations");
            DropForeignKey("dbo.Voyages", "IdAgenceVoyage", "dbo.AgencesVoyage");
            DropForeignKey("dbo.Reservations", "IdParticipant", "dbo.Participants");
            DropForeignKey("dbo.Reservations", "IdClient", "dbo.Clients");
            DropIndex("dbo.Participants", new[] { "ID" });
            DropIndex("dbo.Clients", new[] { "ID" });
            DropIndex("dbo.Voyages", new[] { "IdDestination" });
            DropIndex("dbo.Voyages", new[] { "IdAgenceVoyage" });
            DropIndex("dbo.Reservations", new[] { "IdParticipant" });
            DropIndex("dbo.Reservations", new[] { "IdClient" });
            DropTable("dbo.Participants");
            DropTable("dbo.Clients");
            DropTable("dbo.Voyages");
            DropTable("dbo.Reservations");
            DropTable("dbo.Destinations");
            DropTable("dbo.Personnes");
            DropTable("dbo.AgencesVoyage");
        }
    }
}
