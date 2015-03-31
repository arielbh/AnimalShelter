using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            foreach (var dog in dogs)
            {
                dog.IsFavorite = FavoritesManager.FavoriteDogs.Any(d => d.Id == dog.Id);
                // TODO: BUG 6: Snow the dog needs to be vaccinated, by the system thinks otherwise!
                dog.HasUpToDateVaccine = (dog.Age - dog.AgeAtLastVaccine) > GetMaximumYearsBetweenShots(dog.Breed);
                dog.PropertyChanged += dog_PropertyChanged;
            }
            Dogs = CollectionViewSource.GetDefaultView(dogs);
        }

        private double GetMaximumYearsBetweenShots(string breed)
        {
            switch (breed)
            {
                case "Pug":
                case "Labrador":
                case "Bulldog":
                    return 0.5;
                case "Pincher":
                case "Basset":
                case "Danish":
                    return 1;
                case "Mastiff":
                case "The Hound":
                    return 2;
                default:
                    return 0;
            }
        }

        void dog_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsFavorite2")
            {
                Dog dog = sender as Dog;
                if (dog.IsFavorite)
                {
                    FavoritesManager.FavoriteDogs.Add(dog);
                }
                else
                {                                  
                    FavoritesManager.FavoriteDogs.Remove(dog);
                }
                FavoritesManager.Save();
            }
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
        private IFavoritesManager _favoritesManager;

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
        public IFavoritesManager FavoritesManager
        {
            get { return _favoritesManager; }
            set { _favoritesManager = value;
                _favoritesManager.Load();
            }
        }

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
