//-----------------------------------------------------------------------
// <copyright file="ListOfPointsApiController.cs" company="aa">
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
    /// Kontroler listy punktów do aplikacji mobilnej 
    /// </summary>
    public class ListOfPointsApiController : ApiController
    {
        /// <summary>
        /// Obiekt DTO listy punktów
        /// </summary>
        private ListOfPointsDTO lofDto = new ListOfPointsDTO();
        /*
        private GeodetaEntities1 db = new GeodetaEntities1();

        // GET api/ListOfPointsApi

        /// <summary>
        /// Pobieranie wszystkich list punktów z bazy danych
        /// </summary>
        /// <returns>lista wszystlich list punktów</returns>
        public IEnumerable<ListOfPoints> GetListOfPoints()
        {
            var listofpoints = this.db.ListOfPoints.Include(l => l.Line).Include(l => l.Point);
            return listofpoints.AsEnumerable();
        }
        */

        // GET api/ListOfPointsApi/5

        /// <summary>
        /// Pobierania listy punktów o danym id
        /// </summary>
        /// <param name="id">id listy punktów</param>
        /// <returns>lista punktów o danym id</returns>
        public IEnumerable<Geodeta.Data.ListOfPoints> GetListOfPoints(int id)
        {
            var listofpoints = this.lofDto.GetListOfPoints(id);
            if (listofpoints == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return listofpoints;
        }

        /*
        // PUT api/ListOfPointsApi/5

        /// <summary>
        /// Edit list of points from android app
        /// </summary>
        /// <param name="id">id line</param>
        /// <param name="listofpoints">model line</param>
        /// <returns>response</returns>
        public HttpResponseMessage PutListOfPoints(int id, ListOfPoints listofpoints)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, this.ModelState);
            }

            if (id != listofpoints.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            this.db.Entry(listofpoints).State = EntityState.Modified;

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

        // POST api/ListOfPointsApi

        /// <summary>
        /// Dodanie listy punktów do bazy danych
        /// </summary>
        /// <param name="listofpoints">Lista punktów, która ma być dodana do bazy</param>
        /// <returns>response</returns>
        public HttpResponseMessage PostListOfPoints(ListOfPoints listofpoints)
        {
            if (ModelState.IsValid)
            {
                this.db.ListOfPoints.Add(listofpoints);
                this.db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, listofpoints);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = listofpoints.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, this.ModelState);
            }
        }

        // DELETE api/ListOfPointsApi/5

        /// <summary>
        /// Usuwanie listy punktów z bazy danych
        /// </summary>
        /// <param name="id">id listy punktów do usunięcia</param>
        /// <returns>response</returns>
        public HttpResponseMessage DeleteListOfPoints(int id)
        {
            ListOfPoints listofpoints = this.db.ListOfPoints.Find(id);
            if (listofpoints == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            this.db.ListOfPoints.Remove(listofpoints);

            try
            {
                this.db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, listofpoints);
        }

        protected override void Dispose(bool disposing)
        {
            this.db.Dispose();
            base.Dispose(disposing);
        }
         * */
    }
}