using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalShelter.Model;
using AnimalShelter.Services;
using Microsoft.Practices.Unity;

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

        [Dependency]
        public IDataService DataService { get; set; }
    }
}
