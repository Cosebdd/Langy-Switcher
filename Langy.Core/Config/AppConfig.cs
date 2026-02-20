using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Langy.Core.Model;
using Newtonsoft.Json;

namespace Langy.Core.Config
{
    public class AppConfig
    {
        private const string ConfigPath = @".\config.json";

        public IDictionary<string, LanguageProfile> LanguageProfiles => InternalAppConfig.LanguageProfiles;

        public static AppConfig CurrentConfig { get; } = new AppConfig();

        internal InternalAppConfig InternalAppConfig { get; }

        private AppConfig()
        {
            try
            {
                var jsonConfig = File.ReadAllText(ConfigPath);
                InternalAppConfig = JsonConvert.DeserializeObject<InternalAppConfig>(jsonConfig) ??
                                    throw new Exception("Unable to deserialize config file.");

                if (InternalAppConfig.LanguageProfiles == null
                    || InternalAppConfig.LanguageProfiles.Count == 0
                    || string.IsNullOrEmpty(InternalAppConfig.WinUserLanguageType))
                    throw new Exception("Invalid config file");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine("Recreating basic config file");
                var basicConfig = BasicConfigCreator.CreateBasicConfig("Main Profile");
                InternalAppConfig = basicConfig;
                SaveConfig();
            }
        }

        public void AddProfile(LanguageProfile profile)
        {
            InternalAppConfig.LanguageProfiles.Add(profile.Name, profile);
            SaveConfig();
        }

        public void RemoveProfile(string profileName)
        {
            if (!InternalAppConfig.LanguageProfiles.Remove(profileName))
            {
                Debug.WriteLine($"Profile {profileName} not found and can't be removed");
                return;
            }

            SaveConfig();
        }

        public void UpdateProfile(string oldName, LanguageProfile updatedProfile)
        {
            if (oldName != updatedProfile.Name)
                InternalAppConfig.LanguageProfiles.Remove(oldName);

            InternalAppConfig.LanguageProfiles[updatedProfile.Name] = updatedProfile;
            SaveConfig();
        }

        private void SaveConfig()
        {
            var jsonConfig = JsonConvert.SerializeObject(InternalAppConfig);
            File.WriteAllText(ConfigPath, jsonConfig);
        }
    }
}