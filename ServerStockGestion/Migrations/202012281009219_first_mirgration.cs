namespace ServerStockGestion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first_mirgration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrateurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 50),
                        Prenom = c.String(nullable: false, maxLength: 40),
                        Adresse = c.String(nullable: false, maxLength: 60),
                        Mail = c.String(nullable: false),
                        MDP = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Commandes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuantiteCom = c.Int(nullable: false),
                        DateCom = c.DateTime(nullable: false),
                        IdProd = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produits", t => t.IdProd, cascadeDelete: true)
                .Index(t => t.IdProd);
            
            CreateTable(
                "dbo.Produits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(nullable: false),
                        PrixUnitaire = c.Int(nullable: false),
                        Quantite = c.Int(nullable: false),
                        Image = c.String(),
                        IdFour = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fournisseurs", t => t.IdFour, cascadeDelete: true)
                .Index(t => t.IdFour);
            
            CreateTable(
                "dbo.Fournisseurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 50),
                        Localite = c.String(nullable: false, maxLength: 80),
                        Mail = c.String(nullable: false),
                        Tel = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false, maxLength: 50),
                        Prenom = c.String(nullable: false, maxLength: 40),
                        Adresse = c.String(nullable: false, maxLength: 60),
                        Tel = c.String(nullable: false, maxLength: 10),
                        Mail = c.String(nullable: false),
                        MDP = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Factures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateFact = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ventes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantite = c.Int(nullable: false),
                        IdFact = c.Int(nullable: false),
                        IdProd = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Factures", t => t.IdFact, cascadeDelete: true)
                .ForeignKey("dbo.Produits", t => t.IdProd, cascadeDelete: true)
                .Index(t => t.IdFact)
                .Index(t => t.IdProd);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ventes", "IdProd", "dbo.Produits");
            DropForeignKey("dbo.Ventes", "IdFact", "dbo.Factures");
            DropForeignKey("dbo.Commandes", "IdProd", "dbo.Produits");
            DropForeignKey("dbo.Produits", "IdFour", "dbo.Fournisseurs");
            DropIndex("dbo.Ventes", new[] { "IdProd" });
            DropIndex("dbo.Ventes", new[] { "IdFact" });
            DropIndex("dbo.Produits", new[] { "IdFour" });
            DropIndex("dbo.Commandes", new[] { "IdProd" });
            DropTable("dbo.Ventes");
            DropTable("dbo.Factures");
            DropTable("dbo.Employes");
            DropTable("dbo.Fournisseurs");
            DropTable("dbo.Produits");
            DropTable("dbo.Commandes");
            DropTable("dbo.Administrateurs");
        }
    }
}
