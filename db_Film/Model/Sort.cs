using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace db_Film.Model
{
   /* class Sort<T> : IComparer
    {
        

        string ColumnName;
        public Sort()
        {
            this.ColumnName = ColumnName;
        }

        public int Compare(object a, object b)
        {
            Film a1 = a as Film;
            Film b1 = b as Film;
            if (a1 == null || b1 == null)
                return 0;

            var prop = typeof(Film).GetProperty(ColumnName);

            return StrCmpLogicalW(prop.GetValue(a1).ToString(), prop.GetValue(b1).ToString());
        }

       
    }/*
    { ААААААА АШИБКА СТОП НОЛЬ НОЛЬ НОЛЬ НОЛЬ} */
}
