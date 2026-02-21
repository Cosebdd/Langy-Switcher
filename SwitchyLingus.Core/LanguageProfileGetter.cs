using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Runspaces;
using SwitchyLingus.Core.Model;

namespace SwitchyLingus.Core
{
    public static class LanguageProfileGetter
    {
        internal static LanguageProfile InternalGetCurrentLanguageProfile(string name, out Type winUserLanguageType)
        {
            using var psRunspace = RunspaceFactory.CreateRunspace();
            psRunspace.Open();
            using var psPipeline = psRunspace.CreatePipeline();
            var languageTags = new List<Language>();

            var command = new Command("Get-WinUserLanguageList");

            psPipeline.Commands.Add(command);

            var results = psPipeline.Invoke();
            dynamic member = results.First().BaseObject;

            winUserLanguageType = member[0].GetType();

            foreach (var mem in member)
            {
                languageTags.Add(
                    new Language(mem.LanguageTag, mem.InputMethodTips.ToArray()
                    ));
            }

            var languageProfile = new LanguageProfile(name, languageTags);
            return languageProfile;
        }

        public static LanguageProfile GetCurrentLanguageProfile(string name)
        {
            return InternalGetCurrentLanguageProfile(name, out var _);
        }
    }
}