//-----------------------------------------------------------------------
// <copyright file="NoteDTO.cs" company="aa">
//     Rafał Niebrzydowski
// </copyright>
//-----------------------------------------------------------------------
namespace Geodeta.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Geodeta.Data;

    /// <summary>
    /// DTO notatki
    /// </summary>
    public class NoteDTO
    {
        /// <summary>
        /// Obiekt bazy danych
        /// </summary>
        private GeodetaEntities1 db = new GeodetaEntities1();

        /// <summary>
        /// Pobieranie wszystkich notatek
        /// </summary>
        /// <returns>Lista notatek</returns>
        public List<Geodeta.Data.Note> GetAllNotes()
        {
            return this.db.Note.ToList();
        }

        /// <summary>
        /// Pobieranie notatki o danym id
        /// </summary>
        /// <param name="id">Id notatki</param>
        /// <returns>Notatka o danym id</returns>
        public Geodeta.Data.Note NoteDetails(int id = 0)
        {
            var notatka = this.db.Note.Find(id);
            var tmp = new Geodeta.Data.Note { ID = notatka.ID, ContentNote = notatka.ContentNote, Title = notatka.Title};
            if (notatka == null)
            {
                return null;
            }

            return tmp;
        }

        /// <summary>
        /// Edycja notatki
        /// </summary>
        /// <param name="id">Id notatki</param>
        /// <param name="title">Tytuł notatki</param>
        /// <param name="content">Zawartość notatki</param>
        public void EditNote(int id, string title, string content)
        {
            Geodeta.Data.Note notatka = this.db.Note.Find(id);
            notatka.Title = title;
            notatka.ContentNote = content;
            this.db.Entry(notatka).State = EntityState.Modified;

            this.db.SaveChanges();
        }

        /// <summary>
        /// Dodawanie nowej notatki
        /// </summary>
        /// <param name="pointId">Id punktu</param>
        /// <param name="title">Tytuł notatki</param>
        /// <param name="content">Zawartość notatki</param>
        /// <returns>Id notatki</returns>
        public int CreateNote(int pointId, string title, string content)
        {
            Geodeta.Data.Note note = this.db.Note.Add(new Geodeta.Data.Note { Title = title, ContentNote = content});
            this.db.SaveChanges();
            if (pointId != 0)
            {
                Geodeta.Data.Point point = this.db.Point.Find(pointId);
                point.NoteId = note.ID;
                this.db.Entry(point).State = EntityState.Modified;

                this.db.SaveChanges();
            }

            return note.ID;
        }

        /// <summary>
        /// Usuwanie notatki
        /// </summary>
        /// <param name="id">Id notatki</param>
        public void DeleteNote(int id)
        {
            IQueryable<Geodeta.Data.Point> point = this.db.Point.Where(x => x.NoteId == id);
            if (point.Count() > 0)
            {
                foreach (var item in point)
                {
                    if (item != null)
                    {
                        item.NoteId = null;
                        this.db.Entry(item).State = EntityState.Modified;
                    }
                }
            }
            IQueryable<Geodeta.Data.Line> line = this.db.Line.Where(x => x.NoteId == id);
            if (line.Count() > 0)
            {
                foreach (var item in line)
                {
                    if (item != null)
                    {
                        item.NoteId = null;
                        this.db.Entry(item).State = EntityState.Modified;
                    }
                }
            }
            this.db.SaveChanges();
            Geodeta.Data.Note notatka = this.db.Note.Find(id);
            this.db.Note.Remove(notatka);
            this.db.SaveChanges();
        }

        /// <summary>
        /// Pobieranie wszyskich notatek obszaru o danym id
        /// </summary>
        /// <param name="id">Id obszaru</param>
        /// <returns>Lista notatek</returns>
        public IEnumerable<Geodeta.Data.Note> GetNote(int id)
        {
            AreaDTO areaDto = new AreaDTO();
            this.db.Configuration.ProxyCreationEnabled = false;
            List<Geodeta.Data.Note> notes = new List<Geodeta.Data.Note>();
            List<int?> noteid = new List<int?>();
            noteid = areaDto.GetAreaNoteId(id);
            for (int i = 0; i < noteid.Count(); i++)
            {
                if (noteid[i] != null)
                {
                    notes.Add(this.db.Note.Find(noteid[i]));
                }
                else
                {
                    notes.Add(new Geodeta.Data.Note { Title = string.Empty, ContentNote = "Brak notatki" });
                }
            }

            return notes;
        }
    }
}
