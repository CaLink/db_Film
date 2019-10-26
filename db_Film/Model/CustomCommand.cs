﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace db_Film
{
    public class CustomCommand<T> : ICommand where T : class
    {
        Action<T> action;
        Func<bool> check;

        public CustomCommand(Action<T> action, Func<bool> check = null)
        {
            this.action = action;
            this.check = check;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (check == null)
                return true;
            else
                return check();
        }

        public void Execute(object parameter)
        {
            if (parameter != null)
                action((T)parameter);
            else
                action(null);
        }
    }
}
