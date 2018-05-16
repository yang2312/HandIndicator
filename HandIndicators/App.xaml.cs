using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
            try
            {
                MainWindow = new MainWindow();
            }
            catch(Exception c)
            {

            }
            
        }
    }
}
