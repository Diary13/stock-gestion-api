using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ServerStockGestion.Services
{
    public class ServiceVente
    {


        public void UpdateProduit(Produit produit,int quantite)
        {
            using(var db= new StockGestionContext())
            {
                db.Produits.Attach(produit);
                produit.Quantite -= quantite;
                db.SaveChanges();
                if (produit.Quantite < 50)
                {
                    Mailer.Mail.Send(produit.Libelle);
                }
                Trace.WriteLine("enregistré");
            }
            
        }
    }
}