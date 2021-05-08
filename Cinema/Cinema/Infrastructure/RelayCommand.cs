using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cinema.Infrastructure
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> action;
        private readonly Predicate<object> predicate;

        public RelayCommand(Action<object> action, Predicate<object> predicate = null)
        {
            this.action = action;
            this.predicate = predicate;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return predicate == null ? true : predicate(parameter);
        }

        public void Execute(object parameter)
        {
            action(parameter);
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> predicate;
        private readonly Action<T> action;

        public RelayCommand(Action<T> action, Predicate<T> predicate = null)
        {
            this.predicate = predicate;
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return predicate == null ? true : predicate((T)parameter);
        }

        public void Execute(object parameter)
        {
            action((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add 
            { 
                CommandManager.RequerySuggested += value; 
            }
            remove 
            { 
                CommandManager.RequerySuggested -= value; 
            }
        }
    }
}
