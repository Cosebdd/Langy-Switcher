using System.Collections.Generic;
using Langy.Core.Model;

namespace Langy.Core.Config
{
    internal class InternalAppConfig
    {
        public List <LanguageProfile> LanguageProfiles { get; set; }
        public string WinUserLanguageType { get; set; }
    }
}