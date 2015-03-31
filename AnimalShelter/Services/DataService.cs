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
                    Breed = "Pug",
                    Gender = Gender.Male,
                    Id = 0,
                    Weight = 29.3,
                    ShelterId = 1,
                    Size = "XL",
                    AnimalKind = AnimalKind.Dog
                },
                new Dog
                {
                    Age = 5.5,
                    Name = "Lady",
                    Breed = "Labrador",
                    Gender = Gender.Female,
                    Id = 1,
                    Weight = 23.5,
                    ShelterId = 2,
                    Size = "L",
                    AnimalKind = AnimalKind.Dog

                },
                new Dog
                {
                    Age = 2,
                    Name = "Winston",
                    Breed = "Bulldog",
                    Gender = Gender.Male,
                    Id = 2,
                    Weight = 8.2,
                    ShelterId = 1,
                    Size = "M",
                    AnimalKind = AnimalKind.Dog
                },
                new Dog
                {
                    Age = 7,
                    Name = "Magic",
                    Breed = "Terrier",
                    Gender = Gender.Male,
                    Id = 3,
                    Weight = 4,
                    ShelterId = 1,
                    Size = "M"
                },
                new Dog
                {
                    Age = 8,
                    Name = "Mrs.Cool",
                    Breed = "The Hound",
                    Gender = Gender.Female,
                    Id = 4,
                    Weight = 24,
                    ShelterId = 2,
                    Size = "XL"

                },
                new Dog
                {
                    Age = 4,
                    Name = "Churchill",
                    Breed = "Bulldog",
                    Gender = Gender.Male,
                    Id = 5,
                    Weight = 15.3,
                    ShelterId = 1,
                    Size = "L"
                },
                new Dog
                {
                    Age = 0.1,
                    Name = "Matt",
                    Breed = "Pincher",
                    Gender = Gender.Male,
                    Id = 6,
                    Weight = 0.4,
                    ShelterId = 1,
                    Size = "M"
                },
                new Dog
                {
                    Age = 3,
                    Name = "Tweety",
                    Breed = "Basset",
                    Gender = Gender.Female,
                    Id = 7,
                    Weight = 2.3,
                    ShelterId = 2,
                    Size = "M"

                },
                new Dog
                {
                    Age = 10,
                    Name = "Biggy",
                    Breed = "Danish",
                    Gender = Gender.Male,
                    Id = 9,
                    Weight = 38.2,
                    ShelterId = 1,
                    Size = "L"
                },
                new Dog
                {
                    Age = 9,
                    Name = "Tale",
                    Breed = "Mastiff",
                    Gender = Gender.Male,
                    Id = 10,
                    Weight = 44.4,
                    ShelterId = 1,
                    Size = "XL"
                },
                new Dog
                {
                    Age = 1.5,
                    Name = "Snow",
                    Breed = "Terrier",
                    Gender = Gender.Male,
                    Id = 11,
                    Weight = 18.2,
                    ShelterId = 1,
                    Size = "L"
                },
                new Dog
                {
                    Age = 0,
                    Name = "Grumpy",
                    Breed = "Cat",
                    Gender = Gender.Male,
                    Id = 12,
                    Weight = 3,
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
                        new ShelterSpace { Size = "XL",TotalUnits = 1, AvailableUnits = 0},
                        new ShelterSpace { Size = "L", TotalUnits = 2, AvailableUnits = 2},
                        new ShelterSpace { Size = "M", TotalUnits = 1, AvailableUnits = 0},
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
                        new ShelterSpace { Size = "XL",TotalUnits = 0, AvailableUnits = 0},
                        new ShelterSpace { Size = "L", TotalUnits = 3, AvailableUnits = 2},
                        new ShelterSpace { Size = "M", TotalUnits = 4, AvailableUnits = 4},
                    },
                    Dogs = new ObservableCollection<Dog>(dogs.Where(d => d.ShelterId == 2))
                },
            });
        }
    }
}
