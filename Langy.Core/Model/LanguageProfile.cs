using System.Collections.Generic;

namespace Langy.Core.Model
{
    public class LanguageProfile
    {
        public string Name { get; set; }
        public IEnumerable<Language> Languages { get; set; } 
    }
}