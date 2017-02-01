//-----------------------------------------------------------------------
// <copyright file="UserApiController.cs" company="aa">
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
    using System.Web.Script.Services;
    using Geodeta.Services;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Kontroler użytkownika do aplikacji mobilnej 
    /// </summary>
    public class UserApiController : ApiController
    {
        /// <summary>
        /// Obiekt DTO użytkownika
        /// </summary>
        private UserDTO userDto = new UserDTO();
       /*
        private GeodetaEntities1 db = new GeodetaEntities1();

        // GET api/UserApi
        public IEnumerable<C_User> GetC_User()
        {
            this.db.Configuration.ProxyCreationEnabled = false;
            return this.db.C_User.AsEnumerable();
        }
        */

        // GET api/UserApi/5

        /// <summary>
        /// Pobieranie użytkownika o danym emailu i haśle
        /// </summary>
        /// <param name="email">Email użytkownika</param>
        /// <param name="password">Hasło użytkownika</param>
        /// <returns>Użytkownik o danym emailu i haśle</returns>
        [System.Web.Http.HttpGet]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IEnumerable<Geodeta.Data.User> GetC_User(string email, string password)
        {
            IEnumerable<Geodeta.Data.User> c_user = this.userDto.GetC_User(email, password);
            if (c_user == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return c_user;
        }

        
        /*
        // PUT api/UserApi/5
        public HttpResponseMessage PutC_User(int id, C_User c_user)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, this.ModelState);
            }

            if (id != c_user.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            this.db.Entry(c_user).State = EntityState.Modified;

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

        // POST api/UserApi
        public HttpResponseMessage PostC_User(C_User c_user)
        {
            if (ModelState.IsValid)
            {
                this.db.C_User.Add(c_user);
                this.db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, c_user);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = c_user.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, this.ModelState);
            }
        }

        // DELETE api/UserApi/5
        public HttpResponseMessage DeleteC_User(int id)
        {
            C_User c_user = this.db.C_User.Find(id);
            if (c_user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            this.db.C_User.Remove(c_user);

            try
            {
                this.db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, c_user);
        }

        protected override void Dispose(bool disposing)
        {
            this.db.Dispose();
            base.Dispose(disposing);
        }
         * */
    }
}