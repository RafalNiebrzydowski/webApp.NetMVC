//-----------------------------------------------------------------------
// <copyright file="PointApiController.cs" company="aa">
//     Rafał Niebrzydowski
// </copyright>
//-----------------------------------------------------------------------
namespace Inzynierka.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;
    using Geodeta.Services;

    /// <summary>
    /// Web Point Controller
    /// </summary>
    public class PointApiController : ApiController
    {
        /// <summary>
        /// Obiekt DTO punktu
        /// </summary>
        private PointDTO pointDto = new PointDTO();

        /// <summary>
        /// Obiekt DTO użytkownika
        /// </summary>
        private UserDTO userDto = new UserDTO();

        /*
        // GET api/PointApi
        public IEnumerable<Point> GetPoints()
        {
            this.db.Configuration.ProxyCreationEnabled = false;
            var query = from p in this.db.Point
                        from l in this.db.Line
                        from lof in this.db.ListOfPoints
                        where ((l.AreaId == 2) && (lof.ID_line == l.ID) && (lof.ID_point == p.Id))
                        select p;
            var lista_punktow = query.Distinct();
            return lista_punktow.AsEnumerable();
            /*var point = db.Point.Include(p => p.Note);
             return point.AsEnumerable();
        }
    */

        // GET api/PointApi/5

        /// <summary>
        /// Pobieranie wszystkich punktow obszaru
        /// </summary>
        /// <param name="id">Id obszaru</param>
        /// <param name="email">Email użytkownika</param>
        /// <param name="token">Token użytkownika</param>
        /// <returns>Lista punktów obszaru</returns>
        public IEnumerable<Geodeta.Data.Point> GetPoint(int id, string email, string token)
        {

            if (userDto.TokenIsValid(email, token))
            {
                return this.pointDto.GetPoint(id);
            }
            return null;
        }

        /*
        // PUT api/PointApi/5
        public HttpResponseMessage PutPoint(int id, Point point)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, this.ModelState);
            }

            if (id != point.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            this.db.Entry(point).State = EntityState.Modified;

            try
            {
                this.db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/PointApi
        public HttpResponseMessage PostPoint(Point point)
        {
            if (ModelState.IsValid)
            {
                this.db.Point.Add(point);
                this.db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, point);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = point.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, this.ModelState);
            }
        }

        // DELETE api/PointApi/5
        public HttpResponseMessage DeletePoint(int id)
        {
            Point point = this.db.Point.Find(id);
            if (point == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            this.db.Point.Remove(point);

            try
            {
                this.db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, point);
        }

        protected override void Dispose(bool disposing)
        {
            this.db.Dispose();
            base.Dispose(disposing);
        }
         * */
    }
}