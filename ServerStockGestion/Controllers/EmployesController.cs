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

namespace ServerStockGestion.Controllers
{
    public class EmployesController : ApiController
    {
        [Auth]
        private StockGestionContext db = new StockGestionContext();

        // GET: api/Employes
        public IQueryable<Employe> GetEmployes()
        {
            return db.Employes;
        }

        // GET: api/Employes/5
        [Auth]
        [ResponseType(typeof(Employe))]
        public IHttpActionResult GetEmploye(int id)
        {
            Employe employe = db.Employes.Find(id);
            if (employe == null)
            {
                return NotFound();
            }

            return Ok(employe);
        }

        // PUT: api/Employes/5
        [Auth]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmploye(int id, Employe employe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employe.Id)
            {
                return BadRequest();
            }

            db.Entry(employe).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeExists(id))
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

        // POST: api/Employes
        [ResponseType(typeof(Employe)), HttpPost, Route("api/Employes/connect")]
        public IHttpActionResult UserConnect([FromBody]Employe tmpUser)
        {
            Employe user = new Employe();
            try
            {
                user = db.Employes.Single(x => x.Mail == tmpUser.Mail);
            }
            catch (Exception e)
            {
                return null;
            }
            if (user == null || user.MDP != tmpUser.MDP)
            {
                throw new Exception("Erreur de connexion");
            }
            return Ok(user);
        }


        // POST: api/Employes
        [Auth]
        [ResponseType(typeof(Employe))]
        public IHttpActionResult PostEmploye(Employe employe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employes.Add(employe);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employe.Id }, employe);
        }

        // DELETE: api/Employes/5
        [Auth]
        [ResponseType(typeof(Employe))]
        public IHttpActionResult DeleteEmploye(int id)
        {
            Employe employe = db.Employes.Find(id);
            if (employe == null)
            {
                return NotFound();
            }

            db.Employes.Remove(employe);
            db.SaveChanges();

            return Ok(employe);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeExists(int id)
        {
            return db.Employes.Count(e => e.Id == id) > 0;
        }
    }
}