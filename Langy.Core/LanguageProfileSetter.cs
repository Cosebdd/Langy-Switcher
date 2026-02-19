using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Runspaces;
using Langy.Core.Config;
using Langy.Core.Model;

namespace Langy.Core
{
    public static class LanguageProfileSetter
    {
        public static void SetProfile(LanguageProfile profile)
        {
            using var psRunspace = RunspaceFactory.CreateRunspace();
            psRunspace.Open();
            using var psPipeline = psRunspace.CreatePipeline();

            var command = new Command("Set-WinUserLanguageList");

            var langList = GetLanguageList(profile);

            psPipeline.Commands.Add(command);
            command.Parameters.Add("LanguageList", langList);
            command.Parameters.Add("Force", true);

            psPipeline.Invoke();
        }

        private static IList GetLanguageList(LanguageProfile profile)
        {
            var langType = Type.GetType(AppConfig.CurrentConfig.InternalAppConfig.WinUserLanguageType);

            return langType == null 
                ? throw new ArgumentNullException(AppConfig.CurrentConfig.InternalAppConfig.WinUserLanguageType) 
                : profile.Languages.Select(CreateLanguage).ToList();

            dynamic CreateLanguage(Language language)
            {
                dynamic resultLang = Activator.CreateInstance(langType, language.Tag);
                resultLang.InputMethodTips.Clear();
                resultLang.InputMethodTips.AddRange(language.InputMethods);
                return resultLang;
            }
        }
    }
}