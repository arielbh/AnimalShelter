using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuiteValue.UI.MVVM;

namespace AnimalShelter.Model
{
    public class Dog : NotifyObject
    {
        public string Name { get; set; }
        public double Age { get; set; }
        public double Weight { get; set; }
        public int Id { get; set; }
        public Gender Gender { get; set; }
        public string Breed { get; set; }

        public string Size { get; set; }

        public int ShelterId { get; set; }

        //TODO: BUG 2: Invalid DogIdentifier
        public string DogIdentifier
        {
            get { return Id + '-' + Gender + '-' + Breed + '-' + Name; }
        }

        //TODO: BUG 1: Human Age Calc
        public double HumanAge 
        {
            get { return AgeConverter.ConvertDogYearsToHuman(Age); }
        }

        public bool ShouldBeFed { get; set; }
        public AnimalKind AnimalKind { get; set; }
        private bool _isFavorite;
        private int _foodRation;

        public bool IsFavorite
        {
            get { return _isFavorite; }
            set
            {
                if (value != _isFavorite)
                {
                    _isFavorite = value;
                    OnPropertyChanged(() => IsFavorite);
                }
            }
        }

        public Uri ImageUri { get { return new Uri("pack://application:,,,/AnimalShelter;component/Assets/" + Id + ".jpg"); } }
        public bool HasUpToDateVaccine { get; set; }
        public double AgeAtLastVaccine { get; set; }

        public int FoodRation
        {
            get { return _foodRation; }
            set { _foodRation = value; }
        }
    }
}
