using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AnimalShelter.Model;
using AnimalShelter.Services;
using Microsoft.Practices.Unity;
using SuiteValue.UI.MVVM;

namespace AnimalShelter.ViewModels
{
    class SheltersViewModel : HeaderViewModel
    {
        public SheltersViewModel()
        {
            Header = "Shelters";
        }

        public override async void Activate()
        {
            base.Activate();
            Shelters = await DataService.GetShelters(await DataService.GetDogs());
        }

        private Shelter[] _shelters;

        public Shelter[] Shelters
        {
            get { return _shelters; }
            set
            {
                if (value != _shelters)
                {
                    _shelters = value;
                    OnPropertyChanged(() => Shelters);
                }
            }
        }

        private DelegateCommand<Shelter> _feedCommand;

        public DelegateCommand<Shelter> FeedCommand
        {
            get
            {
                return _feedCommand ?? (_feedCommand = new DelegateCommand<Shelter>(
                    p =>
                    {
                        var message = "No Animals are needed to feed.";
                        var names = string.Join(",", p.Dogs.Where(d => d.ShouldBeFeed).Select(d => d.Name));
                        if (!string.IsNullOrEmpty(names))
                        {
                            message = "Those aniamals are needed to be feed: " + names;
                        }
                        MessageBox.Show(message);
                    }));
            }
        }




        [Dependency]
        public IDataService DataService { get; set; }
    }
}
