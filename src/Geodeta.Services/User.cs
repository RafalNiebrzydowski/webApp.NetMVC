//-----------------------------------------------------------------------
// <copyright file="C_User.cs" company="aa">
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
    /// Model DTO Użytkownika 
    /// </summary>
    public class User
    {
        /// <summary>
        /// Inicjalizuje nową instancję klasy User
        /// </summary>
        public User() 
        {
        }

        /// <summary>
        /// Pobiera lub ustawia id użytkownika
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Pobiera lub ustawia adres email
        /// </summary>
        [Required(ErrorMessage = "Pole e-mail nie może być puste.")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy adres e-mail.")]
        [StringLength(150)]
        public string Email { get; set; }

        /// <summary>
        /// Pobiera lub ustawia hasło
        /// </summary>
        [Required(ErrorMessage = "Pole hasło nie może być puste.")]
        [DataType(DataType.Password)]
        [StringLength(150, MinimumLength = 6, ErrorMessage = "Hasło musi mieć co najmniej 6 znaków.")]
        public string Password { get; set; }

        /// <summary>
        /// Pobiera lub ustawia hasło
        /// </summary>
        [DataType(DataType.Password)]
        [StringLength(150, MinimumLength = 6)]
        public string NewPassword { get; set; }

        /// <summary>
        /// Pobiera lub ustawia hasło
        /// </summary>
        [DataType(DataType.Password)]
        [StringLength(150, MinimumLength = 6)]
        public string NewConfirmedPassword { get; set; }

        /// <summary>
        /// Pobiera lub ustawia imię użytkownika
        /// </summary>
        [Required(ErrorMessage = "Pole imię nie może być puste.")]
        public string FirstName { get; set; }

        /// <summary>
        /// Pobiera lub ustawia nazwisko użytkownika
        /// </summary>
        [Required(ErrorMessage = "Pole nazwisko nie może być puste.")]
        public string LastName { get; set; }

        /// <summary>
        /// Pobiera lub ustawia wartość wskazującą czy użytkownik ma być zapamiętany przy logowaniu
        /// </summary>
        public bool RememberMe { get; set; }

        /// <summary>
        /// Czy użytkownik chce zmienić hasło
        /// </summary>
        public bool CheckboxChangePassword { get; set; }

        /// <summary>
        /// Token do operacji get i post
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Pobiera lub ustawia relację z tabelą Area
        /// </summary>
        public virtual ICollection<Area> Area { get; set; }
    }
}
