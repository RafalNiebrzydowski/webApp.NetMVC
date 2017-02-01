//-----------------------------------------------------------------------
// <copyright file="Note.cs" company="aa">
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
    /// Model DTO notatki
    /// </summary>
    public class Note
    {
        /// <summary>
        /// Inicjalizuje nową instancję klasy Note
        /// </summary>
        public Note() 
        { 
        }

        /// <summary>
        /// Pobiera lub ustawia id notatki
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Pobiera lub ustawia tytuł notatki
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Pobiera lub ustawia zawartość notatki
        /// </summary>
        public string ContentNote { get; set; }

        /// <summary>
        /// Pobiera lub ustawia relację z tabelą Line
        /// </summary>
        public virtual ICollection<Line> Line { get; set; }

        /// <summary>
        /// Pobiera lub ustawia relację z tabelą Point
        /// </summary>
        public virtual ICollection<Point> Point { get; set; }
    }
}
