using System.Windows.Input;

namespace Langy.UI
{
    public class ContextMenuItem
    {
        public string Text { get; set; }
        public ICommand Command { get; set; }
    }
}