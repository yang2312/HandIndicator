using HandIndicators.ViewModel;

using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace HandIndicators.Views
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.Instance.Main;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
