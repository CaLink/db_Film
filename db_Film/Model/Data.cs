using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace db_Film.Model
{
    public class Data
    {
        //public static Data staticInstance = new Data();

        static public ObservableCollection<Film> Films { get; set; }
        static public ObservableCollection<Film> Serials { get; set; }
        static public ObservableCollection<Film> ANIME { get; set; }

        static public ObservableCollection<Dick> Genre { get; set; }
        static public ObservableCollection<Dick> Country { get; set; }
        static public ObservableCollection<Dick> Producer { get; set; }
        static public ObservableCollection<Dick> AgeRate{ get; set; }
        static public ObservableCollection<Dick> Type { get; set; }




        //public override object ProvideValue(IServiceProvider serviceProvider)
        //{
        //    return staticInstance;
        //}
    }
}
