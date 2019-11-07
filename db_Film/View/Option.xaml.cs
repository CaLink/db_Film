using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Markup;
using System.Threading;

namespace db_Film.View
{
    /// <summary>
    /// Логика взаимодействия для Option.xaml
    /// </summary>
    public partial class Option : Window
    {
        public Option()
        {
            InitializeComponent();
        }
    }

    public class SqlProperties : MarkupExtension
    {
        public string Server { get => Properties.Settings.Default.server; 
                               set { Properties.Settings.Default.server = value; } }

        public string DB { get => Properties.Settings.Default.db;
                           set { Properties.Settings.Default.db = value; }}

        public string User { get => Properties.Settings.Default.user; 
                             set { Properties.Settings.Default.user = value;} }

        public string Password { get => Properties.Settings.Default.password; 
                                 set { Properties.Settings.Default.password = value;} }
               
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new SqlProperties();
        }
    }
}
