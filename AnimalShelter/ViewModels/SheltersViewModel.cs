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
                foreach (var dog in shelter.Dogs)
                {
                    dog.IsFavorite = FavoritesManager.FavoriteDogs.Any(d => d.Id == dog.Id);
                    dog.PropertyChanged += dog_PropertyChanged;
                }
            }

            UpdateFoodRations();
        }

        void dog_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsFavorite")
            {
                Dog dog = sender as Dog;
                if (dog != null && dog.IsFavorite)
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

        private DelegateCommand _showFoodRationsCommand;

        public DelegateCommand ShowFoodRationsCommand
        {
            get
            {
                return _showFoodRationsCommand ?? (_showFoodRationsCommand = new DelegateCommand(ShowFoodRations));
            }
        }

        private void ShowFoodRations()
        {
            UpdateFoodRations();
            var builder = new StringBuilder();
            foreach (var shelter in Shelters)
            {
                foreach (var dog in shelter.Dogs)
                {
                    builder.AppendFormat("{0} should eat {1} grams of dog food today." + Environment.NewLine, 
                                         dog.Name, dog.FoodRation);

                    //TODO: BUG 6: Some dogs get no food!
                    //STEPS: 1. Put a breakpoint here and hit it by clicking the "Update Food Rations" button
                    //       2. Hover over dog.FoodRation, open the Magic Wand, and choose "When Set... Break"
                    //       3. Set a condition for "value == 0" so that you only break only the faulty value.
                    //       4. Click the "Show Food Rations" button again
                }
            }

            MessageBox.Show(builder.ToString());
        }


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
          
            var message = "No Animals need to be fed.";
            //TODO: BUG 3: Dogs not being fed!
            //STEPS: 1. Step over the statement
            //       2. Click the Simplify icon
            //       3. Think about the different options - what could have caused 'names' return an empty string?
            //       4. Hover over each expression box, then expand "Results View" to find out which part 
            //          of the expression is causing problems! Was it the Select call's fault or the Where's?
            var names = string.Join(",", p.Dogs.Where(d => d.ShouldBeFed).Select(d => d.Name));
            if (!string.IsNullOrEmpty(names))
            {
                message = "These animals need to be fed: " + names;
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
                    // BUG: ArrangeSpaces: Checks for Available but without equal
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
                        dog.FoodRation =  (int) (dog.FoodRation / 1.5);
                    }

                }
            }}
    }
}
