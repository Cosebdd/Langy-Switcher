using System.Collections.Generic;
using Langy.Core.Model;

namespace Langy.Core.Config
{
    internal static class BasicConfigCreator
    {
        public static InternalAppConfig CreateBasicConfig(string name)
        {
            var langProfile = LanguageProfileGetter.InternalGetCurrentLanguageProfile(name, out var type);
            VerifyThat.IsNotNull(type.AssemblyQualifiedName);
            var languageProfiles = new Dictionary<string, LanguageProfile>(){{langProfile.Name, langProfile}};

            return new InternalAppConfig(languageProfiles, type.AssemblyQualifiedName);
        }
    }
}