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
            //TODO: BUG 5: Favorites not registering.
            //STEPS 1: Try to figure out what's going on!
            //      2: Come on! You can do it!
            //      3: OK, scroll down to the bottom of this file for some hints.
            Container.RegisterType<IFavoritesManager, FavoritesManager>();

        }
        public static IUnityContainer Container { get; set; }

    }
}



















































































// HINT 1: It has to do with dependency injection! 







// HINT 2: OK, I'll tell you this! I suspect that there is more than one FavoritesManager in play here!!!
//         Try to prove or disprove what this hypothesis by putting you caret on the "FavoritesManager" up there, hitting Alt+Shift+D, then choosing
//         "Show All Instances of FavoritesManager"






// HINT 3: The problem has to do with the FavoritesManager's lifetime not being the singleton lifetime, meaning that the 
//         DogsViewModel and the SheltersViewModel each got a different instance! In the Unity Dependency Injection framework, 
//         you use ContainerControlledLifetimeManager (see example above) for specify a component should have singleton lifetime. 
//         Now go and fix it!