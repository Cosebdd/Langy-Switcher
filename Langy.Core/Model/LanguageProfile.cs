using System.Collections.Generic;

namespace Langy.Core.Model
{
    public class LanguageProfile
    {
        public string Name { get; set; }
        public IEnumerable<string> LanguageTags { get; set; } 
    }
}