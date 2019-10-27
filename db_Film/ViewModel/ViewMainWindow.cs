﻿using db_Film.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using db_Film.View;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace db_Film.ViewModel
{
    class ViewMainWindow : NotifyModel
    {
        string _typeNameString;
        public object TypeName { get => _typeNameString; set { _typeNameString = ((TabItem)value).Header.ToString(); RaiseEvent(nameof(TypeName)); } }
        // Я хз как правильно это все привести

        public ObservableCollection<Film> Films { get; set; }
        public ObservableCollection<Film> FFilms { get; set; }

        public ObservableCollection<Film> Serials { get; set; }
        public ObservableCollection<Film> FSerials { get; set; }

        public ObservableCollection<Film> ANIME { get; set; }
        public ObservableCollection<Film> FANIME { get; set; }

        public ObservableCollection<Dick> LGenre { get; set; }
        public ObservableCollection<Dick> LCountry { get; set; }
        public ObservableCollection<Dick> LProducer { get; set; }
        public ObservableCollection<Dick> LAgeRate { get; set; }
        public ObservableCollection<Dick> LType{ get; set; }



        Film _edit;
        public Film EditFilm { get { return _edit; } set { _edit = value; RaiseEvent(nameof(EditFilm)); } }

        public CustomCommand<string> OpenSmt { get; set; }
        public CustomCommand<Film> ButtonEdit { get; set; }
        public CustomCommand<Film> ButtonAdd { get; set; }
        public CustomCommand<Film> ButtonRemove { get; set; }

        public CustomCommand<Film> FindIndex { get; set; }

        public CustomCommand<string> Save { get; set; }
        public CustomCommand<Film> FiFnd { get; set; }
        public CustomCommand<Poopy> DropIt { get; set; }


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
            Data.AgeRate= LAgeRate;
            LType = new ObservableCollection<Dick>(DickSQL.GetDick("type"));
            Data.Type = LType;


            FindIndex = new CustomCommand<Film>(
                (s) =>
                {
                    
                });


            Save = new CustomCommand<string>(
                (s) => 
                {
                    switch (/* А где взять*/)
                    {
                        case "Save": { EditFilm = null; break; }
                        case "Add":{
                                    switch (EditFilm.Type.Name)
                                    {
                                        case "Film":   { Films.Add(EditFilm); break; }
                                        case "Serial": { Serials.Add(EditFilm); break; }
                                        case "ANIME":  { Films.Add(EditFilm); break; }
                                    }
                                    EditFilm = null; break; 
                                    }

                    }

                });



            OpenSmt = new CustomCommand<string>(
                (s) =>
                {
                    switch (s)
                    {
                        case "connect": { new Option().ShowDialog(); break;}
                        case "dickpick": { new DictionaryEditor().ShowDialog(); break; }
                        case "Abort": { new Abort().ShowDialog(); break; }

                    }
                });
            ButtonEdit = new CustomCommand<Film>((film) => {
                EditFilm = film;
            });

            ButtonAdd = new CustomCommand<Film>((film) =>
                {
                    Film NewFilm = new Film();
                    EditFilm = NewFilm;
                    //Тут нужно еще ID докинуть фильму
                });

            ButtonRemove = new CustomCommand<Film>((film) =>
            {
                switch (film.Type.Name)
                {
                    case "Film": {Films.Remove(film); break;}
                    case "Serial": {Serials.Remove(film); break;}
                    case "ANIME": {Films.Remove(film); break; }
                }
            }, ()=> EditFilm != null);
            // Предупредить еще БД нужно 


            DropIt = new CustomCommand<Poopy>(
                (s) =>
                {

                });

            FiFnd = new CustomCommand<Film>(
                (s) => 
                {
                    // Вероятно делать событине на привязку к FFilm а тут его как-то заполнить нужно
                    // А тут придумать, как заполнить тот самый FFilm
                });
        }

    }
}
