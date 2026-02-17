using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Langy.Core.Config;
using Langy.UI.Annotations;
using Langy.UI.ViewModel;

namespace Langy.UI
{
    internal class OptionsViewModel : INotifyPropertyChanged
    {
        private readonly Options _optionsDialog;
        private readonly LanguageProfileItemsManager _itemsManager;
        private ContextMenuItem _selectedItem;

        public OptionsViewModel(LanguageProfileItemsManager itemsManager, Options optionsDialog)
        {
            _itemsManager = itemsManager;
            _optionsDialog = optionsDialog;

            CreateNewProfileCommand = CreateNewProfile();
            RenameProfileCommand = RenameProfile();
            RemoveProfileCommand = RemoveProfile();
        }

        public ContextMenuItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                RenameProfileCommand.RaiseCanExecuteChanged();
                RemoveProfileCommand.RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ContextMenuItem> ProfileItems => _itemsManager.ProfileItems;

        public ICommand CreateNewProfileCommand { get; }

        public BasicCommand RenameProfileCommand { get; }

        public BasicCommand RemoveProfileCommand { get; }

        private BasicCommand CreateNewProfile()
        {
            return new BasicCommand(() =>
            {
                var vm = new LayoutSelectionWizardViewModel(ProfileItems);
                var wizard = new LayoutSelectionWizard
                {
                    DataContext = vm,
                    Owner = _optionsDialog,
                    Title = "Create New Profile"
                };
                if (wizard.ShowDialog() != true) return;

                var profile = vm.BuildProfile();
                _itemsManager.CreateLangProfileContextMenuItem(profile);
                AppConfig.CurrentConfig.AddProfile(profile);
            });
        }

        private BasicCommand RemoveProfile()
        {
            return new BasicCommand(() =>
            {
                AppConfig.CurrentConfig.RemoveProfile(SelectedItem.Name);
                _itemsManager.ProfileItems.Remove(SelectedItem);
            }, 
            () => SelectedItem != null
            );
        }

        private BasicCommand RenameProfile()
        {
            return new BasicCommand(() =>
            {
                if (!TryGetProfileNameFromDialog(SelectedItem.Name, "Rename profile", out var profileName)) return;

                AppConfig.CurrentConfig.RenameProfile(SelectedItem.Name, profileName);
                SelectedItem.Name = profileName;
            },
            () => SelectedItem != null);
        }

        private bool TryGetProfileNameFromDialog(string defaultText, string dialogTitle, out string profileName)
        {
            var viewModel = new ProfileNameDialogViewModel(ProfileItems) { ProfileName = defaultText,  };
            var dialog = new ProfileNameDialog
            {
                DataContext = viewModel,
                Owner = _optionsDialog,
                Title = dialogTitle
            };
            var result = dialog.ShowDialog();
            profileName = viewModel.ProfileName;
            return result ?? false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}