//-----------------------------------------------------------------------
// <copyright file="Point.cs" company="aa">
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
    /// Model DTO Punktu
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Inicjalizuje nową instancję klasy Point
        /// </summary>
        public Point() 
        { 
        }

        /// <summary>
        /// Pobiera lub ustawia id punktu
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Pobiera lub ustawia współrzędną X punktu
        /// </summary>
        public double CoordinateX { get; set; }

        /// <summary>
        /// Pobiera lub ustawia współrzędną Y punktu
        /// </summary>
        public double CoordinateY { get; set; }

        /// <summary>
        /// Pobiera lub ustawia id notatki
        /// </summary>
        public int? NoteId { get; set; }

        /// <summary>
        /// Pobiera lub ustawia relację z tabelą ListOfPoints
        /// </summary>
        public virtual ICollection<ListOfPoints> ListOfPoints { get; set; }

        /// <summary>
        /// Pobiera lub ustawia relację z tabelą Note
        /// </summary>
        public virtual Note Note { get; set; }
    }
}
