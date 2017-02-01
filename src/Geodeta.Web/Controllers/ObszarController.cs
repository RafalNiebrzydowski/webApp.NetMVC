//-----------------------------------------------------------------------
// <copyright file="ObszarController.cs" company="aa">
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
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using Geodeta.Services;
    using PagedList;
    using WebMatrix.WebData;

    /// <summary>
    /// Kontroler widoków dotyczących obszarów
    /// </summary>
    public class ObszarController : Controller
    {
        /// <summary>
        /// Obiekt DTO obszaru
        /// </summary>
        private AreaDTO areaDto = new AreaDTO();

        /// <summary>
        /// Obiekt DTO obszaru punktu
        /// </summary>
        private PointDTO pointDto = new PointDTO();

        /// <summary>
        /// Obiekt DTO linii
        /// </summary>
        private LineDTO lineDto = new LineDTO();

        /// <summary>
        /// Obiekt DTO listy punktów
        /// </summary>
        private ListOfPointsDTO lofDto = new ListOfPointsDTO();

        /// <summary>
        /// Obiekt DTO notatki
        /// </summary>
        private NoteDTO noteDto = new NoteDTO();

        /// <summary>
        /// Obiekt DTO użytkownika
        /// </summary>
        private UserDTO userDto = new UserDTO();

        /// <summary>
        /// Pobiera lub ustawia id dodanego obszaru
        /// </summary>
        public int AddedIdArea { get; set; }

        /// <summary>
        /// Pobiera lub ustawia id nowej notatki
        /// </summary>
        public int NewNoteId { get; set; }

        // GET: /Obszar/

        /// <summary>
        /// Akcja do obsługi widoku zarządzania obszarami
        /// </summary>
        /// <param name="sortOrder">Sortowanie rosąco lub malejąco</param>
        /// <param name="currentFilter">Filtrowanie</param>
        /// <param name="searchString">Szukana fraza</param>
        /// <param name="page">Numer strony</param>
        /// <returns>Widok z listą obszarów użytkownika</returns>
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : string.Empty;
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;

                var areas = this.areaDto.OrderGetAllAreas(User.Identity.Name);

                if (!String.IsNullOrEmpty(searchString))
                {
                    areas = areas.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));
                }

                switch (sortOrder)
                {
                    case "name_desc":
                        areas = areas.OrderByDescending(s => s.Name);
                        break;
                    case "Date":
                        areas = areas.OrderBy(s => s.DateMod);
                        break;
                    case "date_desc":
                        areas = areas.OrderByDescending(s => s.DateMod);
                        break;
                    default:
                        areas = areas.OrderBy(s => s.Name);
                        break;
                }

                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return this.View(areas.ToPagedList(pageNumber, pageSize));
            }

            return this.RedirectToAction("Index", "Home");
        }

        // GET: /Obszar/Details/5

        /// <summary>
        /// Akcja odpowiedzialna za widok podglądu obszaru
        /// </summary>
        /// <param name="id">Id obszaru</param>
        /// <returns>Widok podglądu obszaru</returns>
        public ActionResult Details(int id = 0)
        {
            if (this.areaDto.AreaDetails(id) == null)
            {
                return this.HttpNotFound();
            }

            if (Request.IsAuthenticated)
            {
                return this.View(this.areaDto.AreaDetails(id));
            }

            return this.RedirectToAction("Index", "Home");
        }

        // GET: /Obszar/Create

        /// <summary>
        /// Akcja odpowiedzialna za widok tworzenia obszaru
        /// </summary>
        /// <returns>Widok tworzenia nowego obszaru</returns>
        public ActionResult Create()
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.UserId = 1;
                return this.View();
            }

            return this.RedirectToAction("Index", "Home");
        }

        // POST: /Obszar/Create

        /// <summary>
        /// Akcja dodawania obszaru do bazy
        /// </summary>
        /// <param name="obszar">Obszar dodawany</param>
        /// <returns>Json z paramterem Success ustawionym na true i z id dodanego obszaru jeżeli dodanie obszaru się powiodło</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Area obszar)
        {
            if (ModelState.IsValid)
            {
                obszar.DateMod = System.DateTime.Now;
                obszar.IsNewVersion = false;
                obszar.UserId = this.userDto.GetId(User.Identity.Name);
                this.AddedIdArea = this.areaDto.CreateArea(obszar);

                return this.Json(new { Success = true, id2 = this.AddedIdArea });
            }

            return this.View();
        }

        /// <summary>
        /// Usuwanie listy punktów
        /// </summary>
        /// <param name="id3">Id obszaru</param>
        public void RemoveListOfPoints(int id3)
        {
            this.areaDto.RemoveListOfPoints(id3);
        }

        /// <summary>
        /// Dodawania punktu
        /// </summary>
        /// <param name="x">współrzędna x</param>
        /// <param name="y">współrzędna y</param>
        [HttpPost]
        public void PointCreate(double x, double y)
        {
            this.pointDto.PointCreate(x, y);
        }

        /// <summary>
        /// Dodawanie linii
        /// </summary>
        /// <param name="id3">Id obszaru</param>
        public void LineCreate(int id3)
        {
            this.lineDto.LineCreate(id3);
        }

        /// <summary>
        /// Dodawanie listy punktów
        /// </summary>
        /// <param name="a">Id pierwszego punktu</param>
        /// <param name="b">Id drugiego punktu</param>
        public void ListOfPointCreate(int a, int b)
        {
            this.lofDto.ListOfPointCreate(a, b);
        }

        /// <summary>
        /// Dodawanie punktów, linii, listy punktów
        /// </summary>
        /// <param name="id3">id obszaru</param>
        /// <param name="x">Lista współrzędnych x</param>
        /// <param name="y">Lista współrzędnych y</param>
        /// <param name="note">Lista notatek</param>
        public void CreateListOfPoints(int id3, List<double> x, List<double> y, List<string> notetitlepoint, List<string> notecontentpoint, List<string> notetitleline, List<string> notecontentline)
        {
            this.areaDto.CreateListOfPoints(id3, x, y, notetitlepoint, notecontentpoint, notetitleline, notecontentline);
        }

        /// <summary>
        /// Pobieranie listy punktów 
        /// </summary>
        /// <param name="id">Id obszaru</param>
        /// <returns>Json z listą puntków</returns>
        public ActionResult GetPoints(int id)
        {
            return this.Json(this.pointDto.GetPoints(id).ToList(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Pobieranie listy linii 
        /// </summary>
        /// <param name="id">Id obszaru</param>
        /// <returns>Json z listą linii</returns>
        public ActionResult GetLines(int id)
        {
            return this.Json(this.lineDto.GetLines(id).ToList(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Pobieranie notatki 
        /// </summary>
        /// <param name="id">Id notatki</param>
        /// <returns>Json z notatką</returns>
        public ActionResult GetNote(int id)
        {
            return this.Json(this.noteDto.NoteDetails(id), JsonRequestBehavior.AllowGet);
        }

        // GET: /Obszar/Edit/5

        /// <summary>
        /// Akcja dotycząca widoku edycji obszaru
        /// </summary>
        /// <param name="id">Id obszaru</param>
        /// <returns>Widok edycji obszaru</returns>
        public ActionResult Edit(int id = 0)
        {
            if (this.areaDto.Edit(id) == null)
            {
                return this.HttpNotFound();
            }

            if (Request.IsAuthenticated)
            {
                return this.View(this.areaDto.Edit(id));
            }

            return this.RedirectToAction("Index", "Home");
        }

        // POST: /Obszar/Edit/5

        /// <summary>
        /// Akcja edycji obszaru
        /// </summary>
        /// <param name="obszar">Obszar edytowany</param>
        /// <returns>Json z parametrem Success ustawionym na true jeżeli edycja przebiegła prawidłowo</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Area obszar)
        {
            if (ModelState.IsValid)
            {
                obszar.DateMod = System.DateTime.Now;
                obszar.IsNewVersion = true;
                obszar.UserId = this.userDto.GetId(User.Identity.Name);
                this.areaDto.Edit(obszar);
                return this.Json(new { Success = true });
            }

            return this.View(obszar);
        }

        // POST: /Obszar/Delete/5

        /// <summary>
        /// Akcja usuwania obszaru
        /// </summary>
        /// <param name="id">Id obszaru</param>
        /// <returns>Json z parametrem Success ustawiony na true jeżeli usunięcie przebiegło prawidłowo</returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                this.areaDto.DeleteConfirmed(id);
                return this.Json(new { Success = true });
            }

            return this.Json(new { Success = false });
        }

        /*
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }*/

        /// <summary>
        /// Edycja notatki
        /// </summary>
        /// <param name="id">Id notatki</param>
        /// <param name="title">Tytuł notatki</param>
        /// <param name="content">Zawartość notatki</param>
        [HttpPost]
        public void EditNote(int id, string title, string content)
        {
            if (ModelState.IsValid)
            {
                this.noteDto.EditNote(id, title, content);
            }
        }

        /// <summary>
        /// Akcja dodawania notatki
        /// </summary>
        /// <param name="pointId">Id punktu</param>
        /// <param name="title">Tytuł notatki</param>
        /// <param name="content">Zawartość notatki</param>
        /// <returns>Json z parametrem Success ustawionym na true i z id dodanej notatki jeżeli dodawania przebiegło prawidłowo</returns>
        [HttpPost]
        public ActionResult CreateNote(int pointId, string title, string content)
        {
            if (ModelState.IsValid)
            {
                this.NewNoteId = this.noteDto.CreateNote(pointId, title, content);

                return this.Json(new { Success = true, newNoteId = this.NewNoteId });
            }

            return this.Json(new { Success = false });
        }

        /// <summary>
        /// Usuwanie notatki
        /// </summary>
        /// <param name="id">Id notatki</param>
        public void DeleteNote(int id)
        {
            this.noteDto.DeleteNote(id);
        }
    }
}