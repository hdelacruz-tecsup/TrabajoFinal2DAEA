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
using Trabajofinal2.Models;

namespace Trabajofinal2.Controllers
{
    public class FacturaController : ApiController
    {
        private TrabajoFinalEntities db = new TrabajoFinalEntities();

        // GET: api/Factura
        public IQueryable<factura> Getfacturas()
        {
            return db.facturas;
        }

        // GET: api/Factura/5
        [ResponseType(typeof(factura))]
        public IHttpActionResult Getfactura(int id)
        {
            factura factura = db.facturas.Find(id);
            if (factura == null)
            {
                return NotFound();
            }

            return Ok(factura);
        }

        // PUT: api/Factura/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putfactura(int id, factura factura)
        {

            if (id != factura.idFactura)
            {
                return BadRequest();
            }

            db.Entry(factura).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!facturaExists(id))
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

        // POST: api/Factura
        [ResponseType(typeof(factura))]
        public IHttpActionResult Postfactura(factura factura)
        {

            db.facturas.Add(factura);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = factura.idFactura }, factura);
        }

        // DELETE: api/Factura/5
        [ResponseType(typeof(factura))]
        public IHttpActionResult Deletefactura(int id)
        {
            factura factura = db.facturas.Find(id);
            if (factura == null)
            {
                return NotFound();
            }

            db.facturas.Remove(factura);
            db.SaveChanges();

            return Ok(factura);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool facturaExists(int id)
        {
            return db.facturas.Count(e => e.idFactura == id) > 0;
        }
    }
}