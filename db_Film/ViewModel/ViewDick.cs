using db_Film.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db_Film.ViewModel
{
    class ViewDick : NotifyModel
    {
        DickView _view;
        public DickView SelectedDick { get { return _view; } set { _view = value;RaiseEvent(nameof(SelectedDick)); } }

        public ObservableCollection<DickView> ListDicks { get; set; } = new ObservableCollection<DickView>();

        Dick _editDickItem;
        public Dick EditDickItem { get { return _editDickItem; } set { _editDickItem = value; RaiseEvent(nameof(EditDickItem)); } }

        public ViewDick()
        {
            
            ListDicks.Add(new DickView { Array = Data.Genre, Name = "Genres" });
            ListDicks.Add(new DickView { Array = Data.Country, Name = "Country" });
            ListDicks.Add(new DickView { Array = Data.Producer, Name = "Producer" });
        }
    }

    class DickView
    {
        public string Name { get; set; }
        public ObservableCollection<Dick> Array { get; set; }
    }

}
