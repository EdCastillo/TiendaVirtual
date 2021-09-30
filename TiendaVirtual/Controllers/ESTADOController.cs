using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using TiendaVirtual.Models;

namespace TiendaVirtual.Controllers
{
    /*
    Puede que la clase WebApiConfig requiera cambios adicionales para agregar una ruta para este controlador. Combine estas instrucciones en el método Register de la clase WebApiConfig según corresponda. Tenga en cuenta que las direcciones URL de OData distinguen mayúsculas de minúsculas.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using TiendaVirtual.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<ESTADO>("ESTADO");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ESTADOController : ODataController
    {
        private TiendaVirtualEntities db = new TiendaVirtualEntities();

        // GET: odata/ESTADO
        [EnableQuery]
        public IQueryable<ESTADO> GetESTADO()
        {
            return db.ESTADO;
        }

        // GET: odata/ESTADO(5)
        [EnableQuery]
        public SingleResult<ESTADO> GetESTADO([FromODataUri] int key)
        {
            return SingleResult.Create(db.ESTADO.Where(eSTADO => eSTADO.ES_ID == key));
            
        }

        // PUT: odata/ESTADO(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<ESTADO> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ESTADO eSTADO = db.ESTADO.Find(key);
            if (eSTADO == null)
            {
                return NotFound();
            }

            patch.Put(eSTADO);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ESTADOExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(eSTADO);
        }

        // POST: odata/ESTADO
        public IHttpActionResult Post(ESTADO eSTADO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ESTADO.Add(eSTADO);
            db.SaveChanges();

            return Created(eSTADO);
        }

        // PATCH: odata/ESTADO(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<ESTADO> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ESTADO eSTADO = db.ESTADO.Find(key);
            if (eSTADO == null)
            {
                return NotFound();
            }

            patch.Patch(eSTADO);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ESTADOExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(eSTADO);
        }

        // DELETE: odata/ESTADO(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            ESTADO eSTADO = db.ESTADO.Find(key);
            if (eSTADO == null)
            {
                return NotFound();
            }

            db.ESTADO.Remove(eSTADO);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ESTADOExists(int key)
        {
            return db.ESTADO.Count(e => e.ES_ID == key) > 0;
        }
    }
}
