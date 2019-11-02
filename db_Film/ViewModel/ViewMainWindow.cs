using db_Film.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using db_Film.View;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;

namespace db_Film.ViewModel
{
    class ViewMainWindow : NotifyModel
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        public static extern int StrCmpLogicalW(string psz1, string psz2);
        string lastColumn;
        bool columnChecher;



        string _typeNameString;
        public object TypeName { get => _typeNameString; set { _typeNameString = ((TabItem)value).Header.ToString(); RaiseEvent(nameof(TypeName)); } }
        // Я хз как правильно это все привести

        public ObservableCollection<Film> Films { get; set; }
        public ObservableCollection<Film> Serials { get; set; }
        public ObservableCollection<Film> ANIME { get; set; }


        public ObservableCollection<Dick> LGenre { get; set; }
        public ObservableCollection<Dick> LCountry { get; set; }
        public ObservableCollection<Dick> LProducer { get; set; }
        public ObservableCollection<Dick> LAgeRate { get; set; }
        public ObservableCollection<Dick> LType { get; set; }



        Film _edit;
        public Film EditFilm { get { return _edit; } set { _edit = value; RaiseEvent(nameof(EditFilm)); } }

        Film findFilm = new Film();
        public Film FindFilm { get { return findFilm; } set { findFilm = value; RaiseEvent(nameof(FindFilm)); } }

        public CustomCommand<string> OpenSmt { get; set; }
        public CustomCommand<Film> ButtonEdit { get; set; }
        public CustomCommand<Film> ButtonAdd { get; set; }
        public CustomCommand<Film> ButtonRemove { get; set; }

        public CustomCommand<Film> FindIndex { get; set; }

        public CustomCommand<string> Save { get; set; }
        public CustomCommand<Film> FindIt { get; set; }
        public CustomCommand<Poopy> DropIt { get; set; }

        public CustomCommand<Poopy> Export { get; set; }
        public CustomCommand<Poopy> DumpTo { get; set; }
        public CustomCommand<Poopy> DumpFrom { get; set; }




        public ViewMainWindow()
        {
            Films = new ObservableCollection<Film>(FilmSQL.GetFilm("1"));
            Data.Films = Films;
            Serials = new ObservableCollection<Film>(FilmSQL.GetFilm("2"));
            Data.Serials = Serials;
            ANIME = new ObservableCollection<Film>(FilmSQL.GetFilm("3"));
            Data.ANIME = ANIME;

            LGenre = new ObservableCollection<Dick>(DickSQL.GetDick("genre"));
            Data.Genre = LGenre;
            LCountry = new ObservableCollection<Dick>(DickSQL.GetDick("country"));
            Data.Country = LCountry;
            LProducer = new ObservableCollection<Dick>(DickSQL.GetDick("producer"));
            Data.Producer = LProducer;
            LAgeRate = new ObservableCollection<Dick>(DickSQL.GetDick("agerate"));
            Data.AgeRate = LAgeRate;
            LType = new ObservableCollection<Dick>(DickSQL.GetDick("type"));
            Data.Type = LType;


            Save = new CustomCommand<string>(
                (s) =>
                {
                    if (EditFilm.Id == -1)
                    {
                        //EditFilm.Id = MySQL.
                        // А вот тут конструктор фильма по полям
                        if (string.IsNullOrEmpty(EditFilm.Comment))
                            EditFilm.Comment = "None";
                        new FilmSQL().IncertFilm(EditFilm);

                        switch (EditFilm.Type.Name)
                        {

                            // Где-то тут нужно обработать ID фильма и закинуть в БД
                            case "Film": { Films.Add(EditFilm); break; }
                            case "Serial": { Serials.Add(EditFilm); break; }
                            case "ANIME": { ANIME.Add(EditFilm); break; }
                        }
                        EditFilm = null;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(EditFilm.Comment))
                            EditFilm.Comment = "None";

                        new FilmSQL().UpdateFilm(EditFilm);

                        if (EditFilm.Type.Name != TypeName.ToString())
                        {
                            switch (EditFilm.Type.Name)
                            {

                                // Где-то тут нужно обработать ID фильма и закинуть в БД
                                case "Film": { Films.Add(EditFilm); break; }
                                case "Serial": { Serials.Add(EditFilm); break; }
                                case "ANIME": { ANIME.Add(EditFilm); break; }
                            }

                            Film temp = EditFilm;
                            foreach (var item in LType)
                            {
                                if (item.Name == TypeName.ToString())
                                {
                                    temp.Type = item;
                                    switch (temp.Type.Name)
                                    {

                                        // Где-то тут нужно обработать ID фильма и закинуть в БД
                                        case "Film": { Films.Remove(EditFilm); break; }
                                        case "Serial": { Serials.Remove(EditFilm); break; }
                                        case "ANIME": { ANIME.Remove(EditFilm); break; }
                                    }
                                }
                            }
                        }
                        EditFilm = null;
                    }
                }, () =>
                {
                    if (EditFilm == null)
                        return false;
                    else if ((EditFilm.Type != null && !string.IsNullOrEmpty(EditFilm.Name) && !string.IsNullOrEmpty(EditFilm.Score) && EditFilm.Producer != null
                                && EditFilm.Genre != null && EditFilm.Country != null && EditFilm.AgeRate != null && EditFilm.Year != null))
                        return true;
                    else return false;
                });



            OpenSmt = new CustomCommand<string>(
                (s) =>
                {
                    switch (s)
                    {
                        case "connect": { new Option().ShowDialog(); break; }
                        case "dickpick": { new DictionaryEditor().ShowDialog(); break; }
                        case "Abort": { new Abort().ShowDialog(); break; }
                    }
                });
            ButtonEdit = new CustomCommand<Film>((film) =>
            {
                EditFilm = film;
            });

            ButtonAdd = new CustomCommand<Film>((film) =>
                {
                    Film NewFilm = new Film() { Id = -1 };
                    EditFilm = NewFilm;
                });

            ButtonRemove = new CustomCommand<Film>((film) =>
            {
                new FilmSQL().RemoveFilm(film);
                switch (film.Type.Name)
                {
                    case "Film": { Films.Remove(film); break; }
                    case "Serial": { Serials.Remove(film); break; }
                    case "ANIME": { ANIME.Remove(film); break; }
                }
            }, () => EditFilm != null);
            // Предупредить еще БД нужно 


            DropIt = new CustomCommand<Poopy>(
                (s) =>
                {
                    Films = new ObservableCollection<Film>(FilmSQL.GetFilm("1"));
                    Data.Films = Films;
                    Serials = new ObservableCollection<Film>(FilmSQL.GetFilm("2"));
                    Data.Serials = Serials;
                    ANIME = new ObservableCollection<Film>(FilmSQL.GetFilm("3"));
                    Data.ANIME = ANIME;

                    RaiseEvent(nameof(Films));
                    RaiseEvent(nameof(Serials));
                    RaiseEvent(nameof(ANIME));

                    FindFilm = new Film();
                });

            FindIt = new CustomCommand<Film>(
                (s) =>
                {
                    string where = " ";

                    if (!string.IsNullOrEmpty(FindFilm.Name))
                        where += $"AND tbl_film.`Name` LIKE '%{FindFilm.Name}%' ";
                    if (FindFilm.Producer != null)
                        where += $"AND tbl.film.Producer = {FindFilm.Producer.ID} ";
                    if (!string.IsNullOrEmpty(FindFilm.Year))
                        where += $"AND tbl_film.`Year` LIKE '%{FindFilm.Year}%' ";
                    if (FindFilm.Genre != null)
                        where += $"AND tbl_film.Genre = {FindFilm.Genre.ID} ";
                    if (FindFilm.Country != null)
                        where += $"AND tbl_film.Country = {FindFilm.Country.ID} ";
                    if (FindFilm.AgeRate != null)
                        where += $"AND tbl_film.AgeRate = {FindFilm.AgeRate.ID} ";
                    if (!string.IsNullOrEmpty(FindFilm.Score))
                        where += $"AND tbl_film.Score = '{FindFilm.Score}' ";

                    switch (TypeName)
                    {
                        case "Film": { Films = new ObservableCollection<Film>(FilmSQL.FindFilm("1 ", where)); RaiseEvent(nameof(Films)); break; }
                        case "Serial": { Serials = new ObservableCollection<Film>(FilmSQL.FindFilm("2 ", where)); RaiseEvent(nameof(Serials)); break; }
                        case "ANIME": { ANIME = new ObservableCollection<Film>(FilmSQL.FindFilm("3 ", where)); RaiseEvent(nameof(ANIME)); break; }
                    }
                    
                }, () => (!string.IsNullOrEmpty(FindFilm.Name) || FindFilm.Producer != null || !string.IsNullOrEmpty(FindFilm.Year) || FindFilm.Genre != null)
                            || FindFilm.Country != null || FindFilm.AgeRate != null || !string.IsNullOrEmpty(FindFilm.Score));



            Export = new CustomCommand<Poopy>(
                (s) =>
                {
                    FileStream fs;

                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        IWorkbook wb = new XSSFWorkbook();
                        ICellStyle styleTex = wb.CreateCellStyle();
                        styleTex.FillForegroundColor = HSSFColor.Gold.Index;
                        styleTex.FillPattern = FillPattern.SolidForeground;
                        ICellStyle styleRow = wb.CreateCellStyle();
                        styleRow.FillForegroundColor = HSSFColor.SkyBlue.Index;
                        styleRow.FillPattern = FillPattern.SolidForeground;



                        ISheet films = wb.CreateSheet("Films");
                        ISheet serials = wb.CreateSheet("Serials");
                        ISheet anime = wb.CreateSheet("ANIME");



                        IRow row = films.CreateRow(0);
                        row.CreateCell(0).SetCellValue("Name");
                        row.GetCell(0).CellStyle = styleTex;
                        row.CreateCell(1).SetCellValue("Producer");
                        row.GetCell(1).CellStyle = styleTex;
                        row.CreateCell(2).SetCellValue("Year");
                        row.GetCell(2).CellStyle = styleTex;
                        row.CreateCell(3).SetCellValue("Genre");
                        row.GetCell(3).CellStyle = styleTex;
                        row.CreateCell(4).SetCellValue("Country");
                        row.GetCell(4).CellStyle = styleTex;
                        row.CreateCell(5).SetCellValue("AgeRate");
                        row.GetCell(5).CellStyle = styleTex;
                        row.CreateCell(6).SetCellValue("Score");
                        row.GetCell(6).CellStyle = styleTex;


                        for (int i = 0; i < Films.Count; i++)
                        {
                            row = films.CreateRow(i + 1);
                            row.CreateCell(0).SetCellValue(Films[i].Name);
                            row.GetCell(0).CellStyle = styleRow;
                            row.CreateCell(1).SetCellValue(Films[i].Producer.Name);
                            row.GetCell(1).CellStyle = styleRow;
                            row.CreateCell(2).SetCellValue(Films[i].Year);
                            row.GetCell(2).CellStyle = styleRow;
                            row.CreateCell(3).SetCellValue(Films[i].Genre.Name);
                            row.GetCell(3).CellStyle = styleRow;
                            row.CreateCell(4).SetCellValue(Films[i].Country.Name);
                            row.GetCell(4).CellStyle = styleRow;
                            row.CreateCell(5).SetCellValue(Films[i].AgeRate.Name);
                            row.GetCell(5).CellStyle = styleRow;
                            row.CreateCell(6).SetCellValue(Films[i].Score);
                            row.GetCell(6).CellStyle = styleRow;
                        }

                        row = serials.CreateRow(0);
                        row.CreateCell(0).SetCellValue("Name");
                        row.GetCell(0).CellStyle = styleTex;
                        row.CreateCell(1).SetCellValue("Producer");
                        row.GetCell(1).CellStyle = styleTex;
                        row.CreateCell(2).SetCellValue("Year");
                        row.GetCell(2).CellStyle = styleTex;
                        row.CreateCell(3).SetCellValue("Genre");
                        row.GetCell(3).CellStyle = styleTex;
                        row.CreateCell(4).SetCellValue("Country");
                        row.GetCell(4).CellStyle = styleTex;
                        row.CreateCell(5).SetCellValue("AgeRate");
                        row.GetCell(5).CellStyle = styleTex;
                        row.CreateCell(6).SetCellValue("Score");
                        row.GetCell(6).CellStyle = styleTex;

                        for (int i = 0; i < Serials.Count; i++)
                        {
                            row = serials.CreateRow(i + 1);
                            row.CreateCell(0).SetCellValue(Serials[i].Name);
                            row.GetCell(0).CellStyle = styleRow;
                            row.CreateCell(1).SetCellValue(Serials[i].Producer.Name);
                            row.GetCell(1).CellStyle = styleRow;
                            row.CreateCell(2).SetCellValue(Serials[i].Year);
                            row.GetCell(2).CellStyle = styleRow;
                            row.CreateCell(3).SetCellValue(Serials[i].Genre.Name);
                            row.GetCell(3).CellStyle = styleRow;
                            row.CreateCell(4).SetCellValue(Serials[i].Country.Name);
                            row.GetCell(4).CellStyle = styleRow;
                            row.CreateCell(5).SetCellValue(Serials[i].AgeRate.Name);
                            row.GetCell(5).CellStyle = styleRow;
                            row.CreateCell(6).SetCellValue(Serials[i].Score);
                            row.GetCell(6).CellStyle = styleRow;

                        }

                        row = anime.CreateRow(0);
                        row.CreateCell(0).SetCellValue("Name");
                        row.GetCell(0).CellStyle = styleTex;
                        row.CreateCell(1).SetCellValue("Producer");
                        row.GetCell(1).CellStyle = styleTex;
                        row.CreateCell(2).SetCellValue("Year");
                        row.GetCell(2).CellStyle = styleTex;
                        row.CreateCell(3).SetCellValue("Genre");
                        row.GetCell(3).CellStyle = styleTex;
                        row.CreateCell(4).SetCellValue("Country");
                        row.GetCell(4).CellStyle = styleTex;
                        row.CreateCell(5).SetCellValue("AgeRate");
                        row.GetCell(5).CellStyle = styleTex;
                        row.CreateCell(6).SetCellValue("Score");
                        row.GetCell(6).CellStyle = styleTex;

                        for (int i = 0; i < ANIME.Count; i++)
                        {
                            row = anime.CreateRow(i + 1);
                            row.CreateCell(0).SetCellValue(ANIME[i].Name);
                            row.GetCell(0).CellStyle = styleRow;
                            row.CreateCell(1).SetCellValue(ANIME[i].Producer.Name);
                            row.GetCell(1).CellStyle = styleRow;
                            row.CreateCell(2).SetCellValue(ANIME[i].Year);
                            row.GetCell(2).CellStyle = styleRow;
                            row.CreateCell(3).SetCellValue(ANIME[i].Genre.Name);
                            row.GetCell(3).CellStyle = styleRow;
                            row.CreateCell(4).SetCellValue(ANIME[i].Country.Name);
                            row.GetCell(4).CellStyle = styleRow;
                            row.CreateCell(5).SetCellValue(ANIME[i].AgeRate.Name);
                            row.GetCell(5).CellStyle = styleRow;
                            row.CreateCell(6).SetCellValue(ANIME[i].Score);
                            row.GetCell(6).CellStyle = styleRow;

                        }
                        try
                        {
                            fs = File.Create(fbd.SelectedPath + "\\Films.xlsx");
                            wb.Write(fs);
                            fs.Close();
                        }
                        catch (Exception)
                        {
                            System.Windows.MessageBox.Show("Close Document First");

                        }

                    }


                });

            DumpTo = new CustomCommand<Poopy>(
                (s) =>
                {

                });

            DumpFrom = new CustomCommand<Poopy>(
                (s) =>
                {

                });
        }

        internal void RaiseSort(string header, IEnumerable itemsSource)
        {
            if (lastColumn == header)
                if (columnChecher) columnChecher = false;
                else columnChecher = true;
            else columnChecher = false;
            lastColumn = header;

            var list = (ObservableCollection<Film>)itemsSource;
            if (Films == list)
            {
                var temp = Films.ToList();
                temp.Sort((x, y) =>
                {
                    if (header == "Name" || header == "Year" || header == "Score")
                    {
                        var prop = typeof(Film).GetProperty(header);
                        if (columnChecher) return StrCmpLogicalW(prop.GetValue(x).ToString(), prop.GetValue(y).ToString());
                        else return StrCmpLogicalW(prop.GetValue(y).ToString(), prop.GetValue(x).ToString());
                    }
                    else
                    {
                        switch (header)
                        {
                            case "Producer":
                                if (columnChecher) return StrCmpLogicalW(x.Producer.Name.ToString(), y.Producer.Name.ToString());
                                else return StrCmpLogicalW(y.Producer.Name.ToString(), x.Producer.Name.ToString());

                            case "Country":
                                if (columnChecher) return StrCmpLogicalW(x.Country.Name.ToString(), y.Country.Name.ToString());
                                else return StrCmpLogicalW(y.Country.Name.ToString(), x.Country.Name.ToString());
                            case "AgeRate":
                                if (columnChecher) return StrCmpLogicalW(x.AgeRate.Name.ToString(), y.AgeRate.Name.ToString());
                                else return StrCmpLogicalW(y.AgeRate.Name.ToString(), x.AgeRate.Name.ToString());
                            default:
                                if (columnChecher) return StrCmpLogicalW(x.Genre.Name.ToString(), y.Genre.Name.ToString());
                                else return StrCmpLogicalW(y.Genre.Name.ToString(), x.Genre.Name.ToString());

                        }




                    }
                });
                Films = new ObservableCollection<Film>(temp);
                RaiseEvent(nameof(Films));
            }
            //...............
            if (Serials == list)
            {
                var temp = Serials.ToList();
                temp.Sort((x, y) =>
                {
                    if (header == "Name" || header == "Year" || header == "Score")
                    {
                        var prop = typeof(Film).GetProperty(header);
                        if (columnChecher) return StrCmpLogicalW(prop.GetValue(x).ToString(), prop.GetValue(y).ToString());
                        else return StrCmpLogicalW(prop.GetValue(y).ToString(), prop.GetValue(x).ToString());
                    }
                    else
                    {
                        switch (header)
                        {
                            case "Producer":
                                if (columnChecher) return StrCmpLogicalW(x.Producer.Name.ToString(), y.Producer.Name.ToString());
                                else return StrCmpLogicalW(y.Producer.Name.ToString(), x.Producer.Name.ToString());

                            case "Country":
                                if (columnChecher) return StrCmpLogicalW(x.Country.Name.ToString(), y.Country.Name.ToString());
                                else return StrCmpLogicalW(y.Country.Name.ToString(), x.Country.Name.ToString());
                            case "AgeRate":
                                if (columnChecher) return StrCmpLogicalW(x.AgeRate.Name.ToString(), y.AgeRate.Name.ToString());
                                else return StrCmpLogicalW(y.AgeRate.Name.ToString(), x.AgeRate.Name.ToString());
                            default:
                                if (columnChecher) return StrCmpLogicalW(x.Genre.Name.ToString(), y.Genre.Name.ToString());
                                else return StrCmpLogicalW(y.Genre.Name.ToString(), x.Genre.Name.ToString());

                        }


                    }
                });
                Serials = new ObservableCollection<Film>(temp);
                RaiseEvent(nameof(Serials));
            }
            if (ANIME == list)
            {
                var temp = ANIME.ToList();
                temp.Sort((x, y) =>
                {
                    if (header == "Name" || header == "Year" || header == "Score")
                    {
                        var prop = typeof(Film).GetProperty(header);
                        if (columnChecher) return StrCmpLogicalW(prop.GetValue(x).ToString(), prop.GetValue(y).ToString());
                        else return StrCmpLogicalW(prop.GetValue(y).ToString(), prop.GetValue(x).ToString());
                    }
                    else
                    {
                        switch (header)
                        {
                            case "Producer":
                                if (columnChecher) return StrCmpLogicalW(x.Producer.Name.ToString(), y.Producer.Name.ToString());
                                else return StrCmpLogicalW(y.Producer.Name.ToString(), x.Producer.Name.ToString());

                            case "Country":
                                if (columnChecher) return StrCmpLogicalW(x.Country.Name.ToString(), y.Country.Name.ToString());
                                else return StrCmpLogicalW(y.Country.Name.ToString(), x.Country.Name.ToString());
                            case "AgeRate":
                                if (columnChecher) return StrCmpLogicalW(x.AgeRate.Name.ToString(), y.AgeRate.Name.ToString());
                                else return StrCmpLogicalW(y.AgeRate.Name.ToString(), x.AgeRate.Name.ToString());
                            default:
                                if (columnChecher) return StrCmpLogicalW(x.Genre.Name.ToString(), y.Genre.Name.ToString());
                                else return StrCmpLogicalW(y.Genre.Name.ToString(), x.Genre.Name.ToString());

                        }




                    }
                });
                ANIME = new ObservableCollection<Film>(temp);
                RaiseEvent(nameof(ANIME));
            }
        }

    }
}
