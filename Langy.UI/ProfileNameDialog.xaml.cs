using System.Windows;

namespace Langy.UI
{
    /// <summary>
    /// Interaction logic for ProfileNameDialog.xaml
    /// </summary>
    public partial class ProfileNameDialog : Window
    {
        public ProfileNameDialog()
        {
            InitializeComponent();
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
