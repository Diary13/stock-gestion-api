using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ServerStockGestion
{
    public class StockGestionContext:DbContext
    {
        #region DbSet

        public DbSet<Administrateur> Administrateurs { get; set; }
        public DbSet<Fournisseur> Fournisseurs { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Facture> Factures { get; set; }

        #endregion

        public System.Data.Entity.DbSet<ServerStockGestion.Employe> Employes { get; set; }

        public System.Data.Entity.DbSet<ServerStockGestion.Commande> Commandes { get; set; }

        public System.Data.Entity.DbSet<ServerStockGestion.Vente> Ventes { get; set; }
    }
}