using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Langy.Core.Extension;
using Langy.UI.Annotations;

namespace Langy.UI
{
    public class ProfileNameDialogViewModel : INotifyPropertyChanged
    {
        private string? _profileName;
        private bool _nameIsValid;
        private readonly IReadOnlyCollection<ContextMenuItem> _profileItems;

        public ProfileNameDialogViewModel(IReadOnlyCollection<ContextMenuItem> profileItems)
        {
            _profileItems = profileItems;
        }

        public string? ProfileName
        {
            get => _profileName;
            set
            {
                _profileName = value;
                NameIsValid = !_profileItems
                    .Select(p => p.Name)
                    .WhereNotNull()
                    .Any(name => name.Equals(_profileName)) && !string.IsNullOrWhiteSpace(_profileName);
                OnPropertyChanged();
            }
        }

        public bool NameIsValid
        {
            get => _nameIsValid;
            set
            {
                _nameIsValid = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}