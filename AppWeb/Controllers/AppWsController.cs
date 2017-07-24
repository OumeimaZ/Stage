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
using AppWeb.Models;
using System.Web;

namespace AppWeb.Controllers
{
    public class AppWsController : ApiController
    {
        private AppWebContext db = new AppWebContext();

        // GET: api/AppWs
        public IQueryable<AppW> GetAppWs()
        {
            return db.AppWs;
        }

        // GET: api/AppWs/5
        [ResponseType(typeof(AppW))]
        public IHttpActionResult GetAppW(int id)
        {
            AppW appW = db.AppWs.Find(id);
            if (appW == null)
            {
                return NotFound();
            }

            return Ok(appW);
        }

        // PUT: api/AppWs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAppW(int id, AppW appW)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appW.Id)
            {
                return BadRequest();
            }

            db.Entry(appW).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppWExists(id))
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

        // POST: api/AppWs
        [ResponseType(typeof(AppW))]
        public IHttpActionResult PostAppW(AppW appW)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AppWs.Add(appW);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = appW.Id }, appW);
        }

        // DELETE: api/AppWs/5
        [ResponseType(typeof(AppW))]
        public IHttpActionResult DeleteAppW(int id)
        {
            AppW appW = db.AppWs.Find(id);
            if (appW == null)
            {
                return NotFound();
            }

            db.AppWs.Remove(appW);
            db.SaveChanges();

            return Ok(appW);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppWExists(int id)
        {
            return db.AppWs.Count(e => e.Id == id) > 0;
        }
        public string postFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        var filename = postedFile.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();
                        var filePath = HttpContext.Current.Server.MapPath("~/App_Data/" + filename);

                        postedFile.SaveAs(filePath);
                        return "/ App_Data/ " + filename;
                    }


                }
            }
            catch(Exception exception)
            {
                return exception.Message;
            }
            return "no files";
            
        }
    }
}