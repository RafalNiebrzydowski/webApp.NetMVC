//-----------------------------------------------------------------------
// <copyright file="AreaApiController.cs" company="aa">
//     Rafał Niebrzydowski
// </copyright>
//-----------------------------------------------------------------------
namespace Inzynierka.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;
    using System.Web.Script.Services;
    using Geodeta.Services;

    /// <summary>
    /// Web Area Controller
    /// </summary>
    public class AreaApiController : ApiController
    {
        /// <summary>
        /// Obiekt DTO obszaru
        /// </summary>
        private AreaDTO areaDto = new AreaDTO();

        /// <summary>
        /// Obiekt DTO notatki
        /// </summary>
        private NoteDTO noteDto = new NoteDTO();

        /// <summary>
        /// Obiekt DTO użytkownika
        /// </summary>
        private UserDTO userDto = new UserDTO();

        // GET api/AreaApi

        /// <summary>
        /// Pobieranie obszarów użytkownika
        /// </summary>
        /// <param name="id">Id użytkownika</param>
        /// <param name="email">Email użytkownika</param>
        /// <param name="token">Token użytkownika</param>
        /// <returns>Lista obszarów użytkownika</returns>
        [ActionName("GetAreasUser")]
        public IEnumerable<Geodeta.Services.Area> GetAreasUser(int id, string email, string token)
        {

            if (userDto.TokenIsValid(email, token))
            {
                return this.areaDto.GetAreasUser(id);
            }
            return null;
        }

        // GET api/AreaApi/5
        /*
        /// <summary>
        /// Pobieranie obszaru o danym id
        /// </summary>
        /// <param name="id">Id obszaru</param>
        /// <param name="email">Email użytkownika</param>
        /// <param name="token">Token użytkownika</param>
        /// <returns>Obszar o danym id</returns>
        public Area GetArea(int id, string email, string token)
        {
            if (userDto.TokenIsValid(email, token))
            {
                return this.areaDto.GetArea(id);
            }
            return null;
        }

        */
        /// <summary>
        /// Pobieranie ostatniego id
        /// </summary>
        /// <param name="email">Email użytkownika</param>
        /// <param name="token">Token użytkownika</param>
        /// <returns>id nowo dodanego obszaru</returns>
        [ActionName("GetLastAddedAreaId")]
        public int GetLastAddedAreaId(string email, string token)
        {
            if (userDto.TokenIsValid(email, token))
            {
                return this.areaDto.GetLastId(email);
            }
            return 0;
        }
        // PUT api/AreaApi/5

        /// <summary>
        /// Edycja obszaru o danym id
        /// </summary>
        /// <param name="id">Id area</param>
        /// <param name="a">Informacje do edycji obszaru z pkt i liniami</param>
        /// <returns>Response</returns>
        [ActionName("PutArea")]
        public HttpResponseMessage PutArea(int id, AreaApi a)
        {
            if (userDto.TokenIsValid(a.Email, a.Token))
            {
                if (ModelState.IsValid)
                {
                    Geodeta.Services.Area area = this.areaDto.PutArea(id, a);
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, area);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = area.ID }));
                    return response;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Edycja obszaru o danym id przy synchronizacji
        /// </summary>
        /// <param name="id">Id area</param>
        /// <param name="email">Email użytkownika</param>
        /// <param name="token">Token użytkownika</param>
        /// <returns>Response</returns>
        [ActionName("PutAreaSync")]
        public HttpResponseMessage PutAreaSync(int id, string email, string token)
        {
            if (userDto.TokenIsValid(email, token))
            {
                if (ModelState.IsValid)
                {
                    Geodeta.Services.Area area = this.areaDto.PutAreaSync(id);
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, area);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = area.ID }));
                    return response;
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        // POST api/AreaApi

        /// <summary>
        /// Dodawanie nowego obszaru
        /// </summary>
        /// <param name="a">Informacje do stworzenia obszaru z pkt i liniami</param>
        /// <returns>response</returns>
        public HttpResponseMessage PostArea(AreaApi a)
        {
            
            if (userDto.TokenIsValid(a.Email, a.Token))
            {
                
                Area area = this.areaDto.PostArea(a);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, area);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = area.ID }));
                return response;
            }

            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, this.ModelState);
            }
        }

        // DELETE api/AreaApi/5

        /// <summary>
        /// Usuwanie obszaru 
        /// </summary>
        /// <param name="id">Id obszaru</param>
        /// <returns>response</returns>
        public HttpResponseMessage DeleteArea(int id)
        {
            if (ModelState.IsValid)
            {
                this.areaDto.DeleteConfirmed(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        /*
        protected override void Dispose(bool disposing)
        {
            this.db.Dispose();
            base.Dispose(disposing);
        }
         * */
    }
}