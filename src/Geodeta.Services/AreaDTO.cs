//-----------------------------------------------------------------------
// <copyright file="AreaDTO.cs" company="aa">
//     Rafał Niebrzydowski
// </copyright>
//-----------------------------------------------------------------------
namespace Geodeta.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Geodeta.Data;
    using PagedList;

    /// <summary>
    /// DTO obszaru
    /// </summary>
    public class AreaDTO
    {
        /// <summary>
        /// Obiekt bazy danych
        /// </summary>
        private GeodetaEntities1 db = new GeodetaEntities1();

        /// <summary>
        /// DTO użytkownika
        /// </summary>
        private UserDTO userDto = new UserDTO();


        /// <summary>
        /// Pobierania wszystkich obszarów użytkownika
        /// </summary>
        /// <param name="email">Email użytkownika</param>
        /// <returns>Lista obszarów</returns>
        public List<Geodeta.Services.Area> GetAllAreas(string email)
        {
            var id = this.userDto.GetId(email);
            var obszar = this.db.Area.Select(x => new Area
                    {
                        ID = x.ID,
                        Name = x.Name,
                        DateMod = x.DateMod,
                        UserId = x.UserId,
                        IsNewVersion = x.IsNewVersion
                    }
                        ).Where(o => o.UserId == id);
            return obszar.ToList();
        }

        /// <summary>
        /// Lista obszarów do sortowania 
        /// </summary>
        /// <param name="email">Emial użytkownika</param>
        /// <returns>Obszary do sortowanie</returns>
        public IQueryable<Geodeta.Services.Area> OrderGetAllAreas(string email)
        {
            var id = this.userDto.GetId(email);
            var obszar = this.db.Area.Select(x => new Area
            {
                ID = x.ID,
                Name = x.Name,
                DateMod = x.DateMod,
                UserId = x.UserId,
                IsNewVersion = x.IsNewVersion
            }
                        ).Where(o => o.UserId == id);
            return obszar;
        }

        /// <summary>
        /// Pobieranie obszarow użytkownika
        /// </summary>
        /// <param name="id">Id użytkownika</param>
        /// <returns>Lista obszarów danego użytkownika</returns>
        public IEnumerable<Geodeta.Services.Area> GetAreasUser(int id)
        {
            this.db.Configuration.ProxyCreationEnabled = false;

            return this.db.Area.Select(x => new Area
            {
                ID = x.ID,
                Name = x.Name,
                DateMod = x.DateMod,
                UserId = x.UserId,
                IsNewVersion = x.IsNewVersion
            }
                        ).Where(a => a.UserId == id).AsEnumerable();
        }

        /// <summary>
        /// Pobieranie obszaru o danym id
        /// </summary>
        /// <param name="id">Id obszaru</param>
        /// <returns>Obszar o danym id</returns>
        public Geodeta.Services.Area AreaDetails(int id = 0)
        {
            Geodeta.Data.Area obszar = this.db.Area.Find(id);
            if (obszar == null)
            {
                return null;
            }
            Area obszardto = new Area { ID = obszar.ID, Name = obszar.Name, DateMod = obszar.DateMod, UserId = obszar.UserId, IsNewVersion = obszar.IsNewVersion };
            return obszardto;
        }

        /// <summary>
        /// Pobieranie obszaru o danym id
        /// </summary>
        /// <param name="id">Id obszaru</param>
        /// <returns>Obszar o danym id</returns>
        public Area GetArea(int id)
        {
            this.db.Configuration.ProxyCreationEnabled = false;
            Geodeta.Data.Area tmp = this.db.Area.Find(id);
            Area area = new Area { ID = tmp.ID, Name = tmp.Name, UserId = tmp.UserId, IsNewVersion = tmp.IsNewVersion, DateMod = tmp.DateMod };
            if (area == null)
            {
                return null;
            }

            return area;
        }

        /// <summary>
        /// Dodawanie obszaru do bazy danych
        /// </summary>
        /// <param name="obszar">Obszar dodawany</param>
        /// <returns>Id dodanego obszaru</returns>
        public int CreateArea(Area obszar)
        {
            Geodeta.Data.Area area = new Geodeta.Data.Area { ID = obszar.ID, Name = obszar.Name, UserId = obszar.UserId, IsNewVersion = obszar.IsNewVersion, DateMod = obszar.DateMod };
            this.db.Area.Add(area);
            this.db.SaveChanges();
            return area.ID;
        }

        /// <summary>
        /// Pobieranie id ostatniego obszaru
        /// </summary>
        /// <returns>Id dodanego obszaru</returns>
        public int GetLastId(string email)
        {
            //int i = userDto.GetId(email);
            this.db.Configuration.ProxyCreationEnabled = false;
            var id = this.db.Area.Max(p => p.ID);
            return id;
        }

        /// <summary>
        /// Dodawanie linii punktów i listy punktów obszaru
        /// </summary>
        /// <param name="id">Id obszaru</param>
        /// <param name="x">Lista wspołrzędnych X punktów</param>
        /// <param name="y">Lista wspołrzędnych Y punktów</param>
        /// <param name="note">Lista id notatek</param>
        public void CreateListOfPoints(int id, List<double> x, List<double> y, List<string> notetitlepoint, List<string> notecontentpoint, List<string> notetitleline, List<string> notecontentline)
        {
            List<int> lineid = new List<int>();
            List<int> pointid = new List<int>();
            Debug.WriteLine(notetitleline.Count);
            for (int i = 0; i < x.Count; i++)
            {
                if (notetitleline[i] != "" && notecontentline[i] != "Brak notatki")
                {
                    Geodeta.Data.Note note1 = this.db.Note.Add(new Geodeta.Data.Note { Title = notetitleline[i], ContentNote = notecontentline[i]});
                    this.db.SaveChanges();
                    Geodeta.Data.Line line = new Geodeta.Data.Line { AreaId = id, NoteId = note1.ID };
                    this.db.Line.Add(line);
                    this.db.SaveChanges();
                    lineid.Add(line.ID);
                }
                else
                {
                    Geodeta.Data.Line line = new Geodeta.Data.Line { AreaId = id };
                    this.db.Line.Add(line);
                    this.db.SaveChanges();
                    lineid.Add(line.ID);
                }

                if (notetitlepoint[i] != "" && notecontentpoint[i] != "Brak notatki")
                {
                    Debug.WriteLine("waruenk 1");
                    Geodeta.Data.Note note1 = this.db.Note.Add(new Geodeta.Data.Note { Title = notetitlepoint[i], ContentNote = notecontentpoint[i] });
                    this.db.SaveChanges();
                    Geodeta.Data.Point point = new Geodeta.Data.Point { CoordinateX = x[i], CoordinateY = y[i], NoteId =note1.ID};
                    this.db.Point.Add(point);
                    this.db.SaveChanges();
                    pointid.Add(point.ID);
                }
                else
                {
                    Debug.WriteLine("waruenk else1");
                    Geodeta.Data.Point point = new Geodeta.Data.Point { CoordinateX = x[i], CoordinateY = y[i]};
                    this.db.Point.Add(point);
                    this.db.SaveChanges();
                    pointid.Add(point.ID);
                }

            }

            for (int i = 0; i < pointid.Count; i++)
            {
                if (i == 0)
                {
                    this.db.ListOfPoints.Add(new Geodeta.Data.ListOfPoints { LineId = lineid[i], PointId = pointid[i] });

                    this.db.SaveChanges();
                }

                if (i > 0 && pointid.Count - 1 > i)
                {
                    this.db.ListOfPoints.Add(new Geodeta.Data.ListOfPoints { LineId = lineid[i - 1], PointId = pointid[i] });

                    this.db.ListOfPoints.Add(new Geodeta.Data.ListOfPoints { LineId = lineid[i], PointId = pointid[i] });

                    this.db.SaveChanges();
                }

                if (pointid.Count - 1 == i)
                {
                    this.db.ListOfPoints.Add(new Geodeta.Data.ListOfPoints { LineId = lineid[i - 1], PointId = pointid[i] });

                    this.db.ListOfPoints.Add(new Geodeta.Data.ListOfPoints { LineId = lineid[i], PointId = pointid[i] });

                    this.db.ListOfPoints.Add(new Geodeta.Data.ListOfPoints { LineId = lineid[i], PointId = pointid[i - (x.Count - 1)] });

                    this.db.SaveChanges();
                }
   
            }
        }

        /// <summary>
        /// Wyszukanie obszaru
        /// </summary>
        /// <param name="id">Id obszaru</param>
        /// <returns>Obszar o danym id</returns>
        public Geodeta.Data.Area Delete(int id = 0)
        {
            Geodeta.Data.Area obszar = this.db.Area.Find(id);
            if (obszar == null)
            {
                return null;
            }

            return obszar;
        }

        /// <summary>
        /// Wyszukanie obszaru
        /// </summary>
        /// <param name="id">Id obszaru</param>
        /// <returns>Obszar o danym id</returns>
        public Geodeta.Data.Area Edit(int id = 0)
        {
            Geodeta.Data.Area obszar = this.db.Area.Find(id);
            if (obszar == null)
            {
                return null;
            }

            return obszar;
        }

        /// <summary>
        /// Usuwanie obszaru z jego punktami listami punktów i liniami
        /// </summary>
        /// <param name="id">Id obszaru</param>
        public void DeleteConfirmed(int id)
        {
            
            var query = from p in this.db.Point
                        from l in this.db.Line
                        from lof in this.db.ListOfPoints
                        where ((l.AreaId == id) && (lof.LineId == l.ID) && (lof.PointId == p.ID))
                        select p;
            List<Geodeta.Data.Point> lista_punktow = query.Distinct().ToList();
            var query2 = from l in this.db.Line
                         where (l.AreaId == id)
                         select l;
            List<Geodeta.Data.Line> lista_linii = query2.ToList();
            var query3 = from l in this.db.Line
                         from lof in this.db.ListOfPoints
                         where ((l.AreaId == id) && (lof.LineId == l.ID))
                         select lof;
            List<Geodeta.Data.ListOfPoints> lista_lof = query3.ToList();
            for (int i = 0; i < lista_lof.Count; i++)
            {
                this.db.ListOfPoints.Remove(lista_lof[i]);

                this.db.SaveChanges();
            }

            for (int i = 0; i < lista_punktow.Count; i++)
            {
                this.db.Point.Remove(lista_punktow[i]);

                this.db.SaveChanges();
            }

            for (int i = 0; i < lista_linii.Count; i++)
            {
                this.db.Line.Remove(lista_linii[i]);

                this.db.SaveChanges();
            }

            Geodeta.Data.Area obszar = this.db.Area.Find(id);
            this.db.Area.Remove(obszar);
            this.db.SaveChanges();
        }

        /// <summary>
        /// Edycja obszaru
        /// </summary>
        /// <param name="obszar">Obszar edytowany</param>
        public void Edit(Area obszar)
        {
            Geodeta.Data.Area area = new Geodeta.Data.Area { ID = obszar.ID, Name = obszar.Name, UserId = obszar.UserId, IsNewVersion=obszar.IsNewVersion, DateMod = System.DateTime.Now };
            this.db.Entry(area).State = EntityState.Modified;
            this.db.SaveChanges();
        }

        /// <summary>
        /// Usuwanie listy punktów linii i punktów
        /// </summary>
        /// <param name="id">Id obszaru</param>
        public void RemoveListOfPoints(int id)
        {
            var query = from p in this.db.Point
                        from l in this.db.Line
                        from lof in this.db.ListOfPoints
                        where ((l.AreaId == id) && (lof.LineId == l.ID) && (lof.PointId == p.ID))
                        select p;
            List<Geodeta.Data.Point> lista_punktow = query.Distinct().ToList();
            var query2 = from l in this.db.Line
                         where (l.AreaId == id)
                         select l;
            List<Geodeta.Data.Line> lista_linii = query2.ToList();
            var query3 = from l in this.db.Line
                         from lof in this.db.ListOfPoints
                         where ((l.AreaId == id) && (lof.LineId == l.ID))
                         select lof;
            List<Geodeta.Data.ListOfPoints> lista_lof = query3.ToList();
            for (int i = 0; i < lista_lof.Count; i++)
            {
                this.db.ListOfPoints.Remove(lista_lof[i]);
                this.db.SaveChanges();
            }

            for (int i = 0; i < lista_punktow.Count; i++)
            {
                this.db.Point.Remove(lista_punktow[i]);
                this.db.SaveChanges();
                if(lista_punktow[i].NoteId.HasValue){
                       int idnote=lista_punktow[i].NoteId.Value;
                       Geodeta.Data.Note notatka = this.db.Note.Find(idnote);
                       this.db.Note.Remove(notatka);
                       this.db.SaveChanges();
                }
            }

            for (int i = 0; i < lista_linii.Count; i++)
            {
                this.db.Line.Remove(lista_linii[i]);
                this.db.SaveChanges();
                if (lista_linii[i].NoteId.HasValue)
                {
                    int idnote = lista_linii[i].NoteId.Value;
                    Geodeta.Data.Note notatka = this.db.Note.Find(idnote);
                    this.db.Note.Remove(notatka);
                    this.db.SaveChanges();
                }
            }
            
        }

        /// <summary>
        /// Pobranie listy id notatek obszaru
        /// </summary>
        /// <param name="id">Id obszaru</param>
        /// <returns>Lista id notatek obszaru</returns>
        public List<int?> GetAreaNoteId(int id)
        {
            List<int?> listid = new List<int?>();
            var query = (from p in this.db.Point
                        from l in this.db.Line
                        from lof in this.db.ListOfPoints
                        where ((l.AreaId == id) && (lof.LineId == l.ID) && (lof.PointId == p.ID))
                        select p.NoteId)
            .Union (from p in this.db.Point
                        from l in this.db.Line
                        from lof in this.db.ListOfPoints
                        where ((l.AreaId == id) && (lof.LineId == l.ID) && (lof.PointId == p.ID))
                        select l.NoteId);
            listid = query.Distinct().ToList();
            return listid;
        }

        /// <summary>
        /// Edycja obszaru o danym id
        /// </summary>
        /// <param name="id">Id area</param>
        /// <param name="a">Informacje do edycji obszaru z pkt i liniami</param>
        /// <returns>Obszar zedytowany</returns>
        public Area PutArea(int id, AreaApi a)
        {
            Debug.WriteLine("weszlo");
            NoteDTO noteDto = new NoteDTO();
            List<double> pointsX = new List<double>();
            List<double> pointsY = new List<double>();
            List<int?> noteid = new List<int?>();
            List<int?> note = new List<int?>();
            for (int i = 0; i < a.PointX.Count(); i++)
            {
                pointsX.Add(double.Parse(a.PointX[i]));
                pointsY.Add(double.Parse(a.PointY[i]));
            }

            noteid = this.GetAreaNoteId(id);
            for (int i = 0; i < noteid.Count(); i++)
            {
                if (noteid[i].HasValue)
                {
                    noteDto.DeleteNote(noteid[i].Value);
                    this.db.SaveChanges();
                }
            }

            Geodeta.Services.Area area = new Geodeta.Services.Area { ID = id, Name = a.Name, UserId = a.UserId, IsNewVersion=a.IsNewVersion, DateMod = a.DateMod };
            this.Edit(area);
            this.db.SaveChanges();
            this.RemoveListOfPoints(id);
            this.CreateListOfPoints(id, pointsX, pointsY, a.Title, a.Content, a.TitleLine, a.ContentLine);
            return area;
        }


        /// <summary>
        /// Edycja obszaru o danym id
        /// </summary>
        /// <param name="id">Id area</param>
        /// <param name="a">Informacje do edycji obszaru z pkt i liniami</param>
        /// <returns>Obszar zedytowany</returns>
        public Area PutAreaSync(int id)
        {
            Geodeta.Data.Area area = this.db.Area.Find(id);
            area.IsNewVersion = false;
            Area tmp = new Area { Name = area.Name, ID = area.ID, UserId = area.UserId, DateMod = area.DateMod, IsNewVersion = area.IsNewVersion };
            this.db.Entry(area).State = EntityState.Modified;
            this.db.SaveChanges();
            return tmp;
        }

        /// <summary>
        /// Dodawanie nowego obszaru
        /// </summary>
        /// <param name="a">Informacje do stworzenia obszaru z pkt i liniami</param>
        /// <returns>Obszar dododany</returns>
        public Area PostArea(AreaApi a)
        {
            List<double> pointsX = new List<double>();
            List<double> pointsY = new List<double>();
            List<int?> note = new List<int?>();
            for (int i = 0; i < a.PointX.Count(); i++)
            {
                pointsX.Add(double.Parse(a.PointX[i]));
                pointsY.Add(double.Parse(a.PointY[i]));
            }

            Geodeta.Data.Area area;
            Debug.WriteLine(a.Name + " " + a.UserId + " " + a.IsNewVersion + " " + a.DateMod);
            area = this.db.Area.Add(new Geodeta.Data.Area { Name = a.Name, UserId = a.UserId, IsNewVersion = a.IsNewVersion, DateMod = a.DateMod });

            this.db.SaveChanges();
            Area tmp = new Area { ID = area.ID, Name = area.Name, UserId = area.UserId, DateMod = area.DateMod };
            this.CreateListOfPoints(area.ID, pointsX, pointsY, a.Title, a.Content, a.TitleLine, a.ContentLine);
            return tmp;
        }
    }
}
