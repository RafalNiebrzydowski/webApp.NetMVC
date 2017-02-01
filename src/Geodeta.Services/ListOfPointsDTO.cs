//-----------------------------------------------------------------------
// <copyright file="ListOfPointsDTO.cs" company="aa">
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
    /// DTO listy punktów
    /// </summary>
    public class ListOfPointsDTO
    {
        /// <summary>
        /// Obiekt bazy danych
        /// </summary>
        private GeodetaEntities1 db = new GeodetaEntities1();

        /// <summary>
        /// Pobieranie wszystkich list punktów
        /// </summary>
        /// <returns>Lista list punktów</returns>
        public List<Geodeta.Data.ListOfPoints> GetAllListOfPoints()
        {
            var lista_punktów = this.db.ListOfPoints.Include(l => l.Line).Include(l => l.Point);
            return lista_punktów.ToList();
        }

        /// <summary>
        /// Pobieranie listy punktów o danym id
        /// </summary>
        /// <param name="id">Id listy punktów</param>
        /// <returns>Lista punktów o danym id</returns>
        public Geodeta.Data.ListOfPoints ListOfPointsDetails(int id = 0)
        {
            Geodeta.Data.ListOfPoints lista_punktów = this.db.ListOfPoints.Find(id);
            if (lista_punktów == null)
            {
                return null;
            }

            return lista_punktów;
        }

        /// <summary>
        /// Dodawanie nowej listy punktów
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public void ListOfPointCreate(int a, int b)
        {
            PointDTO p_dto = new PointDTO();
            LineDTO l_dto = new LineDTO();
            var id = l_dto.GetLastIdLine(a);
            var point1_id = p_dto.GetLastIdPoint(a);
            var point2_id = p_dto.GetLastIdPoint(b);
            this.db.ListOfPoints.Add(new Geodeta.Data.ListOfPoints { LineId = id, PointId = point1_id });
            this.db.ListOfPoints.Add(new Geodeta.Data.ListOfPoints { LineId = id, PointId = point2_id });
            this.db.SaveChanges();
        }

        /// <summary>
        /// Pobranie listy punktow obszaru
        /// </summary>
        /// <param name="id">Id linii</param>
        /// <returns>Lista punktów obszaru</returns>
        public IEnumerable<Geodeta.Data.ListOfPoints> GetListOfPoints(int id)
        {
            this.db.Configuration.ProxyCreationEnabled = false;
            var lof = this.db.ListOfPoints.Where(a => a.LineId == id).ToList();
            if (lof == null)
            {
                return null;
            }
            
            return lof;
        }
    }
}
