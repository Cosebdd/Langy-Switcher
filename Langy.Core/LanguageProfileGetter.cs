using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Runspaces;
using Langy.Core.Model;

namespace Langy.Core
{
    public static class LanguageProfileGetter
    {
        internal static LanguageProfile InternalGetCurrentLanguageProfile(string name, out Type winUserLanguageType)
        {
            RunspaceConfiguration psConfig = RunspaceConfiguration.Create();
            var psRunspace = RunspaceFactory.CreateRunspace(psConfig);
            psRunspace.Open();
            List<string> languageTags = new List<string>();

            using (Pipeline psPipeline = psRunspace.CreatePipeline())
            {
                Command command = new Command("Get-WinUserLanguageList");

                psPipeline.Commands.Add(command);

                var results = psPipeline.Invoke();
                dynamic member = results.First().BaseObject;

                winUserLanguageType = member[0].GetType();

                foreach (var mem in member)
                {
                    languageTags.Add(mem.LanguageTag);
                }

            }

            var languageProfile = new LanguageProfile() { LanguageTags = languageTags, Name = name };
            return languageProfile;
        }

        public static LanguageProfile GetCurrentLanguageProfile(string name)
        {
            return InternalGetCurrentLanguageProfile(name, out var _ );
        }
    }
}