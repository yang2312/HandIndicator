using System;
using HandIndicators.Views;
using System.Windows;

namespace HandIndicators
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            MainWindow = new MainWindow();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            // Shutdown the application.
            Application.Current.Shutdown();
        }
    }
}
