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
    public class FournisseursController : ApiController
    {
        private StockGestionContext db = new StockGestionContext();

        // GET: api/Fournisseurs
        public IQueryable<Fournisseur> GetFournisseurs()
        {
            return db.Fournisseurs;
        }

        // GET: api/Fournisseurs/5
        [ResponseType(typeof(Fournisseur))]
        public IHttpActionResult GetFournisseur(int id)
        {
            Fournisseur fournisseur = db.Fournisseurs.Find(id);
            if (fournisseur == null)
            {
                return NotFound();
            }

            return Ok(fournisseur);
        }

        // PUT: api/Fournisseurs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFournisseur(int id, Fournisseur fournisseur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fournisseur.Id)
            {
                return BadRequest();
            }

            db.Entry(fournisseur).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FournisseurExists(id))
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

        // POST: api/Fournisseurs
        [ResponseType(typeof(Fournisseur))]
        public IHttpActionResult PostFournisseur(Fournisseur fournisseur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Fournisseurs.Add(fournisseur);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = fournisseur.Id }, fournisseur);
        }

        // DELETE: api/Fournisseurs/5
        [ResponseType(typeof(Fournisseur))]
        public IHttpActionResult DeleteFournisseur(int id)
        {
            Fournisseur fournisseur = db.Fournisseurs.Find(id);
            if (fournisseur == null)
            {
                return NotFound();
            }

            db.Fournisseurs.Remove(fournisseur);
            db.SaveChanges();

            return Ok(fournisseur);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FournisseurExists(int id)
        {
            return db.Fournisseurs.Count(e => e.Id == id) > 0;
        }
    }
}