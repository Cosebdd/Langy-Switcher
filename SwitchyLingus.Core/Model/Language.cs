namespace SwitchyLingus.Core.Model
{
    public class Language
    {
        public Language(string tag, string[] inputMethods)
        {
            Tag = tag;
            InputMethods = inputMethods;
        }

        public string Tag { get; set; }
        public string[] InputMethods { get; set; } 
    }
}