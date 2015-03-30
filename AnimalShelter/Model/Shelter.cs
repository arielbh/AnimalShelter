using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalShelter.Views;

namespace AnimalShelter.Model
{
    class Shelter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ObservableCollection<Dog> Dogs { get; set; }
    }
}
