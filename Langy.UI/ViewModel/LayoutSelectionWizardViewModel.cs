using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Langy.Core;
using Langy.Core.Extension;
using Langy.Core.Model;
using Langy.UI.Annotations;

namespace Langy.UI.ViewModel
{
    internal class LayoutSelectionWizardViewModel : INotifyPropertyChanged
    {
        private readonly IReadOnlyCollection<ContextMenuItem> _existingProfiles;
        private readonly IReadOnlyCollection<KeyboardLayoutInfo> _allLayouts;
        private string _profileName = string.Empty;
        private string _searchText = string.Empty;
        private bool _canSave;
        private KeyboardLayoutInfo _selectedAvailableLayout;
        private KeyboardLayoutInfo _selectedChosenLayout;

        public LayoutSelectionWizardViewModel(IReadOnlyCollection<ContextMenuItem> existingProfiles)
        {
            _existingProfiles = existingProfiles;
            _allLayouts = KeyboardLayoutEnumerator.AvailableLayouts;

            AvailableLayouts = new ObservableCollection<KeyboardLayoutInfo>(_allLayouts);
            SelectedLayouts = new ObservableCollection<KeyboardLayoutInfo>();

            AddSelectedLayoutsCommand = new BasicCommand(AddSelectedLayout, () => _selectedAvailableLayout != null);
            RemoveSelectedLayoutsCommand = new BasicCommand(RemoveSelectedLayout, () => _selectedChosenLayout != null);
        }

        public string ProfileName
        {
            get => _profileName;
            set
            {
                _profileName = value;
                OnPropertyChanged();
                UpdateCanSave();
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                FilterAvailableLayouts();
            }
        }

        public ObservableCollection<KeyboardLayoutInfo> AvailableLayouts { get; }

        public ObservableCollection<KeyboardLayoutInfo> SelectedLayouts { get; }

        public KeyboardLayoutInfo SelectedAvailableLayout
        {
            get => _selectedAvailableLayout;
            set
            {
                _selectedAvailableLayout = value;
                OnPropertyChanged();
                AddSelectedLayoutsCommand.RaiseCanExecuteChanged();
            }
        }

        public KeyboardLayoutInfo SelectedChosenLayout
        {
            get => _selectedChosenLayout;
            set
            {
                _selectedChosenLayout = value;
                OnPropertyChanged();
                RemoveSelectedLayoutsCommand.RaiseCanExecuteChanged();
            }
        }

        public bool CanSave
        {
            get => _canSave;
            private set
            {
                _canSave = value;
                OnPropertyChanged();
            }
        }

        public BasicCommand AddSelectedLayoutsCommand { get; }

        public BasicCommand RemoveSelectedLayoutsCommand { get; }

        private void AddSelectedLayout()
        {
            if (_selectedAvailableLayout == null) return;

            var layout = _selectedAvailableLayout;
            AvailableLayouts.Remove(layout);
            SelectedLayouts.Add(layout);
            UpdateCanSave();
        }

        private void RemoveSelectedLayout()
        {
            if (_selectedChosenLayout == null) return;

            var layout = _selectedChosenLayout;
            SelectedLayouts.Remove(layout);
            FilterAvailableLayouts();

            UpdateCanSave();
        }

        private void FilterAvailableLayouts()
        {
            AvailableLayouts.Clear();
            var selectedKlids = new HashSet<string>(SelectedLayouts.Select(l => l.Klid));

            AvailableLayouts
                .AddRange(_allLayouts
                    .Where(layout => !selectedKlids.Contains(layout.Klid) && MatchesFilter(layout)));
        }

        private bool MatchesFilter(KeyboardLayoutInfo layout)
        {
            if (string.IsNullOrWhiteSpace(_searchText)) return true;

            var search = _searchText.ToLowerInvariant();
            return layout.DisplayName.ToLowerInvariant().Contains(search) ||
                   layout.LanguageTag.ToLowerInvariant().Contains(search);
        }

        private void UpdateCanSave()
        {
            CanSave = !string.IsNullOrWhiteSpace(_profileName) &&
                      !_existingProfiles.Any(p => p.Name.Equals(_profileName)) &&
                      SelectedLayouts.Count > 0;
        }

        public LanguageProfile BuildProfile()
        {
            var languages = SelectedLayouts
                .GroupBy(l => l.LanguageTag)
                .Select(g => 
                    new Language(g.Key, g
                        .Select(l => l.InputMethodTip)
                        .ToArray())
                );

            return new LanguageProfile(ProfileName, languages.ToList());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
