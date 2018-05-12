using System;
using System.Windows.Input;

namespace Langy.UI
{
    public class BasicCommand : ICommand
    {
        private readonly Action _action;

        public BasicCommand(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public event EventHandler CanExecuteChanged;
    }
}