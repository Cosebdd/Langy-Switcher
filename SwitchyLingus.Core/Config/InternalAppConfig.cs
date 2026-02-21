using System.Collections.Generic;
using SwitchyLingus.Core.Model;

namespace SwitchyLingus.Core.Config
{
    internal class InternalAppConfig
    {
        public InternalAppConfig(IDictionary<string, LanguageProfile> languageProfiles, string winUserLanguageType)
        {
            LanguageProfiles = languageProfiles;
            WinUserLanguageType = winUserLanguageType;
        }

        public IDictionary<string, LanguageProfile> LanguageProfiles { get; set; }
        public string WinUserLanguageType { get; set; }
    }
}