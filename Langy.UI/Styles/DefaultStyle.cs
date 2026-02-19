using System.Windows;

namespace Langy.UI.Styles;

internal static class LocalExtensions
{
    public static void ForWindowFromTemplate(this object templateFrameworkElement, Action<Window> action)
    {
        if (((FrameworkElement)templateFrameworkElement).TemplatedParent is Window window) action(window);
    }
}

public partial class DefaultStyle
{
    void CloseButtonClick(object sender, RoutedEventArgs e)
    {
        sender.ForWindowFromTemplate(SystemCommands.CloseWindow);
    }
}