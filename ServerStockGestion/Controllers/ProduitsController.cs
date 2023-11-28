using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ServerStockGestion;
using ServerStockGestion.Filters;
using System.Diagnostics;
using ServerStockGestion.Services;

namespace ServerStockGestion.Controllers
{
    public class ProduitsController : ApiController
    {
        private StockGestionContext db = new StockGestionContext();

        //[Auth]
        //GET: api/Produits
        //public IQueryable<Produit> GetProduits()
        //{
        //    return db.Produits;
        //}
        [HttpGet, Route("api/Produits")]
        public IHttpActionResult GetAllProduit()
        {
            var response = (from p in db.Produits.Include("Four") where p.Quantite>50 select p).ToArray();
            for(int i = 0; i < response.Length; i++)
            {
                var id = response[i].Four.Id;
                var fourn = db.Fournisseurs.Single(p => p.Id == id);
                response[i].Four = fourn;
            }
            
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        // GET: api/Produits/5
        [ResponseType(typeof(Produit))]
        public IHttpActionResult GetProduit(int id)
        {
            Produit produit = db.Produits.Find(id);
            if (produit == null)
            {
                return NotFound();
            }
           return Ok(produit);
        }

        [HttpGet,Route("api/Produits/ProduitFaible")]
        public IHttpActionResult GetProduitFaible()
        {
            var produit = (from p in db.Produits.Include("Four") where p.Quantite<50 select p).ToArray();

            if (produit == null)
            {
                return NotFound();
            }
            return Ok(produit);
        }

        [HttpGet,Route("api/Produits/name/{libelle}")]
        public IHttpActionResult GetProduitByName([FromUri] string libelle)
        {
            var produit = (from p in db.Produits.Include("Four") where p.Libelle == libelle select p).ToArray();
            if (produit == null)
            {
                return NotFound();
            }
            return Ok(produit);
        }

        // PUT: api/Produits/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduit(int id, Produit produit)
        {
            ServiceProduit s = new ServiceProduit();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != produit.Id)
            {
                return BadRequest();
            }
            

            db.Entry(produit).State = EntityState.Modified;
            
            try
            {

                db.Commandes.Add(new Commande
                {
                    DateCom = DateTime.Now,
                    QuantiteCom = produit.Quantite - s.getResteProduit(id),
                    IdProd = produit.Id
                });
                db.SaveChanges();
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProduitExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Produits
        [ResponseType(typeof(Produit)),HttpPost,Route("api/Produits/new/{nomFour}")]
        public IHttpActionResult PostProduit([FromBody] Produit produit,[FromUri] string nomFour)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var f = db.Fournisseurs.Single(e => e.Nom == nomFour);
            
            int idFour = f.Id;
            produit.IdFour = idFour;
            db.Produits.Add(produit);
            db.SaveChanges();

            db.Commandes.Add(new Commande
            {
                DateCom = DateTime.Now,
                QuantiteCom = produit.Quantite,
                IdProd = produit.Id
            });
            db.SaveChanges();

            //Trace.WriteLine("Ato");

            return Ok(produit);
        }

        // DELETE: api/Produits/5
        [ResponseType(typeof(Produit))]
        public IHttpActionResult DeleteProduit(int id)
        {
            Produit produit = db.Produits.Find(id);
            if (produit == null)
            {
                return NotFound();
            }

            db.Produits.Remove(produit);
            db.SaveChanges();

            return Ok(produit);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProduitExists(int id)
        {
            return db.Produits.Count(e => e.Id == id) > 0;
        }
    }
}