using System;
using System.Collections.Generic;
using AnimalShelter.Model;
using SuiteValue.UI.MVVM;

namespace AnimalShelter.ViewModels
{
    public class FilterViewModel : ViewModelBase
    {
        private bool _fromAgeIsChecked;

        public bool FromAgeIsChecked
        {
            get { return _fromAgeIsChecked; }
            set
            {
                if (value != _fromAgeIsChecked)
                {
                    _fromAgeIsChecked = value;
                    OnPropertyChanged(() => FromAgeIsChecked);
                    FilterCommand.Refresh();
                }
            }
        }

        private double _fromAge;

        public double FromAge
        {
            get { return _fromAge; }
            set
            {
                if (value != _fromAge)
                {
                    _fromAge = value;
                    OnPropertyChanged(() => FromAge);
                }
            }
        }

        public bool NeedsToBeWashedIsChecked
        {
            get { return _NeedsToBeWashedIsChecked; }
            set
            {
                if (value != _NeedsToBeWashedIsChecked)
                {
                    _NeedsToBeWashedIsChecked = value;
                    OnPropertyChanged(() => NeedsToBeWashedIsChecked);
                    FilterCommand.Refresh();
                }
                
            }
        }

        private bool _toAgeIsChecked;

        public bool ToAgeIsChecked
        {
            get { return _toAgeIsChecked; }
            set
            {
                if (value != _toAgeIsChecked)
                {
                    _toAgeIsChecked = value;
                    OnPropertyChanged(() => ToAgeIsChecked);
                    FilterCommand.Refresh();
                }
            }
        }

        private double _toAge;

        public double ToAge
        {
            get { return _toAge; }
            set
            {
                if (value != _toAge)
                {
                    _toAge = value;
                    OnPropertyChanged(() => ToAge);
                }
            }
        }


        private DelegateCommand _filterCommand;

        public DelegateCommand FilterCommand
        {
            get
            {
                return _filterCommand ?? (_filterCommand = new DelegateCommand(
                    () =>
                    {
                        var list = new List<Func<Dog,bool>>();
                        if (FromAgeIsChecked)
                        {
                            list.Add(d => d.Age > FromAge);
                        }
                        if (ToAgeIsChecked)
                        {
                            list.Add(d => d.Age < ToAge);
                        }

                        if (NeedsToBeWashedIsChecked)
                        {
                            list.Add(ShouldBeWashed);
                        }
                        Filter = list.ToArray();
                        OnFilter(false);
                    },
                    () =>
                    {
                        return FromAgeIsChecked || ToAgeIsChecked || NeedsToBeWashedIsChecked;
                    }));
            }
        }

        private static bool ShouldBeWashed(Dog dog)
        {
            //TODO: BUG 4: The filter for animals that need to be washed regularly is not working.
            //               At least one cat, Grumpy, needs to be washed, but the system isn't reporting it.
            //STEPS: 1. Notice that you are seeing a predicted result of what the expression will return
            //       2. Use Edit & Continue to fix the bug in this line of code. You will get instant feedback!
            //       3. Hover over dog.Age. Double click it and change the Age to a negative number. See what happens.
            return (dog.AnimalKind == AnimalKind.Dog && dog.AnimalKind == AnimalKind.Cat) && dog.Age > 0.1;
        }

        private DelegateCommand _clearCommand;
        private bool _NeedsToBeWashedIsChecked;

        public DelegateCommand ClearCommand
        {
            get
            {
                return _clearCommand ?? (_clearCommand = new DelegateCommand(
                    () =>
                    {
                        FromAge = 0;
                        FromAgeIsChecked = false;
                        ToAge = 0;
                        ToAgeIsChecked = false;
                        Filter = null;
                        OnFilter(true);
                    }));
            }
        }




        public Func<Dog,bool>[] Filter { get; set; }

        public event Action<bool> OnFilter = delegate {  };




    }
}