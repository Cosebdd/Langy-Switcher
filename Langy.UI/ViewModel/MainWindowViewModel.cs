using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Langy.Core.Config;

namespace Langy.UI
{
    public class MainWindowViewModel
    {
        private object lockObject = new object();

        private readonly AppConfig _config = AppConfig.CurrentConfig;
        private readonly LanguageProfileItemsManager _itemsManager;
        private Options _openedOptionsDialog;

        public MainWindowViewModel()
        {
            _itemsManager = new LanguageProfileItemsManager();
            CreateProfileMenuItems();
            OptionsCommand = ShowOptionsDialogCommand();
            ExitCommand = ExitApplicationCommand();
            _openedOptionsDialog = null;
        }

        private ICommand ExitApplicationCommand()
        {
            return new BasicCommand(() => { Application.Current.Shutdown(); });
        }

        public ObservableCollection<ContextMenuItem> ProfileItems => _itemsManager.ProfileItems;

        public ICommand OptionsCommand { get; }
        public ICommand ExitCommand { get; }

        private ICommand ShowOptionsDialogCommand()
        {
            return new BasicCommand(() =>
            {
                lock (lockObject)
                {
                    if (_openedOptionsDialog == null)
                    {
                        _openedOptionsDialog = new Options();
                        _openedOptionsDialog.DataContext = new OptionsViewModel(_itemsManager, _openedOptionsDialog);
                        _openedOptionsDialog.Closed += (sender, args) => { _openedOptionsDialog = null; };
                        _openedOptionsDialog.Show();
                    }
                    else
                    {
                        _openedOptionsDialog.Activate();
                    }
                }
            });
        }

        private void CreateProfileMenuItems()
        {
            foreach (var languageProfilesValue in _config.LanguageProfiles)
            {
                _itemsManager.CreateLangProfileContextMenuItem(languageProfilesValue);
            }
        }
    }
}