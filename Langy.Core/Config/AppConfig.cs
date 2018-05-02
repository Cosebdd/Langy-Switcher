using System;
using System.Collections.Generic;
using System.IO;
using Langy.Core.Model;
using Newtonsoft.Json;

namespace Langy.Core.Config
{
    public class AppConfig
    {
        private const string ConfigPath = @".\config.json";

        public IReadOnlyDictionary<string, LanguageProfile> LanguageProfiles => InternalAppConfig.LanguageProfiles;

        public static AppConfig CurrentConfig { get; } = new AppConfig();

        internal InternalAppConfig InternalAppConfig { get; }

        private AppConfig()
        {
            InternalAppConfig = new InternalAppConfig();
            try
            {
                var jsonConfig = File.ReadAllText(ConfigPath);
                InternalAppConfig = JsonConvert.DeserializeObject<InternalAppConfig>(jsonConfig);
                
                if (InternalAppConfig.LanguageProfiles == null 
                    || InternalAppConfig.LanguageProfiles.Count == 0 
                    || Type.GetType(InternalAppConfig.WinUserLanguageType) == null)
                    throw new Exception();
            }
            catch
            {
                var basicConfig = BasicConfigCreator.CreateBasicConfig("Main Profile");
                InternalAppConfig = basicConfig;
                SaveConfig();
            }

        }

        public void AddProfile(LanguageProfile profile)
        {
            if (InternalAppConfig.LanguageProfiles.ContainsKey(profile.Name))
                throw new Exception("Profile with given name already exists");
            InternalAppConfig.LanguageProfiles.Add(profile.Name, profile);
            SaveConfig();
        }

        public void RemoveProfile(string profileName)
        {
            InternalAppConfig.LanguageProfiles.Remove(profileName);
            SaveConfig();
        }

        private void SaveConfig()
        {
            var jsonConfig = JsonConvert.SerializeObject(InternalAppConfig);
            File.WriteAllText(ConfigPath, jsonConfig);
        }
    }

}
