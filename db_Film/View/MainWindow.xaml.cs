using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace db_Film.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_Add(object sender, RoutedEventArgs e)
        {
            //MainTab.Width = new GridLength(MainTab.MinWidth);
            //AddEditor.Width = new GridLength(AddEditor.MaxWidth);
            SaveButton.Content = "Add";
        }

        private void Open_Edit(object sender, RoutedEventArgs e)
        {
            //MainTab.Width = new GridLength(MainTab.MinWidth);
            //AddEditor.Width = new GridLength(AddEditor.MaxWidth);
            SaveButton.Content = "Edit";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //MainTab.Width =  new GridLength(1, GridUnitType.Star);
            //AddEditor.Width = new GridLength(0);
        }
        
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        
    }
}
