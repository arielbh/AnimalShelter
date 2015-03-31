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
                    Gender = Gender.Male,
                    Id = 1234,
                    Weight = 29.3,
                    ShelterId = 1,
                },
                new Dog
                {
                    Age = 5.5,
                    Name = "Lady",
                    Breed = "Labrador",
                    Gender = Gender.Female,
                    Id = 333,
                    Weight = 23.5,
                    ShelterId = 2

                },
                new Dog
                {
                    Age = 2,
                    Name = "Winston",
                    Breed = "Bulldog",
                    Gender = Gender.Male,
                    Id = 555,
                    Weight = 8.2,
                    ShelterId = 1

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
                    Dogs = new ObservableCollection<Dog>(dogs.Where(d => d.ShelterId == 1))
                },
                                new Shelter 
                {
                    Name = "Family Dogs New Life Shelter",
                    Id = 2,
                    Address = "9101 SE Stanley Ave Portland, OR, United States",
                    Dogs = new ObservableCollection<Dog>(dogs.Where(d => d.ShelterId == 2))
                },
            });
        }
    }
}
