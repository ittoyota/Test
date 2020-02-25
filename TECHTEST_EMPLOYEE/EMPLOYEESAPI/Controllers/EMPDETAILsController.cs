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
using EMPLOYEESAPI.Models;

namespace EMPLOYEESAPI.Controllers
{
    public class EMPDETAILsController : ApiController
    {
        private EMPLOYEEDBEntities1 db = new EMPLOYEEDBEntities1();

        // GET: api/EMPDETAILs
        public IQueryable<EMPDETAIL> GetEMPDETAILS()
        {
            return db.EMPDETAILS;
        }

        // GET: api/EMPDETAILs/5
        [ResponseType(typeof(EMPDETAIL))]
        public IHttpActionResult GetEMPDETAIL(int id)
        {
            EMPDETAIL eMPDETAIL = db.EMPDETAILS.Find(id);
            if (eMPDETAIL == null)
            {
                return NotFound();
            }

            return Ok(eMPDETAIL);
        }

        // PUT: api/EMPDETAILs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEMPDETAIL(int id, EMPDETAIL eMPDETAIL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eMPDETAIL.EMPLOYEEID)
            {
                return BadRequest();
            }

            db.Entry(eMPDETAIL).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EMPDETAILExists(id))
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

        // POST: api/EMPDETAILs
        [ResponseType(typeof(EMPDETAIL))]
        public IHttpActionResult PostEMPDETAIL(EMPDETAIL eMPDETAIL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EMPDETAILS.Add(eMPDETAIL);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = eMPDETAIL.EMPLOYEEID }, eMPDETAIL);
        }

        // DELETE: api/EMPDETAILs/5
        [ResponseType(typeof(EMPDETAIL))]
        public IHttpActionResult DeleteEMPDETAIL(int id)
        {
            EMPDETAIL eMPDETAIL = db.EMPDETAILS.Find(id);
            if (eMPDETAIL == null)
            {
                return NotFound();
            }

            db.EMPDETAILS.Remove(eMPDETAIL);
            db.SaveChanges();

            return Ok(eMPDETAIL);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EMPDETAILExists(int id)
        {
            return db.EMPDETAILS.Count(e => e.EMPLOYEEID == id) > 0;
        }
    }
}