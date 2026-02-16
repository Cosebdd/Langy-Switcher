using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Langy.Core.Extension;
using Langy.Core.Model;
using Microsoft.Win32;

namespace Langy.Core
{
    public static class KeyboardLayoutEnumerator
    {
        public static List<KeyboardLayoutInfo> GetAvailableLayouts()
        {
            var layouts = new List<KeyboardLayoutInfo>();

            using var key = Registry.LocalMachine
                .OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Keyboard Layouts");
            if (key == null) return layouts;
            
            return key.GetSubKeyNames()
                .Select(klid => GetLayoutInfo(klid, key))
                .WhereNotNull()
                .OrderBy(l => l.DisplayName)
                .ToList();
        }

        private static KeyboardLayoutInfo? GetLayoutInfo(string klid, RegistryKey key)
        {
            using var subKey = key.OpenSubKey(klid);

            var displayName = subKey?.GetValue("Layout Text") as string;
            if (string.IsNullOrEmpty(displayName)) return null;

            if (!int.TryParse(klid.Substring(4), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var langId))
                return null;

            string languageTag;
            try
            {
                languageTag = CultureInfo.GetCultureInfo(langId).Name;
            }
            catch (CultureNotFoundException)
            {
                return null;
            }

            var inputMethodTip = $"{langId:X4}:{klid}";

            return new KeyboardLayoutInfo(klid, displayName!, languageTag, inputMethodTip);
        }
    }
}
