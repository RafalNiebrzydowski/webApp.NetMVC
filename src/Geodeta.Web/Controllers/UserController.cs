//-----------------------------------------------------------------------
// <copyright file="UserController.cs" company="aa">
//     Rafał Niebrzydowski
// </copyright>
//-----------------------------------------------------------------------
namespace Inzynierka.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using Mvc.Mailer;
    using Geodeta.Services;
    using System.Web.Mail;
    using System.Net.Mail;
    using System.Net;
    using System.Net.Mime;

    /// <summary>
    /// Kontroler widoku User
    /// </summary>
    public class UserController : Controller
    {
        /// <summary>
        /// obiekt UserDTO
        /// </summary>
        private UserDTO userDto = new UserDTO();



        /// <summary>
        /// Logowanie użytkownika
        /// </summary>
        /// <param name="user">Użytkowanik logujący się</param>
        /// <returns>Widok strony głównej</returns>
        [HttpPost]
        public ActionResult Login(User user)
        {
            AES aes = new AES();
                if (this.IsValid(user.Email, aes.Encrypt(user.Password)))
                {
                    if (user.RememberMe == true)
                    {
                        var authTicket = new FormsAuthenticationTicket(1, user.Email, DateTime.Now, DateTime.Now.AddMinutes(20), user.RememberMe, string.Empty, "/");
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                        Response.Cookies.Add(cookie);
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(user.Email, false);
                    }

                    return this.RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, string.Empty);
                }
            
            return this.View();
        }

        // GET: /User/Create

        /// <summary>
        /// Akcja odpowiedzialna za widok rejestracji
        /// </summary>
        /// <returns>Widok rejestracji</returns>
        public ActionResult Register()
        {
            return this.View();
        }

        // POST: /User/Create

        /// <summary>
        /// Rejestracja użytkowanika
        /// </summary>
        /// <param name="c_user">Nowy użytkownik </param>
        /// <returns>Widok strony głownej lub rejestracji</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User c_user)
        {
            AES aes = new AES();
            if (this.userDto.CheckDuplicateLogin(c_user.Email))
            {
                c_user.Password = aes.Encrypt(c_user.Password);
                this.userDto.Register(c_user);

                return this.RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Użytkownik o podanym adresie e-mail już istnieje!");

            return this.View();
        }

        /// <summary>
        /// Akcja odpowiedzialna za wylogowanie 
        /// </summary>
        /// <returns>Widok strony głównej</returns>
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return this.RedirectToAction("Index", "Home");
        }

        // GET: /User/Edit/5

        /// <summary>
        /// Akcja odpowiedzilna za widok edycji konta użytkownika
        /// </summary>
        /// <param name="id">Id użytkownika</param>
        /// <returns>Widok edycji użytkownika</returns>
        public ActionResult Edit(int id = 0)
        {
            int userid = this.userDto.GetId(User.Identity.Name);
            if (this.userDto.Edit(userid) == null)
            {
                return this.HttpNotFound();
            }
            Geodeta.Data.User c_user= this.userDto.Edit(userid);
            Debug.WriteLine(c_user.Email);
            Geodeta.Services.User user = new Geodeta.Services.User { ID = c_user.ID, Email = c_user.Email, Password = c_user.Password, FirstName = c_user.FirstName, LastName = c_user.LastName, Token = c_user.Token };
            return this.View(user);
        }

        // POST: /User/Edit/5

        /// <summary>
        /// Educja konta użytkownika
        /// </summary>
        /// <param name="c_user">Użytkownik edytowany</param>
        /// <returns>Widok strony głownej lub edycji użytkownika</returns>
        [HttpPost]
        public ActionResult Edit(User c_user)
        {
            
                if (c_user.CheckboxChangePassword == true)
                {
                    if (c_user.NewPassword == c_user.NewConfirmedPassword)
                    {
                        
                        this.userDto.Edit(c_user);
                        return this.RedirectToAction("Index", "Home");
                    }
                    else {
                        ModelState.AddModelError(string.Empty, "Nowe hasło i potwierdzone nowe hasło się różnią");
                    }
                }
                else {
                    this.userDto.EditWithoutPassword(c_user);
                    return this.RedirectToAction("Index", "Home");
                }
                
            
            return this.View(c_user);
        }

        /// <summary>
        /// Sprawdzanie poprawności danych
        /// </summary>
        /// <param name="email">Email użytkownika</param>
        /// <param name="password">Hasło użytkownika</param>
        /// <returns>True jeżeli prawidłowe</returns>
        private bool IsValid(string email, string password)
        {
            // var crypto = new SimpleCrypto.PBKDF2();s
            bool is_valid = false;

            // password = EncryptPassword(password);
            is_valid = this.userDto.IsValid(email, password);

            return is_valid;
        }

        /// <summary>
        /// Akcja odpowiedzilna za widok przypomnienia hasła
        /// </summary>
        /// <returns>Widok przypomnienia hasła</returns>
        public ActionResult PasswordReminder()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PasswordReminder(User c_user)
        {
            int userid = userDto.GetId(c_user.Email);
            if (userid!=0)
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
                userDto.SetNewPassword(userid,result.ToString());
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(
               "testgeodeta@gmail.com",
              c_user.Email,
               "Wygenerowane nowe hasło",
               "Twoje nowe hasło do logowania to: ");
                string body = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">";
                body += "<HTML><HEAD><META http-equiv=Content-Type content=\"text/html; charset=utf-8\">";
                body += "</HEAD><BODY><DIV><FONT face=Arial color=#EB7261 size=4>Twoje nowe haslo logowania to:"+"</br>" + result.ToString();
                body += "</FONT></DIV></BODY></HTML>";
                ContentType mimeType = new System.Net.Mime.ContentType("text/html");
                // Add the alternate body to the message.

                AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
                message.AlternateViews.Add(alternate);
                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587; 
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false; 
                smtp.Credentials = new NetworkCredential("testgeodeta@gmail.com", "geodeta123");  
                smtp.Host = "smtp.gmail.com";
                smtp.Send(message);
          
                return this.RedirectToAction("SuccessGeneratePassword", "User");
            }
            ModelState.AddModelError(string.Empty, "Nie ma zarejestrowanego użytkownika o podanym adresie e-mail");
            return this.View();
        }

        /// <summary>
        /// Akcja odpowiedzilna za widok powiadomienia o sukcesie zmiany hasła
        /// </summary>
        /// <returns>Widok sukcesu zmiany hasła</returns>
        public ActionResult SuccessGeneratePassword()
        {
            return this.View();
        }
    }
}