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
using System.Net.Mail;
using ServerStockGestion.Filters;

namespace ServerStockGestion.Controllers
{
    public class AdministrateursController : ApiController
    {
        [Auth]
        private StockGestionContext db = new StockGestionContext();

        // GET: api/Administrateurs
        public IQueryable<Administrateur> GetAdministrateurs()
        {
            return db.Administrateurs;
        }

        [Auth]
        // GET: api/Administrateurs/5
        [ResponseType(typeof(Administrateur))]
        public IHttpActionResult GetAdministrateur(int id)
        {
            Administrateur administrateur = db.Administrateurs.Find(id);
            if (administrateur == null)
            {
                return NotFound();
            }

            return Ok(administrateur);
        }

        [Auth]
        // PUT: api/Administrateurs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdministrateur(int id, Administrateur administrateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != administrateur.Id)
            {
                return BadRequest();
            }

            db.Entry(administrateur).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministrateurExists(id))
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

        [Auth]
        // POST: api/Administrateurs
        [ResponseType(typeof(Administrateur))]
        public IHttpActionResult PostAdministrateur(Administrateur administrateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Administrateurs.Add(administrateur);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = administrateur.Id }, administrateur);
        }

        // POST: api/Administrateurs
        [ResponseType(typeof(Administrateur)), HttpPost, Route("api/Administrateurs/connect")]
        public IHttpActionResult AdminConnect([FromBody]Administrateur tmpAdmin)
        {
            //var smtpClient = new SmtpClient("Smtp.gmail.com")
            //{
            //    Port = 587,
            //    Credentials = new NetworkCredential("email", "password"),
            //    EnableSsl = true
            //};

            //return smtpClient.Send("email", "recipint", "subject", "body");
            Administrateur admin=new Administrateur();
            try
            {
                 admin= db.Administrateurs.Single(x => x.Mail == tmpAdmin.Mail);
            }
            catch (Exception e)
            {
                return null;
            }
            if (admin == null || admin.MDP != tmpAdmin.MDP)
            {
                throw new Exception("Erreur de connexion");
            }
            return Ok(admin);
        }

        [Auth]
        // DELETE: api/Administrateurs/5
        [ResponseType(typeof(Administrateur))]
        public IHttpActionResult DeleteAdministrateur(int id)
        {
            Administrateur administrateur = db.Administrateurs.Find(id);
            if (administrateur == null)
            {
                return NotFound();
            }

            db.Administrateurs.Remove(administrateur);
            db.SaveChanges();

            return Ok(administrateur);
        }

        [Auth]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Auth]
        private bool AdministrateurExists(int id)
        {
            return db.Administrateurs.Count(e => e.Id == id) > 0;
        }
    }
}