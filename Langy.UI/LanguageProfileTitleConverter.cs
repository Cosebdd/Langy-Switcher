using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using Langy.Core.Config;

namespace Langy.UI
{
    public class LanguageProfileTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string profileName))
                return null;
            var profile = AppConfig.CurrentConfig.LanguageProfiles.Single(p => p.Name == profileName);
            return $"{profile.Name} ({profile.LanguageTags.Aggregate((a, b) =>  a + "," + b)})";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}