using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Langy.UI
{
    public sealed class ContextMenuItem : INotifyPropertyChanged
    {
        private string? _name;

        public string? Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public ICommand? ItemCommand { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}