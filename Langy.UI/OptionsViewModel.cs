using System.Collections.ObjectModel;
using System.Windows.Input;
using Langy.Core;
using Langy.Core.Config;

namespace Langy.UI
{
    internal class OptionsViewModel
    {
        private readonly Options _optionsDialog;
        private readonly LanguageProfileItemsManager _itemsManager;

        public OptionsViewModel(LanguageProfileItemsManager itemsManager, Options optionsDialog)
        {
            _itemsManager = itemsManager;
            _optionsDialog = optionsDialog;

            CreateNewProfileCommand = CreateNewProfile();
            RenameProfileCommand = RenameProfile();
            RemoveProfileCommand = RemoveProfile();
        }

        public ContextMenuItem SelectedItem { get; set; }

        public ObservableCollection<ContextMenuItem> ProfileItems => _itemsManager.ProfileItems;

        public ICommand CreateNewProfileCommand { get; }

        public ICommand RenameProfileCommand { get; }

        public ICommand RemoveProfileCommand { get; }

        private ICommand CreateNewProfile()
        {
            return new BasicCommand(() =>
            {
                if (!TryGetProfileNameFromDialog("New profile", "Create new profile", out var profileName)) return;

                var profile = LanguageProfileGetter.GetCurrentLanguageProfile(profileName);
                _itemsManager.CreateLangProfileContextMenuItem(profile);
                AppConfig.CurrentConfig.AddProfile(profile);
            });
        }

        private ICommand RemoveProfile()
        {
            return new BasicCommand(() =>
            {
                AppConfig.CurrentConfig.RemoveProfile(SelectedItem.Name);
                _itemsManager.ProfileItems.Remove(SelectedItem);
            });
        }

        private ICommand RenameProfile()
        {
            return new BasicCommand(() =>
            {
                if (!TryGetProfileNameFromDialog(SelectedItem.Name, "Rename profile", out var profileName)) return;

                AppConfig.CurrentConfig.RenameProfile(SelectedItem.Name, profileName);
                SelectedItem.Name = profileName;
            });
        }

        private bool TryGetProfileNameFromDialog(string defaultText, string dialogTitle, out string profileName)
        {
            var viewModel = new ProfileNameDialogViewModel() { ProfileName = defaultText,  };
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
    }
}