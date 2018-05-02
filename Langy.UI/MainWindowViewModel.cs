using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Langy.Core;
using Langy.Core.Config;
using Langy.Core.Model;

namespace Langy.UI
{
    public class MainWindowViewModel
    {
        private readonly AppConfig _config = AppConfig.CurrentConfig;

        public MainWindowViewModel()
        {
            ProfileItems = new ObservableCollection<ContextMenuItem>();
            CreateProfileMenuItems();

            NewProfileCommand = CreateNewProfileCommand();
        }

        public ObservableCollection<ContextMenuItem> ProfileItems { get; set; }

        private static ICommand SetProfileCommand(LanguageProfile languageProfile)
        {
            return new ContextMenuCommand(() => { LanguageProfileSetter.SetProfile(languageProfile); }
            );
        }

        public ICommand NewProfileCommand { get; }

        private ICommand CreateNewProfileCommand()
        {
            return new ContextMenuCommand(() =>
            {
                var profile = LanguageProfileGetter.GetCurrentLanguageProfile("Stub Name");
                CreateLangProfileContextMenuItem(profile);
                AppConfig.CurrentConfig.AddProfile(profile);
            });
        }

        private class ContextMenuCommand : ICommand
        {
            private readonly Action _action;

            public ContextMenuCommand(Action action)
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

        public class ContextMenuItem
        {
            public string Name { get; set; }
            public ICommand ItemCommand { get; set; }
        }

        private void CreateLangProfileContextMenuItem(LanguageProfile profile)
        {
            var item = new ContextMenuItem()
            {
                ItemCommand = SetProfileCommand(profile),
                Name = profile.Name
            };
            ProfileItems.Add(item);
        }

        private void CreateProfileMenuItems()
        {
            foreach (var languageProfilesValue in _config.LanguageProfiles.Values)
            {
                CreateLangProfileContextMenuItem(languageProfilesValue);
            }
        }
    }
}