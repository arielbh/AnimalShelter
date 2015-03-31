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
            foreach (var shelter in Shelters)
            {
                shelter.IsFavorite = FavoritesManager.FavoriteShelters.Any(s => s.Id == shelter.Id);

                shelter.PropertyChanged += shelter_PropertyChanged;
            }

        }

        void shelter_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsFavorite")
            {
                Shelter shelter = sender as Shelter;
                if (shelter.IsFavorite)
                {
                    FavoritesManager.FavoriteShelters.Add(shelter);
                }
                else
                {
                    FavoritesManager.FavoriteShelters.Remove(shelter);
                }
                FavoritesManager.Save();
            }
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

        private DelegateCommand _updateFoodRationsCommand;

        public DelegateCommand UpdateFoodRationsCommand
        {
            get
            {
                return _updateFoodRationsCommand ?? (_updateFoodRationsCommand = new DelegateCommand(
                    UpdateFoodRations));
            }
        }

        private void UpdateFoodRations()
        {
            foreach (var shelter in Shelters)
            {
                foreach (var dog in shelter.Dogs)
                {
                    var ration = dog.Size.Select(c => (int) c).Sum();
                    if (dog.Gender == Gender.Male)
                    {
                        dog.FoodRation = ration*3;

                    }
                    if (dog.Gender == Gender.Female)
                    {
                        //TODO: Bug: not using ration with female dogs
                        dog.FoodRation =  (int) (dog.FoodRation / 1.5);
                    }

                }
            }}


        private DelegateCommand<Shelter> _feedCommand;

        public DelegateCommand<Shelter> FeedCommand
        {
            get
            {
                return _feedCommand ?? (_feedCommand = new DelegateCommand<Shelter>(DisplayDogsToFeed));
            }
        }

        private static void DisplayDogsToFeed(Shelter p)
        {    
            //TODO: BUG 3: Dogs not being fed!
            var message = "No Animals are needed to feed.";
            var names = string.Join(",", p.Dogs.Where(d => d.ShouldBeFed).Select(d => d.Name));
            if (!string.IsNullOrEmpty(names))
            {
                message = "Those animals are needed to be feed: " + names;
            }
            MessageBox.Show(message);
        }

        private DelegateCommand _arrangeSpacesCommand;
        private IFavoritesManager _favoritesManager;

        public DelegateCommand ArrangeSpacesCommand
        {
            get
            {
                return _arrangeSpacesCommand ?? (_arrangeSpacesCommand = new DelegateCommand(async () =>
                    {
                        await ArrangeSpacesForDogs();
                    }));
            }
        }

        private async Task ArrangeSpacesForDogs()
        {
            var dogs = await DataService.GetDogs();
            ResetDogs(dogs);
            var groups = dogs.GroupBy(d => d.Size);
            foreach (var group in groups)
            {
                foreach (var shelter in Shelters)
                {
                    var space = shelter.Spaces.FirstOrDefault(s => s.Size == @group.Key);
                    //TODO: BUG: ArrangeSpaces: Checks for Available but without equal
                    if (space.AvailableUnits > @group.Where(d => d.ShelterId == 0).Count())
                    {
                        space.AvailableUnits -= @group.Count();
                        AssignDogsToShelter(@group.Select(d => d), shelter);
                    }
                    else
                    {
                        var toAssignDogs = @group.Where(d => d.ShelterId == 0).Take(space.AvailableUnits);
                        AssignDogsToShelter(toAssignDogs, shelter);
                    }
                }
            }
        }

        private void AssignDogsToShelter(IEnumerable<Dog> dogs, Shelter shelter)
        {
            foreach (var dog in dogs)
            {
                dog.ShelterId = shelter.Id;
                shelter.Dogs.Add(dog);
            }
        }

        private void ResetDogs(Dog[] dogs)
        {
            foreach (var shelter in Shelters)
            {
                shelter.Dogs.Clear();
                foreach (var space in shelter.Spaces)
                {
                    space.AvailableUnits = space.TotalUnits;
                }
            }
            foreach (var dog in dogs)
            {
                dog.ShelterId = 0;
            }
        }


        [Dependency]
        public IDataService DataService { get; set; }
        [Dependency]
        public IFavoritesManager FavoritesManager
        {
            get { return _favoritesManager; }
            set
            {
                _favoritesManager = value;
                _favoritesManager.Load();
            }
        }
    }
}
