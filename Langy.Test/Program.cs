using System;
using Langy.Core.Config;

namespace Langy.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            AppConfig config = AppConfig.CurrentConfig;
            foreach (var languageProfile in config.LanguageProfiles.Values)
            {
                Console.WriteLine(languageProfile.Name);
                foreach (var tag in languageProfile.LanguageTags)
                {
                    Console.WriteLine(tag);
                }
            }
        }
    }
}
