using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation.Runspaces;
using Langy.Core.Config;
using Langy.Core.Model;

namespace Langy.Core
{
    public static class LanguageProfileSetter
    {
        public static void SetProfile(LanguageProfile profile)
        {
            RunspaceConfiguration psConfig = RunspaceConfiguration.Create();
            var psRunspace = RunspaceFactory.CreateRunspace(psConfig);
            psRunspace.Open();

            using (Pipeline psPipeline = psRunspace.CreatePipeline())
            {
                Command command = new Command("Set-WinUserLanguageList");
                var langType = Type.GetType(AppConfig.CurrentConfig.InternalAppConfig.WinUserLanguageType);
                IList langList = (IList) Activator.CreateInstance(typeof(List<>).MakeGenericType(langType));

                foreach (var language in profile.Languages)
                {
                    dynamic resultLang = Activator.CreateInstance(langType, language.Tag);
                    resultLang.InputMethodTips.Clear();
                    resultLang.InputMethodTips.AddRange(language.InputMethods);
                    langList.Add(resultLang);
                }

                psPipeline.Commands.Add(command);
                command.Parameters.Add("LanguageList", langList);
                command.Parameters.Add("Force", true);

                psPipeline.Invoke();
            }
        }
    }
}