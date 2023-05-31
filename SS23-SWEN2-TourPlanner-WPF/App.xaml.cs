using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using log4net;
using log4net.Repository.Hierarchy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SS23_SWEN2_TourPlanner_WPF.BL;
using SS23_SWEN2_TourPlanner_WPF.DAL;
using SS23_SWEN2_TourPlanner_WPF.Log4Net;
using SS23_SWEN2_TourPlanner_WPF.ViewModels;

namespace SS23_SWEN2_TourPlanner_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();

            // Allow Local Timestamps in NpgSQL
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            this.InitializeComponent();
        }

        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // create all layers
            services.AddSingleton<IDataManager, DataManagerEFImpl>();
            services.AddSingleton<IToursManager, ToursManagerImpl>();

            // create viewmodels
            services.AddSingleton<ToursViewModel>();

            services.AddTransient<AddTourViewModel>();
            services.AddTransient<AddTourLogViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
