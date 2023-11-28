using Newtonsoft.Json.Linq;
using ServerStockGestion.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace ServerStockGestion.Services
{
    public class ServiceProduit
    {
        private StockGestionContext db = new StockGestionContext();
        public int getResteProduit(int id)
        {
            Produit produit = db.Produits.Find(id);
            var response = produit.Quantite;
            //Trace.WriteLine("response=" + response);
            return response;
        }


    }
}