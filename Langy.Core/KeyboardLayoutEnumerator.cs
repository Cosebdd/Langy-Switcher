using System;
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
        private static readonly Lazy<IReadOnlyDictionary<string, KeyboardLayoutInfo>> AllLayouts = new Lazy<IReadOnlyDictionary<string, KeyboardLayoutInfo>>(GetAvailableLayouts);

        public static IReadOnlyDictionary<string, KeyboardLayoutInfo> AvailableLayouts => AllLayouts.Value;

        private static IReadOnlyDictionary<string, KeyboardLayoutInfo> GetAvailableLayouts()
        {
            using var key = Registry.LocalMachine
                .OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Keyboard Layouts");
            if (key == null) throw new Exception("No Keyboard Layouts found in the registry.");

            return key.GetSubKeyNames()
                .Select(klid => GetLayoutInfo(klid, key))
                .WhereNotNull()
                .OrderBy(l => l.DisplayName)
                .ToDictionary(k => k.InputMethodTip);
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
