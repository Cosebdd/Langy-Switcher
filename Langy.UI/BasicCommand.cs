using System;
using System.Windows.Input;

namespace Langy.UI
{
    public class BasicCommand : ICommand
    {
        private readonly Action _action;
        private readonly Func<bool> _canExecute;

        public BasicCommand(Action action, Func<bool> canExecute = null)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public event EventHandler CanExecuteChanged;
    }
}