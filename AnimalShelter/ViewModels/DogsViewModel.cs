using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using AnimalShelter.Model;
using AnimalShelter.Services;
using Microsoft.Practices.Unity;

namespace AnimalShelter.ViewModels
{
    class DogsViewModel : HeaderViewModel
    {
        public DogsViewModel()
        {
            Header = "Dogs";
        }

        public async override void Activate()
        {
            base.Activate();
            var dogs = await DataService.GetDogs();
            Dogs = CollectionViewSource.GetDefaultView(dogs);
        }

        private Dog _selectedDog;

        public Dog SelectedDog
        {
            get { return _selectedDog; }
            set
            {
                if (value != _selectedDog)
                {
                    _selectedDog = value;
                    OnPropertyChanged(() => SelectedDog);
                }
            }
        }


        private ICollectionView _dogs;
        private FilterViewModel _filterViewModel;

        public ICollectionView Dogs
        {
            get { return _dogs; }
            set
            {
                if (value != _dogs)
                {
                    _dogs = value;
                    OnPropertyChanged(() => Dogs);
                }
            }
        }

        [Dependency]
        public IDataService DataService { get; set; }

        [Dependency]
        public FilterViewModel FilterViewModel
        {
            get { return _filterViewModel; }
            set
            {
                _filterViewModel = value;
                _filterViewModel.OnFilter += OnFilter;
            }
        }

        private void OnFilter(bool isClear)
        {
            if (isClear)
            {
                Dogs.Filter = null;
                return;
            }
            Dogs.Filter = d => FilterViewModel.Filter.All(x => x((Dog) d));

        }
    }
}
