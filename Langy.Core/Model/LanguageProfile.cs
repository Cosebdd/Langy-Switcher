using System.Collections.Generic;

namespace Langy.Core.Model
{
    public class LanguageProfile
    {
        public LanguageProfile(string name, IEnumerable<Language> languages)
        {
            Name = name;
            Languages = languages;
        }

        public string Name { get; set; }
        public IEnumerable<Language> Languages { get; set; } 
    }
}