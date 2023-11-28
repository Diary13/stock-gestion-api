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

namespace ServerStockGestion.Controllers
{
    public class FacturesController : ApiController
    {
        private StockGestionContext db = new StockGestionContext();

        // GET: api/Factures
        public IQueryable<Facture> GetFactures()
        {
            return db.Factures;
        }

        // GET: api/Factures/5
        [ResponseType(typeof(Facture))]
        public IHttpActionResult GetFacture(int id)
        {
            Facture facture = db.Factures.Find(id);
            if (facture == null)
            {
                return NotFound();
            }

            return Ok(facture);
        }

        // PUT: api/Factures/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFacture(int id, Facture facture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != facture.Id)
            {
                return BadRequest();
            }

            db.Entry(facture).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FactureExists(id))
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

        // POST: api/Factures
        [ResponseType(typeof(Facture))]
        public IHttpActionResult PostFacture(Facture facture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Factures.Add(facture);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = facture.Id }, facture);
        }

        // DELETE: api/Factures/5
        [ResponseType(typeof(Facture))]
        public IHttpActionResult DeleteFacture(int id)
        {
            Facture facture = db.Factures.Find(id);
            if (facture == null)
            {
                return NotFound();
            }

            db.Factures.Remove(facture);
            db.SaveChanges();

            return Ok(facture);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FactureExists(int id)
        {
            return db.Factures.Count(e => e.Id == id) > 0;
        }
    }
}