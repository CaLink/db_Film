using db_Film.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace db_Film.ViewModel
{
    class ViewDick : NotifyModel
    {
        DickView _view;
        public DickView SelectedDick { get { return _view; } set { _view = value; RaiseEvent(nameof(SelectedDick)); } }

        public ObservableCollection<DickView> ListDicks { get; set; } = new ObservableCollection<DickView>();

        Dick _editDickItem;
        public Dick EditDickItem { get { return _editDickItem; } set { _editDickItem = value; RaiseEvent(nameof(EditDickItem)); } }

        public CustomCommand<string> Save { get; set; }
        public CustomCommand<string> Add { get; set; }
        public CustomCommand<Dick> Del { get; set; }
        




        public ViewDick()
        {

            ListDicks.Add(new DickView { Array = Data.Genre, Name = "Genre" });
            ListDicks.Add(new DickView { Array = Data.Country, Name = "Country" });
            ListDicks.Add(new DickView { Array = Data.Producer, Name = "Producer" });

            Save = new CustomCommand<string>(
                (s) =>
                {
                    if (string.IsNullOrEmpty(EditDickItem.Name))
                        return;
                    if (EditDickItem.ID == -1)
                    {
                        new DickSQL().InputDick(EditDickItem, SelectedDick.Name);
                        
                        SelectedDick.Array.Add(EditDickItem); RaiseEvent(nameof(SelectedDick.Array));
                        
                        EditDickItem = null;

                    }
                    else
                    {

                        new DickSQL().UpdateDick(EditDickItem, SelectedDick.Name);

                        EditDickItem = null;
                    }


                }, () => EditDickItem != null);

            Add = new CustomCommand<string>(
                (s) =>
                {
                    Dick newDick = new Dick() { ID = -1 };
                    EditDickItem = newDick;
                });

            Del = new CustomCommand<Dick>(
                (s) =>
                {
                    try
                    {
                        new DickSQL().OutputDick(EditDickItem, SelectedDick.Name);
                        SelectedDick.Array.Add(EditDickItem); RaiseEvent(nameof(SelectedDick.Array));
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Before deleting the current item, you need to change all the movies in which it participates \nКакая Досада");
                    }
                    
                    

                }, () => EditDickItem != null);



        }
    }

    class DickView
    {
        public string Name { get; set; }
        public ObservableCollection<Dick> Array { get; set; }
    }

}
