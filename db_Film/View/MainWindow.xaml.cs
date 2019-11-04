using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            listSerial.Items.IsLiveSorting = true;
            listFilms.Items.IsLiveSorting = true;
        }

        private void Open_Add(object sender, RoutedEventArgs e)
        {
            //MainTab.Width = new GridLength(MainTab.MinWidth);
            //AddEditor.Width = new GridLength(AddEditor.MaxWidth);
            SaveButton.Content = "Add";
        }

        public void Open_Edit(object sender, RoutedEventArgs e)
        {
            //MainTab.Width = new GridLength(MainTab.MinWidth);
            //AddEditor.Width = new GridLength(AddEditor.MaxWidth);
            SaveButton.Content = "Save";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //MainTab.Width =  new GridLength(1, GridUnitType.Star);
            //AddEditor.Width = new GridLength(0);
        }
        
        

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            fName.Text = null;
            fProducer.SelectedIndex = -1;
            fYear.Text = null;
            fCountry.SelectedIndex = -1;
            fAgeRate.SelectedIndex = -1;
            fGenre.SelectedIndex = -1;
            fScore.SelectedIndex = -1;
        }

        private void Sort(object sender, RoutedEventArgs e)
        {
            string header = ((GridViewColumnHeader)e.OriginalSource).Content.ToString();
            ListView view = (ListView)sender;
            ((ViewModel.ViewMainWindow)gridContext.DataContext).RaiseSort(header, view.ItemsSource);
           
            /*view.Items.SortDescriptions.Clear();
            if (header == "Name" || header == "Year" || header == "Score")
                view.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(header.ToString(), System.ComponentModel.ListSortDirection.Descending));
            else
                view.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription(header + ".Name", System.ComponentModel.ListSortDirection.Descending));
            */

        }

        private void SectorClear(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).CommandParameter)
            {
                case "Name": fName.Text = null; break;
                case "Producer": fProducer.SelectedIndex = -1;break;
                case "Year": fYear.Text = null; ; break;
                case "Country": fCountry.SelectedIndex = -1; ; break;
                case "AgeRate": fAgeRate.SelectedIndex = -1; ; break;
                case "Genre": fGenre.SelectedIndex = -1; ; break;
                case "Score": fScore.SelectedIndex = -1; ; break;
            }
        }
    }
}
