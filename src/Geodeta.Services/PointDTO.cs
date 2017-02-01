//-----------------------------------------------------------------------
// <copyright file="PointDTO.cs" company="aa">
//     Rafał Niebrzydowski
// </copyright>
//-----------------------------------------------------------------------
namespace Geodeta.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Geodeta.Data;

    /// <summary>
    /// DTO punktu
    /// </summary>
    public class PointDTO
    {
        /// <summary>
        /// obiekt bazy danych
        /// </summary>
        private GeodetaEntities1 db = new GeodetaEntities1();

        /// <summary>
        /// Pobieranie id ostatnio dodanego punktu
        /// </summary>
        /// <param name="a"></param>
        /// <returns>Id punktu</returns>
        public int GetLastIdPoint(int a)
        {
            var id = this.db.Point.Max(p => p.ID) - a;
            return id;
        }

        /// <summary>
        /// Pobieranie wszystkich punktów
        /// </summary>
        /// <returns>Lista wszystkich punktów</returns>
        public List<Geodeta.Data.Point> GetAllPoints()
        {
            var punkt = this.db.Point.Include(p => p.Note);
            return punkt.ToList();
        }

        /// <summary>
        /// Pobieranie punktu o danym id
        /// </summary>
        /// <param name="id">Id punktu</param>
        /// <returns>Punkt o danym id</returns>
        public Geodeta.Data.Point PointDetails(int id = 0)
        {
            Geodeta.Data.Point punkt = this.db.Point.Find(id);
            if (punkt == null)
            {
                return null;
            }

            return punkt;
        }

        /// <summary>
        /// Dodawanie nowego punktu
        /// </summary>
        /// <param name="x">Współrzędna X punktu</param>
        /// <param name="y">Współrzędna Y punktu</param>
        public void PointCreate(double x, double y)
        {
            this.db.Point.Add(new Geodeta.Data.Point { CoordinateX = x, CoordinateY = y});
            this.db.SaveChanges();
        }

        /// <summary>
        /// Pobieranie punktów obszaru
        /// </summary>
        /// <param name="id">Id obszaru</param>
        /// <returns>Lista punktów obszaru</returns>
        public List<Geodeta.Data.Point> PointsArea(int id)
        {
            var query = from p in this.db.Point
                        from l in this.db.Line
                        from lof in this.db.ListOfPoints
                        where ((l.AreaId == id) && (lof.LineId == l.ID) && (lof.PointId == p.ID))
                        select p;
            var lista_punktow = query.Distinct();

            return lista_punktow.ToList();
        }

        /// <summary>
        /// Pobieranie punktów
        /// </summary>
        /// <param name="id">Id obsaru</param>
        /// <returns>Lista punktów</returns>
        public List<Geodeta.Data.Point> GetPoints(int id)
        {
            List<Geodeta.Data.Point> points = this.PointsArea(id);
            var p = points.Select(x => new Geodeta.Data.Point { ID = x.ID, CoordinateX = x.CoordinateX, CoordinateY = x.CoordinateY, NoteId = x.NoteId });
            return p.ToList();
        }

        /// <summary>
        /// Pobieranie wszystkich punktow obszaru
        /// </summary>
        /// <param name="id">Id obszaru</param>
        /// <returns>Lista punktów obszaru</returns>
        public IEnumerable<Geodeta.Data.Point> GetPoint(int id)
        {
            this.db.Configuration.ProxyCreationEnabled = false;
            var query = from p in this.db.Point
                        from l in this.db.Line
                        from lof in this.db.ListOfPoints
                        where ((l.AreaId == id) && (lof.LineId == l.ID) && (lof.PointId == p.ID))
                        select p;
            var lista_punktow = query.Distinct();
            return lista_punktow.AsEnumerable();
        }
    }
}
