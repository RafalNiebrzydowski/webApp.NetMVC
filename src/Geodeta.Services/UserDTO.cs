//-----------------------------------------------------------------------
// <copyright file="C_UserDTO.cs" company="aa">
//     Rafał Niebrzydowski
// </copyright>
//-----------------------------------------------------------------------
namespace Geodeta.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Geodeta.Data;
    using System.Security.Cryptography;

    /// <summary>
    /// DTO użytkownika
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// obiekt bazy danych
        /// </summary>
        private GeodetaEntities1 db = new GeodetaEntities1();

        /// <summary>
        /// Pobieranie id użytkownika
        /// </summary>
        /// <param name="email">Adres email użytkownika</param>
        /// <returns>Id użytkownika</returns>
        public int GetId(string email)
        {
            int id = 0;
            var user = this.db.User.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
               id = user.ID;
            }

            return id;
        }

        /// <summary>
        /// Spawdzania czy istnieje już taki użytkownik
        /// </summary>
        /// <param name="email">Adres email użytkownika</param>
        /// <returns>true jeżeli nie ma w bazie takiego adresu email</returns>
        public bool CheckDuplicateLogin(string email)
        {
            var tmp = this.db.User.Where(u => u.Email == email).ToList();
            if (tmp.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Stworzenie nowego użytkownika
        /// </summary>
        /// <param name="user">Użytkownik dodawany do bazy danych</param>
        public void Register(User user)
        {
            char[] chars = new char[62];
            chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetBytes(data);
            data = new byte[20];
            crypto.GetBytes(data);
            StringBuilder result = new StringBuilder(20);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            Geodeta.Data.User c_user = new Geodeta.Data.User { ID = user.ID, Email = user.Email, Password = user.Password, FirstName = user.FirstName, LastName = user.LastName, Token = result.ToString() };
            this.db.User.Add(c_user);
            this.db.SaveChanges();
        }

        /// <summary>
        /// Pobierania użytkownika o danym id
        /// </summary>
        /// <param name="id">Id użytkownika</param>
        /// <returns>Użytkownika o danym id</returns>
        public Geodeta.Data.User Edit(int id = 0)
        {
            Geodeta.Data.User user = this.db.User.Find(id);
            if (user == null)
            {
                return null;
            }

            return user;
        }

        /// <summary>
        /// Edycja użytkownika
        /// </summary>
        /// <param name="user">Użytkownik edytowany</param>
        public void Edit(User user)
        {
            AES aes = new AES();
            Geodeta.Data.User c_user = new Geodeta.Data.User { ID = user.ID, Email = user.Email, Password = aes.Encrypt(user.NewPassword), FirstName = user.FirstName, LastName = user.LastName, Token = user.Token };
            this.db.Entry(c_user).State = EntityState.Modified;
            this.db.SaveChanges();
        }
        /// <summary>
        /// Edycja użytkownika bez hasła
        /// </summary>
        /// <param name="user">Użytkownik edytowany</param>
        public void EditWithoutPassword(User user)
        {
            Geodeta.Data.User c_user = new Geodeta.Data.User { ID = user.ID, Email = user.Email, Password = user.Password, FirstName = user.FirstName, LastName = user.LastName, Token = user.Token };
            this.db.Entry(c_user).State = EntityState.Modified;
            this.db.SaveChanges();
        }
        /// <summary>
        /// Ustawiania nowego hasła
        /// </summary>
        /// <param name="userid">Id użytkownika</param>
        /// <param name="haslo">Hasło</param>
        public void SetNewPassword(int userid,string haslo)
        {
            Geodeta.Data.User c_user = this.Edit(userid);
            c_user.Password = haslo;
            this.db.Entry(c_user).State = EntityState.Modified;
            this.db.SaveChanges();
        }
        /// <summary>
        /// Sprawdzanie czy dane logowania są prawidłowe
        /// </summary>
        /// <param name="email">Adres email podany podczas logowania</param>
        /// <param name="password">Hasło podawane podczas logowania</param>
        /// <returns>true jeżeli dane są prawidłowe</returns>
        public bool IsValid(string email, string password)
        {
            bool is_valid = false;
            
                var user = this.db.User.FirstOrDefault(u => u.Email == email);
                if (user != null)
                {
                    if (user.Password == password)
                    {
                        is_valid = true;
                    }
                }

                return is_valid;
        }
        /// <summary>
        /// Sprawdzenie czy token jest prawidłowy
        /// </summary>
        /// <param name="email">Adres email</param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool TokenIsValid(string email, string token)
        {
            bool is_valid = false;

            var user = this.db.User.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                if (user.Token == token)
                {
                    is_valid = true;
                }
            }

            return is_valid;
        }

        /// <summary>
        /// Pobieranie użytkownika o danym emailu i haśle
        /// </summary>
        /// <param name="email">Email użytkownika</param>
        /// <param name="password">Hasło użytkownika</param>
        /// <returns>Użytkownik o danym emailu i haśle</returns>
        public IEnumerable<Geodeta.Data.User> GetC_User(string email, string password)
        {
            this.db.Configuration.ProxyCreationEnabled = false;
            var query = from p in this.db.User
                        where ((p.Email == email) && (p.Password == password))
                        select p;
            IEnumerable<Geodeta.Data.User> c_user = query;
            if (c_user == null)
            {
                return null;
            }

            return c_user;
        }
    }
}
