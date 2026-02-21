using System.Windows;
using System.Windows.Input;

namespace SwitchyLingus.UI
{
    public partial class LayoutSelectionWizard : Window
    {
        public LayoutSelectionWizard()
        {
            InitializeComponent();
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void AvailableLayouts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var vm = DataContext as ViewModel.LayoutSelectionWizardViewModel;
            if (vm?.AddSelectedLayoutsCommand.CanExecute(null) == true)
                vm.AddSelectedLayoutsCommand.Execute(null);
        }

        private void SelectedLayouts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var vm = DataContext as ViewModel.LayoutSelectionWizardViewModel;
            if (vm?.RemoveSelectedLayoutsCommand.CanExecute(null) == true)
                vm.RemoveSelectedLayoutsCommand.Execute(null);
        }
    }
}
