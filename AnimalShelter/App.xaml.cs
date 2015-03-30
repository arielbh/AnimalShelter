using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AnimalShelter.Services;
using Microsoft.Practices.Unity;

namespace AnimalShelter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Container = new UnityContainer();
            Container.RegisterType<IDataService, DataService>(new ContainerControlledLifetimeManager());
        }
        public static IUnityContainer Container { get; set; }

    }
}
