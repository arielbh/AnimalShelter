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
        protected bool Equals(Dog other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Dog) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public string Name { get; set; }
        public double Age { get; set; }
        public double Weight { get; set; }
        public int Id { get; set; }
        public Gender Gender { get; set; }
        public Breed Breed { get; set; }

        public string Size { get; set; }

        public int ShelterId { get; set; }

        //TODO: BUG 2: Invalid DogIdentifier
        //STEPS: 1. Step over the return statement, figure out what the bug is, and fix it!
        public string DogIdentifier
        {
            get { return Id + '-' + Gender + '-' + (int)Age + '-' + (Breed+ '-' + Name); }
        }

        //TODO: BUG 1: Human Age Calc
        //STEPS: 1. Step-Over the return value, click Simplify
        //       2. Step into the ConvertDogYearsToHuman method, and do the same to try and figure out the issue.
        public double HumanAge 
        {
            get { return AgeConverter.ConvertDogYearsToHuman(BirthDay); }
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

        public DateTime BirthDay { get { return DateTime.Now - TimeSpan.FromDays(365*Age); } }
    }
}
