//-----------------------------------------------------------------------
// <copyright file="LineApiController.cs" company="aa">
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
    /// Kontroler linii do aplikacji mobilnej 
    /// </summary>
    public class LineApiController : ApiController
    {
        /// <summary>
        /// Obiekt DTO linii
        /// </summary>
        private LineDTO lineDto = new LineDTO();

        /// <summary>
        /// Obiekt DTO użytkownika
        /// </summary>
        private UserDTO userDto = new UserDTO();

        // GET api/LineApi

       /* /// <summary>
        /// Get list all lines in database
        /// </summary>
        /// <returns>List all lines in database</returns>
        public IEnumerable<Line> GetLines()
        {
            var line = this.db.Line.Include(l => l.Area).Include(l => l.Note);
            return line.AsEnumerable();
        }
        */

        // GET api/LineApi/5

        /// <summary>
        /// Get list lines area
        /// </summary>
        /// <param name="id">id area</param>
        /// <param name="email">Email użytkownika</param>
        /// <param name="token">Token użytkownika</param>
        /// <returns>list lines area</returns>
        public IEnumerable<Geodeta.Data.Line> GetLine(int id, string email, string token)
        {
            if (userDto.TokenIsValid(email, token))
            {
                var line = this.lineDto.GetLine(id);
                if (line == null)
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }

                return line;
            }
            return null;
        }

        /*
        // PUT api/LineApi/5

        /// <summary>
        /// Edit line from android app
        /// </summary>
        /// <param name="id">id line</param>
        /// <param name="line">model line</param>
        /// <returns>response</returns>
        public HttpResponseMessage PutLine(int id, Line line)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, this.ModelState);
            }

            if (id != line.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            this.db.Entry(line).State = EntityState.Modified;

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

        // POST api/LineApi

        /// <summary>
        /// create line from android app
        /// </summary>
        /// <param name="line">model line</param>
        /// <returns>response</returns>
        public HttpResponseMessage PostLine(Line line)
        {
            if (ModelState.IsValid)
            {
                this.db.Line.Add(line);
                this.db.SaveChanges();
                this
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, line);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = line.ID }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, this.ModelState);
            }
        }

        // DELETE api/LineApi/5

        /// <summary>
        /// Delete line from android app
        /// </summary>
        /// <param name="id">id line</param>
        /// <returns>response</returns>
        public HttpResponseMessage DeleteLine(int id)
        {
            Line line = this.db.Line.Find(id);
            if (line == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            this.db.Line.Remove(line);

            try
            {
                this.db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, line);
        }

        /*
        protected override void Dispose(bool disposing)
        {
            this.db.Dispose();
            base.Dispose(disposing);
        }
         */
    }
}