//-----------------------------------------------------------------------
// <copyright file="AreaApi.cs" company="aa">
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
    /// Klasa do przekazania parametrów w aplikacji mobilnej w metodzie post
    /// </summary>
    public class AreaApi
    {
        /// <summary>
        /// Inicjalizuje nową instancję klasy AreaApi
        /// </summary>
        public AreaApi() 
        {
        }

        /// <summary>
        /// Pobiera lub ustawia nazwę obszaru
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Pobiera lub ustawia id użytkownika
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Pobiera lub ustawia współrzędną X
        /// </summary>
        public List<String> PointX { get; set; }

        /// <summary>
        /// Pobiera lub ustawia współrzędną Y
        /// </summary>
        public List<String> PointY { get; set; }

        /// <summary>
        /// Pobiera lub ustawia tytuł notatki
        /// </summary>
        public List<String> Title { get; set; }

        /// <summary>
        /// Pobiera lub ustawia zawartość notatki
        /// </summary>
        public List<String> Content { get; set; }

        /// <summary>
        /// Pobiera lub ustawia tytuł notatki linii
        /// </summary>
        public List<String> TitleLine { get; set; }

        /// <summary>
        /// Pobiera lub ustawia zawartość notatki linii
        /// </summary>
        public List<String> ContentLine { get; set; }
        
        /// <summary>
        /// Czy obszar był edytowany
        /// </summary>
        public bool IsNewVersion { get; set; }

        /// <summary>
        /// Pobiera lub ustawia Token do operacji get i post
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Pobiera lub ustawia email użytkownika
        /// </summary>
        public string Email { get; set; }


        /// <summary>
        /// Pobiera lub ustawia datę modyfikacji
        /// </summary>
        public System.DateTime DateMod { get; set; }
    }
}
