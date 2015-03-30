using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using SuiteValue.UI.MVVM;

namespace AnimalShelter.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Parts = new HeaderViewModel[]
            {
                App.Container.Resolve<DogsViewModel>(),
                App.Container.Resolve<SheltersViewModel>(),

            };
            SelectedPart = Parts.First();
        }

        private HeaderViewModel _selectedPart;

        public HeaderViewModel SelectedPart
        {
            get { return _selectedPart; }
            set
            {
                if (value != _selectedPart)
                {
                    _selectedPart = value;
                    OnPropertyChanged(() => SelectedPart);
                    if (SelectedPart != null)
                    {
                        SelectedPart.Activate();
                    }
                }
            }
        }

        public HeaderViewModel[] Parts { get; set; }
    }
}
