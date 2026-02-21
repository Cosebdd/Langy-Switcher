using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using SwitchyLingus.Core;
using SwitchyLingus.Core.Model;

namespace SwitchyLingus.UI
{
    internal class LanguageProfileItemsManager
    {
        public ObservableCollection<ContextMenuItem> ProfileItems { get; set; }

        public LanguageProfileItemsManager()
        {
            ProfileItems = new ObservableCollection<ContextMenuItem>();
        }

        public void CreateLangProfileContextMenuItem(LanguageProfile profile)
        {
            var item = new ContextMenuItem()
            {
                ItemCommand = SetProfileCommand(profile),
                Name = profile.Name
            };
            ProfileItems.Add(item);
        }

        public void UpdateLangProfileContextMenuItem(ContextMenuItem item, LanguageProfile profile)
        {
            item.Name = profile.Name;
            item.ItemCommand = SetProfileCommand(profile);
        }

        private static ICommand SetProfileCommand(LanguageProfile languageProfile)
        {
            return new BasicCommand(() =>
                {
                    var task = new Task(() =>
                    {
                        try
                        {
                            LanguageProfileSetter.SetProfile(languageProfile);
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e);
                        }
                    });
                    task.Start();
                }
            );
        }
    }
}