using SS23_SWEN2_TourPlanner_WPF.Log4Net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SS23_SWEN2_TourPlanner_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static ILoggerWrapper logger = LoggerFactory.GetLogger();

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var window = new MainWindow();

            window.Show();
            logger.Warn("Opened Window");
        }
    }
}
