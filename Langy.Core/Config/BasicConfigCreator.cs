using System.Collections.Generic;
using Langy.Core.Model;

namespace Langy.Core.Config
{
    internal static class BasicConfigCreator
    {
        public static InternalAppConfig CreateBasicConfig(string name)
        {
            var langProfile = LanguageProfileGetter.InternalGetCurrentLanguageProfile(name, out var type);

            var languageProfiles = new Dictionary<string, LanguageProfile>();
            languageProfiles.Add(name, langProfile);

            return new InternalAppConfig()
            {
                LanguageProfiles = languageProfiles,
                WinUserLanguageType = type.AssemblyQualifiedName
            };
        }
    }
}