using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelter.Model
{
    public class Dog
    {
        public string Name { get; set; }
        public double Age { get; set; }
        public double Weight { get; set; }
        public int Id { get; set; }
        public Gender Gender { get; set; }
        public string Breed { get; set; }

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
    }
}
