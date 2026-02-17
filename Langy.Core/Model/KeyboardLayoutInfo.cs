using System;

namespace Langy.Core.Model
{
    public class KeyboardLayoutInfo : IEquatable<KeyboardLayoutInfo>
    {
        public KeyboardLayoutInfo(string klid, string displayName, string languageTag, string inputMethodTip)
        {
            Klid = klid;
            DisplayName = displayName;
            LanguageTag = languageTag;
            InputMethodTip = inputMethodTip;
        }

        public string Klid { get; }
        public string DisplayName { get; }
        public string LanguageTag { get; }
        public string InputMethodTip { get; }

        public bool Equals(KeyboardLayoutInfo? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return InputMethodTip == other.InputMethodTip;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((KeyboardLayoutInfo)obj);
        }

        public override int GetHashCode()
        {
            return InputMethodTip.GetHashCode();
        }
    }
}
