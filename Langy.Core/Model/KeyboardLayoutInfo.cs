namespace Langy.Core.Model
{
    public class KeyboardLayoutInfo
    {
        public KeyboardLayoutInfo(string klid, string displayName, string languageTag, string inputMethodTip)
        {
            Klid = klid;
            DisplayName = displayName;
            LanguageTag = languageTag;
            InputMethodTip = inputMethodTip;
        }

        public string Klid { get; set; }
        public string DisplayName { get; set; }
        public string LanguageTag { get; set; }
        public string InputMethodTip { get; set; }
    }
}
