//-----------------------------------------------------------------------
// <copyright file="LineDTO.cs" company="aa">
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
    /// DTO linii
    /// </summary>
    public class LineDTO
    {
        /// <summary>
        /// obiekt bazy danych
        /// </summary>
        private GeodetaEntities1 db = new GeodetaEntities1();

        /// <summary>
        /// Pobieranie id ostatnio dodanej linii
        /// </summary>
        /// <param name="a"></param>
        /// <returns>Id linii</returns>
        public int GetLastIdLine(int a)
        {
            var id = this.db.Line.Max(p => p.ID) - a;
            return id;
        }

        /// <summary>
        /// Pobieranie wszystkich linii 
        /// </summary>
        /// <returns>Lista wszystkich linii</returns>
        public List<Geodeta.Data.Line> GetAllLines()
        {
            var linia = this.db.Line.Include(l => l.Note).Include(l => l.Area);
            return linia.ToList();
        }

        /// <summary>
        /// Pobieranie linii o danym id
        /// </summary>
        /// <param name="id">Id linii</param>
        /// <returns>Linia o danym id</returns>
        public Geodeta.Data.Line LineDetails(int id = 0)
        {
            Geodeta.Data.Line linia = this.db.Line.Find(id);
            if (linia == null)
            {
                return null;
            }

            return linia;
        }

        /// <summary>
        /// Dodawanie nowej linii
        /// </summary>
        /// <param name="id">Id obszaru</param>
        public void LineCreate(int id)
        {
            Geodeta.Data.Line line = new Geodeta.Data.Line { AreaId = id };
            this.db.Line.Add(line);
            this.db.SaveChanges();
        }

        /// <summary>
        /// Pobieranie punktów obszaru
        /// </summary>
        /// <param name="id">Id obszaru</param>
        /// <returns>Lista punktów obszaru</returns>
        public List<Geodeta.Data.Line> LinesArea(int id)
        {
            var query = from l in this.db.Line
                        where (l.AreaId == id)
                        select l;
            var lista_linii = query.Distinct();

            return lista_linii.ToList();
        }

        /// <summary>
        /// Pobieranie punktów
        /// </summary>
        /// <param name="id">Id obsaru</param>
        /// <returns>Lista punktów</returns>
        public List<Geodeta.Data.Line> GetLines(int id)
        {
            List<Geodeta.Data.Line> lines = this.LinesArea(id);
            var p = lines.Select(x => new Geodeta.Data.Line { ID = x.ID, AreaId=x.AreaId, NoteId = x.NoteId });
            return p.ToList();
        }

        /// <summary>
        /// Pobranie listy linii obszaru
        /// </summary>
        /// <param name="id">Id obszaru</param>
        /// <returns>Lista linii obszaru</returns>
        public IEnumerable<Geodeta.Data.Line> GetLine(int id)
        {
            this.db.Configuration.ProxyCreationEnabled = false;
            var line = this.db.Line.Where(a => a.AreaId == id);
            if (line == null)
            {
                return null;
            }
           
                return line;
        }
    }
}
