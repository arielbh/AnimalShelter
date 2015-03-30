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
        public string Id { get; set; }
        public string Gender { get; set; }
        public string Breed { get; set; }

        public int ShelterId { get; set; }

        public string GetDogIdentifierMethod()
        {
            return Id + "-" + Gender + "-" + Breed + "-" + Name;
        }

        public double HumanAge {
            get { return Age*70; }}

        
    }
}
