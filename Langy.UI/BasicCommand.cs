using System;
using System.Windows.Input;

namespace Langy.UI
{
    public class BasicCommand(Action action, Func<bool>? canExecute = null) : ICommand
    {
        public bool CanExecute(object? parameter)
        {
            return canExecute == null || canExecute();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Execute(object? parameter)
        {
            action();
        }

        public event EventHandler? CanExecuteChanged;
    }
}