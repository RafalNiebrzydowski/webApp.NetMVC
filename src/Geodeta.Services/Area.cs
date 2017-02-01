//-----------------------------------------------------------------------
// <copyright file="Area.cs" company="aa">
//     Rafał Niebrzydowski
// </copyright>
//-----------------------------------------------------------------------
namespace Geodeta.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Model DTO obszaru
    /// </summary>
    public class Area
    {
        /// <summary>
        /// Inicjalizuje nową instancję klasy Area
        /// </summary>
        public Area() 
        {
        }

        /// <summary>
        /// Pobiera lub ustawia id obszaru
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Pobiera lub ustawia nazwę obszaru
        /// </summary>
        [Required(ErrorMessage = "Pole nazwa nie może być puste.")]
        public string Name { get; set; }

        /// <summary>
        /// Pobiera lub ustawia id użytkownika
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Pobiera lub ustawia datę modyfikacji
        /// </summary>
        public System.DateTime DateMod { get; set; }

        /// <summary>
        /// Czy obszar został edytowany
        /// </summary>
        public bool IsNewVersion { get; set; }
        /// <summary>
        /// Pobiera lub ustawia relację z tabelą C_User
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Pobiera lub ustawia relację z tabelą Line
        /// </summary>
        public virtual ICollection<Line> Line { get; set; }
    }
}
