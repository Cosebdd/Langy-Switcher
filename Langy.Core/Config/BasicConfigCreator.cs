using System.Collections.Generic;
using Langy.Core.Model;

namespace Langy.Core.Config
{
    internal static class BasicConfigCreator
    {
        public static InternalAppConfig CreateBasicConfig(string name)
        {
            var langProfile = LanguageProfileGetter.InternalGetCurrentLanguageProfile(name, out var type);

            var languageProfiles = new List<LanguageProfile> {langProfile};

            return new InternalAppConfig(languageProfiles, type.AssemblyQualifiedName);
        }
    }
}