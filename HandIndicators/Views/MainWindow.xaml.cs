using HandIndicators.ViewModel;
using System.IO;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace HandIndicators.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Calculate_Clicked(object sender, RoutedEventArgs e)
        {
            ViewModelLocator.Instance.Main.Calculate();
        }

        private void Print_Clicked(object sender, RoutedEventArgs e)
        {
            XpsDocument doc = new XpsDocument("D:/1.xps", FileAccess.ReadWrite);
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);
            writer.Write(MainContent);
            doc.Close();
            
            PdfSharp.Xps.XpsConverter.Convert("D:/1.xps", "D:/Test.pdf", 0);
        }
        
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key== Key.F1)
            {
                Calculate_Clicked(null, new RoutedEventArgs());
            }
            else if (e.Key == Key.F2)
            {
                Print_Clicked(null, new RoutedEventArgs());
            }
        }
    }
}
