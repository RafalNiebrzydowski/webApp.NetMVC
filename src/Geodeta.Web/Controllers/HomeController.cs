//-----------------------------------------------------------------------
// <copyright file="HomeController.cs" company="aa">
//     Rafał Niebrzydowski
// </copyright>
//-----------------------------------------------------------------------
namespace Inzynierka.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Kontroler Strony głównej
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Akcja zwracająca główną strone
        /// </summary>
        /// <returns>Widok strony głównej</returns>
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return this.View();
        }
        public ActionResult AppAndroid()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return this.View();
        }
        public ActionResult Help()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return this.View();
        }
    }
}
