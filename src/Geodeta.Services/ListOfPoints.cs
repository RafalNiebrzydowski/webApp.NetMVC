//-----------------------------------------------------------------------
// <copyright file="ListOfPoints.cs" company="aa">
//     Rafał Niebrzydowski
// </copyright>
//-----------------------------------------------------------------------
namespace Geodeta.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Model DTO listy punktów
    /// </summary>
    public class ListOfPoints
    {
        /// <summary>
        /// Inicjalizuje nową instancję klasy ListOfPoints
        /// </summary>
        public ListOfPoints() 
        { 
        }

        /// <summary>
        /// Pobiera lub ustawia id listy punktów
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Pobiera lub ustawia id punktu
        /// </summary>
        public int PointId { get; set; }

        /// <summary>
        /// Pobiera lub ustawia id linii
        /// </summary>
        public int LineId { get; set; }

        /// <summary>
        /// Pobiera lub ustawia relację z tabelą Line
        /// </summary>
        public virtual Line Line { get; set; }

        /// <summary>
        /// Pobiera lub ustawia relajcję z tabelą Point
        /// </summary>
        public virtual Point Point { get; set; }
    }
}
