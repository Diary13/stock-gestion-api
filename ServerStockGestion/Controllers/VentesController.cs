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
using ServerStockGestion.Services;

namespace ServerStockGestion.Controllers
{
    public class VentesController : ApiController
    {
        private StockGestionContext db = new StockGestionContext();

        // GET: api/Ventes
        public IQueryable<Vente> GetVentes()
        {
            return db.Ventes;
        }

        // GET: api/Ventes/5
        [ResponseType(typeof(Vente))]
        public IHttpActionResult GetVente(int id)
        {
            Vente vente = db.Ventes.Find(id);
            if (vente == null)
            {
                return NotFound();
            }

            return Ok(vente);
        }

        // PUT: api/Ventes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVente(int id, Vente vente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vente.Id)
            {
                return BadRequest();
            }

            db.Entry(vente).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VenteExists(id))
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

        // POST: api/Ventes
        [ResponseType(typeof(Vente[]))]
        public IHttpActionResult PostVente([FromBody] Vente[] ventes)
        {
            ServiceVente s = new ServiceVente();
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            for (int i = 0; i < ventes.Length; i++)
            {
                var id = ventes[i].IdProd;
                var produit = db.Produits.Single(p => p.Id == id);
                s.UpdateProduit(produit,ventes[i].Quantite);

            }
            var facture = new Facture();
            facture.DateFact = DateTime.Now;
            db.Factures.Add(facture);
            db.SaveChanges();

            var f=db.Factures.Attach(facture);
            for (int i= 0;i< ventes.Length;i++)
            {
                ventes[i].IdFact = f.Id;
            }
            db.Ventes.AddRange(ventes);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", null,ventes);
        }

        // DELETE: api/Ventes/5
        [ResponseType(typeof(Vente))]
        public IHttpActionResult DeleteVente(int id)
        {
            Vente vente = db.Ventes.Find(id);
            if (vente == null)
            {
                return NotFound();
            }

            db.Ventes.Remove(vente);
            db.SaveChanges();

            return Ok(vente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VenteExists(int id)
        {
            return db.Ventes.Count(e => e.Id == id) > 0;
        }
    }
}