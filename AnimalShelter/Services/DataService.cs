using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalShelter.Model;

namespace AnimalShelter.Services
{
    internal class DataService : IDataService
    {
        public Task<Dog[]> GetDogs()
        {
            return Task.Run(() => new[]
            {
                new Dog
                {
                    Age = 0.5,
                    Name = "Rex",
                    Breed = "German Shepherd",
                    Gender = "Male",
                    Id = "1234",
                    Weight = 29.3,
                    ShelterId = 1,
                    Size = "XL"
                },
                new Dog
                {
                    Age = 5.5,
                    Name = "Lady",
                    Breed = "Labrador",
                    Gender = "Female",
                    Id = "333",
                    Weight = 23.5,
                    ShelterId = 2,
                    Size = "L"

                },
                new Dog
                {
                    Age = 2,
                    Name = "Winston",
                    Breed = "Bulldog",
                    Gender = "Male",
                    Id = "555",
                    Weight = 8.2,
                    ShelterId = 1,
                    Size = "M"
                },
            });
        }

        public Task<Shelter[]> GetShelters(IEnumerable<Dog> dogs)
        {
            return Task.Run(() => new[]
            {
                new Shelter 
                {
                    Name = "Oregon Humane Society",
                    Id = 1,
                    Address = "1067 NE Columbia Blvd Portland, OR, United States",
                    Spaces = new[]
                    {
                        new ShelterSpace { Size = "XL",Units = 1, Available = 0},
                        new ShelterSpace { Size = "L", Units = 2, Available = 2},
                        new ShelterSpace { Size = "M", Units = 1, Available = 0},
                    },
                    Dogs = new ObservableCollection<Dog>(dogs.Where(d => d.ShelterId == 1))
                },
                                new Shelter 
                {
                    Name = "Family Dogs New Life Shelter",
                    Id = 2,
                    Address = "9101 SE Stanley Ave Portland, OR, United States",
                    Spaces = new[]
                    {
                        new ShelterSpace { Size = "XL",Units = 0, Available = 0},
                        new ShelterSpace { Size = "L", Units = 3, Available = 2},
                        new ShelterSpace { Size = "M", Units = 4, Available = 4},
                    },
                    Dogs = new ObservableCollection<Dog>(dogs.Where(d => d.ShelterId == 2))
                },
            });
        }
    }
}
