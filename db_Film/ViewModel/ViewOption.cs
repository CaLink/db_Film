using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using db_Film.Model;

namespace db_Film.ViewModel
{
    public class ViewOption : NotifyModel
    {
        public CustomCommand<Poopy> TestConnection { get; set; }
        public CustomCommand<Poopy> SaveConnection { get; set; }


        public ViewOption()
        {
            TestConnection = new CustomCommand<Poopy>(
                (s) => 
                {
                    MySQL.SetUpMySqlConnection();
                    if (MySQL.TestConnection())
                        System.Windows.MessageBox.Show("Все работает.Какая досада");
                });

            SaveConnection = new CustomCommand<Poopy>(
                (s) =>
                {
                    MySQL.SetUpMySqlConnection();
                    Properties.Settings.Default.Save();
                    
                });

        }

    }
}
