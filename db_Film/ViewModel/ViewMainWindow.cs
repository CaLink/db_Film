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

namespace db_Film.ViewModel
{
    class ViewMainWindow : NotifyModel
    {
        string _typeName;
        public string TypeName { get => _typeName; set { _typeName = ((TabItem)value).Header.ToString(); RaiseEvent(nameof(TypeName)); } }

        public ObservableCollection<Film> Films { get; set; }
        public ObservableCollection<Film> Serials { get; set; }
        public ObservableCollection<Film> ANIME { get; set; }

        public ObservableCollection<Dick> Genre { get; set; }
        public ObservableCollection<Dick> Country { get; set; }
        public ObservableCollection<Dick> Producer { get; set; }

        Film _edit;
        public Film EditFilm { get { return _edit; } set { _edit = value; RaiseEvent(nameof(EditFilm)); } }

        public CustomCommand<string> OpenSmt { get; set; }
        public CustomCommand<Film> ButtonEdit { get; set; }
        public CustomCommand<Poopy> Save { get; set; }

        public ViewMainWindow()
        {
            Films = new ObservableCollection<Film>(FilmSQL.GetFilm("1"));
            Data.Films = Films;
            Serials = new ObservableCollection<Film>(FilmSQL.GetFilm("2"));
            Data.Serials = Serials;
            ANIME = new ObservableCollection<Film>(FilmSQL.GetFilm("3"));
            Data.ANIME = ANIME;

            Genre = new ObservableCollection<Dick>(DickSQL.GetDick("genre"));
            Data.Genre = Genre;
            Country = new ObservableCollection<Dick>(DickSQL.GetDick("country"));
            Data.Country = Country;
            Producer = new ObservableCollection<Dick>(DickSQL.GetDick("producer"));
            Data.Producer = Producer;

            Save = new CustomCommand<Poopy>((s) => { EditFilm = null;  });



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
        }

    }
}
