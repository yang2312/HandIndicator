using HandIndicators.ViewModel;
using System;
using System.IO;
using System.Windows;
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
            if (ViewModelLocator.Instance.Main.IsDataValidated())
            {
                ViewModelLocator.Instance.Main.Calculate();
            }
            else
            {
                MessageBox.Show("Lưu ý các chỉ số phải > 0 + Năm sinh không được để trống + Loại phải bắt đầu bằng W/U/A", "Alert", MessageBoxButton.OK);
            }
        }

        private void Print_Clicked(object sender, RoutedEventArgs e)
        {
            string root = "D:/HandIndicator";
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            string pathXPS = $"{root}/{ViewModelLocator.Instance.Main.Name}_{DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_ss")}.xps";
            string pathPDF = $"{root}/{ViewModelLocator.Instance.Main.Name}_{DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_ss")}.pdf";

            XpsDocument doc = new XpsDocument(pathXPS, FileAccess.ReadWrite);
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);
            writer.Write(MainContent);
            doc.Close();

            PdfSharp.Xps.XpsConverter.Convert(pathXPS, pathPDF, 0);

            MessageBox.Show($"In thành công. Xem file tại {pathPDF}", "Alert", MessageBoxButton.OK);
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
