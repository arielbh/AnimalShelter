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

        private DelegateCommand _arrangeSpacesCommand;

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
                    if (space.Available > @group.Where(d => d.ShelterId == 0).Count())
                    {
                        space.Available -= @group.Count();
                        AssignDogsToShelter(@group.Select(d => d), shelter);
                    }
                    else
                    {
                        var toAssignDogs = @group.Where(d => d.ShelterId == 0).Take(space.Available);
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
                    space.Available = space.Units;
                }
            }
            foreach (var dog in dogs)
            {
                dog.ShelterId = 0;
            }
        }


        [Dependency]
        public IDataService DataService { get; set; }
    }
}
