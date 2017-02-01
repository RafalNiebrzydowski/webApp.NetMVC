//-----------------------------------------------------------------------
// <copyright file="Line.cs" company="aa">
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
    /// Model DTO linii
    /// </summary>
    public class Line
    {
        /// <summary>
        /// Inicjalizuje nową instancję klasy Line
        /// </summary>
        public Line() 
        { 
        }

        /// <summary>
        /// Pobiera lub ustawia id linii
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Pobiera lub ustawia id obszaru
        /// </summary>
        public int AreaId { get; set; }

        /// <summary>
        /// Pobiera lub ustawia id notatki
        /// </summary>
        public int? NoteId { get; set; }


        /// <summary>
        /// Pobiera lub ustawia relację z tabelą Area
        /// </summary>
        public virtual Area Area { get; set; }

        /// <summary>
        /// Pobiera lub ustawia relację z tabelą Note
        /// </summary>
        public virtual Note Note { get; set; }

        /// <summary>
        /// Pobiera lub ustawia relację z tabelą ListOfPoints
        /// </summary>
        public virtual ICollection<ListOfPoints> ListOfPoints { get; set; }
    }
}
