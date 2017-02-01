//-----------------------------------------------------------------------
// <copyright file="NoteApiController.cs" company="aa">
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
    /// Kontroler notatki do aplikacji mobilnej 
    /// </summary>
    public class NoteApiController : ApiController
    {
        /// <summary>
        /// Obiekt DTO notatki
        /// </summary>
        private NoteDTO noteDto = new NoteDTO();

        /// <summary>
        /// Obiekt DTO użytkownika
        /// </summary>
        private UserDTO userDto = new UserDTO();

        // GET api/Note

       /* /// <summary>
        /// Pobieranie wszystkich notatek z bazy danych
        /// </summary>
        /// <returns>Lista notatek</returns>
        public IEnumerable<Geodeta.Data.Note> GetNotes()
        {
            this.db.Configuration.ProxyCreationEnabled = false;
            return this.db.Note.AsEnumerable();
        }
        */

        // GET api/Note/5

        /// <summary>
        /// Pobieranie wszyskich notatek obszaru o danym id
        /// </summary>
        /// <param name="id">Id obszaru</param>
        /// <param name="email">Email użytkownika</param>
        /// <param name="token">Token użytkownika</param>
        /// <returns>Lista notatek</returns>
        public IEnumerable<Geodeta.Data.Note> GetNote(int id, string email, string token)
        {
            if (userDto.TokenIsValid(email, token))
            {
                return this.noteDto.GetNote(id);
            }
            return null;
        }

/*
        // PUT api/Note/5

        /// <summary>
        /// Edycja notatki
        /// </summary>
        /// <param name="id">Id notatki</param>
        /// <param name="note">Notatka zastępująca poprzędnią notatkę</param>
        /// <returns>response</returns>
        public HttpResponseMessage PutNote(int id, Geodeta.Data.Note note)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, this.ModelState);
            }

            if (id != note.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            this.db.Entry(note).State = EntityState.Modified;

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

        // POST api/Note

        /// <summary>
        /// Dodawanie notatki do bazy danych
        /// </summary>
        /// <param name="note">Notatka dodawana</param>
        /// <returns>response</returns>
        public HttpResponseMessage PostNote(Geodeta.Data.Note note)
        {
            if (ModelState.IsValid)
            {
                this.db.Note.Add(note);
                this.db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, note);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = note.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, this.ModelState);
            }
        }

        // DELETE api/Note/5

        /// <summary>
        /// Usuwanie notatki
        /// </summary>
        /// <param name="id">Id notatki</param>
        /// <returns>response</returns>
        public HttpResponseMessage DeleteNote(int id)
        {
            Geodeta.Data.Note note = this.db.Note.Find(id);
            if (note == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            this.db.Note.Remove(note);

            try
            {
                this.db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, note);
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