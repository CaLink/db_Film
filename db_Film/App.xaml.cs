using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using db_Film.View;

namespace db_Film
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Manager.AddWindowOpen(new Option());
        }
    }


    static class Manager
    {
        static List<Window> openedWindows = new List<Window>();

        public static void AddWindowOpen(Window win)
        {
            win.Closing += Win_Closing;
            openedWindows.Add(win);
            win.Show();
        }

        public static void AddWindowDialog(Window win)
        {
            win.Closing += Win_Closing;
            openedWindows.Add(win);
            win.ShowDialog();
        }

        private static void Win_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ((Window)sender).Closing -= Win_Closing;
            openedWindows.Remove((Window)sender);
            TestToExit();
        }

        internal static void Close(Type type)
        {
            var list = openedWindows.FindAll(s => s.GetType() == type);
            list.ForEach(s => s.Close());
        }

        private static void TestToExit()
        {
            if (openedWindows.Count == 0)
                App.Current.Shutdown();
        }
    }
}
