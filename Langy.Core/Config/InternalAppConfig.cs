using System.Collections.Generic;
using Langy.Core.Model;

namespace Langy.Core.Config
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