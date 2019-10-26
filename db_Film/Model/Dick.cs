using db_Film.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace db_Film.Model
{
    public class Dick : NotifyModel
    {
        private int id { get; set; }
        private string name { get; set; }

        public int ID {
            get => id;
            set { id = value; RaiseEvent(nameof(ID)); }
        }
        public string Name {
            get => name;
            set { name = value; RaiseEvent(nameof(Name)); }
        }

    }
}
