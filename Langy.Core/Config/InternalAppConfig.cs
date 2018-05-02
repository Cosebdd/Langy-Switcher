using System;
using System.Collections.Generic;
using Langy.Core.Model;

namespace Langy.Core.Config
{
    internal class InternalAppConfig
    {
        public Dictionary<string, LanguageProfile> LanguageProfiles { get; set; }
        public string WinUserLanguageType { get; set; }
    }
}